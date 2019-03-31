using System;

namespace CafeDataSet.Simulation.Generators
{
    class PrefixedUniqueId : IdGen
    {
        readonly string _prefix;
        readonly int _suffLen;

        public PrefixedUniqueId(string prefix, int suffLen)
        {
            _prefix = prefix;
            _suffLen = suffLen;
        }

        public override object Next()
        {
            var suf = Guid.NewGuid().ToString("N").Substring(0, _suffLen);
            return _prefix + suf;
        }
    }
}