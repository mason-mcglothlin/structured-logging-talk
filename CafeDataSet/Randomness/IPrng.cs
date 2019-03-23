using System;

namespace CafeDataSet.Randomness
{
    interface IPrng
    {
        int Next();
        double NextDouble();
        Guid NewGuid();
    }
}
