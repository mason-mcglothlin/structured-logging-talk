using System;
using System.Collections.Generic;

namespace CafeDataSet.Randomness
{
    class DeterministicPrng : IPrng
    {
        readonly Random _random = new Random(5341);

        public int Next()
        {
            return _random.Next();
        }

        public double NextDouble()
        {
            return _random.NextDouble();
        }

        public Guid NewGuid()
        {
            var bytes = new List<byte>();
            for (var i = 0; i < 4; ++i)
            {
                foreach (var b in BitConverter.GetBytes(_random.Next()))
                    bytes.Add(b);
            }
            return new Guid(bytes.ToArray());
        }
    }
}
