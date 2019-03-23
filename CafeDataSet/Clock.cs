using System;
using CafeDataSet.Randomness;

namespace CafeDataSet
{
    class Clock : IClock
    {
        const long ResolutionTicks = TimeSpan.TicksPerSecond / 5;

        readonly IPrng _random;
        readonly DateTime _endUtc;
        readonly TimeSpan _offset = DateTimeOffset.Now.Offset;

        DateTime _utcNow;

        public Clock(DateTime startUtc, DateTime endUtc, IPrng random)
        {
            _utcNow = startUtc;
            _endUtc = endUtc;
            _random = random;
        }

        public bool Advance()
        {
            // ReSharper disable once PossibleLossOfFraction
            var distance = ResolutionTicks / 2 + (long) (_random.NextDouble() * ResolutionTicks);
            var newNow = _utcNow.AddTicks(distance);
            if (newNow > _endUtc)
                return false;

            _utcNow = newNow;
            return true;
        }

        public DateTime UtcNow => _utcNow;

        public DateTimeOffset OffsetNow => new DateTimeOffset(_utcNow.ToLocalTime(), _offset);
    }
}