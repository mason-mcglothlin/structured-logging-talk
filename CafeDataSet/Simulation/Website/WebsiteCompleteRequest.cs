using System;
using CafeDataSet.Simulation.Messages;
using Serilog.Context;
using Serilog.Events;

namespace CafeDataSet.Simulation.Website
{
    class WebsiteCompleteRequest : SimulationEvent
    {
        public WebsiteCompleteRequest(SimulationEnvironment environment)
            : base(environment, new IntervalSchedule(TimeSpan.FromSeconds(1), TimeSpan.FromMilliseconds(800), environment.Clock.UtcNow, environment.Random))
        {
        }

        protected override void Execute(SimulationState state, DateTime utcNow)
        {
            foreach (var request in state.Requests)
            {
                using (LogContext.PushProperty("RequestId", request.Id))
                {
                    if (request.PlacedOrder != null)
                    {
                        var message = new PlaceOrderCommand(request.PlacedOrder);
                        state.Messages.Enqueue(message);
                        Log.ForContext("SourceContext", "SeqCafe.Messaging.MessageBus")
                            .Information("Published message {MessageId} of type {MessageType}", message.Id, message.GetType());
                    }

                    var total = utcNow - request.Started;
                    var level = total > TimeSpan.FromMilliseconds(1500)
                        ? LogEventLevel.Warning
                        : LogEventLevel.Information;

                    Log.ForContext("SourceContext", "SeqCafe.Web.DemoMiddleware")
                        .Write(level, "HTTP {Method} {RequestPath} responded {StatusCode} in {Elapsed:0.000} ms",
                        request.Method, request.Path, request.StatusCode, total.TotalMilliseconds);
                }
            }

            state.Requests.Clear();
        }
    }
}
