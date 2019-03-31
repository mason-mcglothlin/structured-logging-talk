using System.Linq;

namespace CafeDataSet.Simulation.Generators
{
    class CompositeId : IdGen
    {
        readonly IdGen[] _components;

        public CompositeId(params IdGen[] components)
        {
            _components = components;
        }

        public override object Next()
        {
            return string.Join("", _components.Select(c => c.Next().ToString()));
        }
    }
}