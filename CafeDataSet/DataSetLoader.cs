using System;
using System.Collections.Generic;
using System.Threading;
using CafeDataSet.Randomness;
using CafeDataSet.Simulation;
using CafeDataSet.Simulation.Integration;
using CafeDataSet.Simulation.Ordering;
using CafeDataSet.Simulation.Website;
using CafeDataSet.TimeWarp;
using Serilog;
using Serilog.Debugging;

namespace CafeDataSet
{
    public static class DataSetLoader
    {
        public static void RunInteractive(string serverUrl, string apiKey, CancellationToken cancel)
        {
            RunInteractive(serverUrl, apiKey, 0, cancel);
        }

        public static void RunInteractive(string serverUrl, string apiKey, int loaderId, CancellationToken cancel)
        {
            Run(serverUrl, apiKey, true, null, null, loaderId, cancel);
        }

        public static void Load(string serverUrl, DateTime startUtc, DateTime endUtc)
        {
            Run(serverUrl, null, false, startUtc, endUtc, 0, CancellationToken.None);
        }

        static void Run(string serverUrl, string apiKey, bool realTime, DateTime? startUtc, DateTime? endUtc, int loaderId, CancellationToken cancel)
        {
            var random = realTime ? (IPrng) new PseudorandomPrng(loaderId) : new DeterministicPrng();

            var clock = realTime ? 
                (IClock)new WallClock() :
                // ReSharper disable PossibleInvalidOperationException
                new Clock(startUtc.Value, endUtc.Value, random);

            using (var log = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.TimeWarp(
                    e => clock.OffsetNow,
                    writeTo => writeTo.Seq(
                        serverUrl,
                        apiKey: apiKey,
                        period: realTime ? TimeSpan.FromSeconds(2) : TimeSpan.FromMilliseconds(1),
                        batchPostingLimit: 1000,
                        compact: true,
                        queueSizeLimit: 10_000_000))
                .CreateLogger())
            {
                var state = new SimulationState();
                var environment = new SimulationEnvironment(clock, state, log, random);

                var items = new List<SimulationEvent>
                {
                    new OrderingProcessOrder(environment),
                    new UpdateProductCatalog(environment),
                    new WebsiteCompleteRequest(environment),
                    new WebsiteStartRequest(environment, realTime)
                };

                if (!realTime)
                {
                    var throttle = new Throttle(environment);

                    SelfLog.Enable(m =>
                    {
                        Console.Error.WriteLine(m);
                        throttle.SlowDown();
                    });

                    items.Add(throttle);
                }

                while (clock.Advance())
                {
                    cancel.ThrowIfCancellationRequested();

                    foreach (var item in items)
                        item.OnTick();
                }
            }
        }
    }
}
