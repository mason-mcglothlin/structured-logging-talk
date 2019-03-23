using System;
using System.Threading;

namespace CafeDataSet
{
    class WallClock : IClock
    {
        public bool Advance()
        {
            Thread.Sleep(1);
            return true;
        }

        public DateTime UtcNow => DateTime.UtcNow;

        public DateTimeOffset OffsetNow => DateTimeOffset.Now;
    }
}
