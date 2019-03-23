using System;
using System.Threading;
using CafeDataSet.Simulation;

namespace CafeDataSet
{
    class Throttle : SimulationEvent
    {
        int _sleepMilliseconds = 500;

        public Throttle(SimulationEnvironment environment)
            : base(environment, new IntervalSchedule(TimeSpan.FromHours(1), TimeSpan.FromSeconds(1), environment.Clock.UtcNow, environment.Random))
        {
        }

        protected override void Execute(SimulationState state, DateTime utcNow)
        {
            Thread.Sleep(_sleepMilliseconds);
        }

        public void SlowDown()
        {
            _sleepMilliseconds = (int)(_sleepMilliseconds * 1.5);
        }
    }
}
