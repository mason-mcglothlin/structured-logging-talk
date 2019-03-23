using System;

namespace CafeDataSet.Randomness
{
    class PseudorandomPrng : IPrng
    {
        readonly Random _random;

        public PseudorandomPrng(int seed)
        {
            _random = new Random(seed + (int)DateTime.Now.Ticks);
        }

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
            return Guid.NewGuid();
        }
    }
}
