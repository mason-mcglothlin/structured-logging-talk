using System;

namespace CafeDataSet
{
    interface IClock
    {
        bool Advance();
        DateTime UtcNow { get; }
        DateTimeOffset OffsetNow { get; }
    }
}
