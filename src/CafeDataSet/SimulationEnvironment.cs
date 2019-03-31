using CafeDataSet.Randomness;
using CafeDataSet.Simulation;
using Serilog.Core;

namespace CafeDataSet
{
    class SimulationEnvironment
    {
        public IClock Clock { get; }
        public SimulationState State { get; }
        public Logger Log { get; }
        public IPrng Random { get; }

        public SimulationEnvironment(IClock clock, SimulationState state, Logger log, IPrng random)
        {
            Clock = clock;
            State = state;
            Log = log;
            Random = random;
        }
    }
}