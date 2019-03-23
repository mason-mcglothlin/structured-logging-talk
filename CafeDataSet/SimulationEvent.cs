using System;
using CafeDataSet.Randomness;
using CafeDataSet.Simulation;
using Serilog;

namespace CafeDataSet
{
    abstract class SimulationEvent
    {
        readonly IClock _clock;
        readonly ISchedule _schedule;
        readonly SimulationState _state;

        protected ILogger Log { get; }
        protected IPrng Random { get; }

        protected SimulationEvent(SimulationEnvironment environment, ISchedule schedule)
        {
            _clock = environment.Clock;
            _schedule = schedule;
            _state = environment.State;

            Log = environment.Log;
            Random = environment.Random;
        }

        public void OnTick()
        {
            if (_schedule.AdvanceIfDue(_clock.UtcNow))
                Execute(_state, _clock.UtcNow);
        }

        protected abstract void Execute(SimulationState state, DateTime utcNow);
    }
}