using System;
using CafeDataSet.Randomness;

namespace CafeDataSet
{
    class IntervalSchedule : ISchedule
    {
        readonly Func<DateTime, TimeSpan> _getIntervalAt;
        readonly TimeSpan _accuracyPlusOrMinus;
        readonly IPrng _random;

        DateTime _dueAt;

        public IntervalSchedule(TimeSpan interval, TimeSpan accuracyPlusOrMinus, DateTime startUtc, IPrng random)
            : this(now => interval, accuracyPlusOrMinus, startUtc, random)
        {
        }

        public IntervalSchedule(Func<DateTime, TimeSpan> getIntervalAt, TimeSpan accuracyPlusOrMinus, DateTime startUtc, IPrng random)
        {
            _getIntervalAt = getIntervalAt;
            _accuracyPlusOrMinus = accuracyPlusOrMinus;
            _random = random;
            _dueAt = Advance(startUtc);
        }

        public bool AdvanceIfDue(DateTime utcNow)
        {
            if (utcNow > _dueAt)
            {
                _dueAt = Advance(_dueAt);
                return true;
            }

            return false;
        }

        DateTime Advance(DateTime from)
        {
            var baseDistance = _getIntervalAt(from) - _accuracyPlusOrMinus;
            if (baseDistance < TimeSpan.Zero)
                baseDistance = TimeSpan.Zero;

            var added = (long)(_random.NextDouble() * (2 * _accuracyPlusOrMinus.Ticks));

            return from.Add(baseDistance).AddTicks(added);
        }
    }
}