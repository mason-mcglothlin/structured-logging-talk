using System;

namespace CafeDataSet
{
    interface ISchedule
    {
        bool AdvanceIfDue(DateTime utcNow);
    }
}