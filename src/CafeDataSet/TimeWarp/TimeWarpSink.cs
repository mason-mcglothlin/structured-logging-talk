using System;
using System.Linq;
using Serilog.Core;
using Serilog.Events;

namespace CafeDataSet.TimeWarp
{
    class TimeWarpSink : IDisposable, ILogEventSink
    {
        readonly ILogEventSink _target;
        readonly Func<LogEvent, DateTimeOffset> _getTimestamp;

        public TimeWarpSink(ILogEventSink target, Func<LogEvent, DateTimeOffset> getTimestamp)
        {
            _target = target ?? throw new ArgumentNullException(nameof(target));
            _getTimestamp = getTimestamp ?? throw new ArgumentNullException(nameof(getTimestamp));
        }

        public void Dispose()
        {
            (_target as IDisposable)?.Dispose();
        }

        public void Emit(LogEvent logEvent)
        {
            var timestamp = _getTimestamp(logEvent);
            var surrogate = new LogEvent(timestamp, logEvent.Level, logEvent.Exception, logEvent.MessageTemplate,
                logEvent.Properties.Select(kv => new LogEventProperty(kv.Key, kv.Value)));
            _target.Emit(surrogate);
        }
    }
}
