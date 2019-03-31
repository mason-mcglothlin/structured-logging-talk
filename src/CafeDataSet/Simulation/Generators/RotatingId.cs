using System.Threading;

namespace CafeDataSet.Simulation.Generators
{
    class RotatingId : IdGen
    {
        readonly object[] _options;
        int _next;

        public RotatingId(params object[] options)
        {
            _options = options;
        }

        public override object Next()
        {
            var next = Interlocked.Increment(ref _next);
            return _options[next % _options.Length];
        }
    }
}