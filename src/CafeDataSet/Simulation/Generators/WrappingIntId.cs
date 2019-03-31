using System.Threading;

namespace CafeDataSet.Simulation.Generators
{
    class WrappingIntId : IdGen
    {
        readonly int _wrap;
        int _next;

        public WrappingIntId(int wrap)
        {
            _wrap = wrap;
        }

        public override object Next()
        {
            return Interlocked.Increment(ref _next) % _wrap;
        }
    }
}
