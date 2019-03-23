using System;

namespace CafeDataSet
{
    class ReactiveSchedule : ISchedule
    {
        public bool AdvanceIfDue(DateTime utcNow)
        {
            return true;
        }
    }
}
