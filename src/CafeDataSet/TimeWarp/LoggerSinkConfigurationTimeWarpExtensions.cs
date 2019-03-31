using System;
using Serilog;
using Serilog.Configuration;
using Serilog.Events;

namespace CafeDataSet.TimeWarp
{
    public static class LoggerSinkConfigurationTimeWarpExtensions
    {
        public static LoggerConfiguration TimeWarp(this LoggerSinkConfiguration loggerSinkConfiguration, Func<LogEvent, DateTimeOffset> getTimestamp, Action<LoggerSinkConfiguration> configure)
        {
            return LoggerSinkConfiguration.Wrap(loggerSinkConfiguration, sink => new TimeWarpSink(sink, getTimestamp), configure, LevelAlias.Minimum, null);
        }
    }
}