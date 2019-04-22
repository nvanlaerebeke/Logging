using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using ILogging;
using System;

namespace Logging.Log4Net {

    public class LogProvider : ILogProvider {

        /// <summary>
        /// Creates and initializes a new log provider
        /// </summary>
        /// <param name="pInitialize"></param>
        public LogProvider(bool pInitialize = true) {
            if (pInitialize) {
                Initialize(new Log4NetParams());
            }
        }

        public LogProvider(Log4NetParams pParams) {
            Initialize(pParams);
        }

        private void Initialize(Log4NetParams pParams) {
            //Configure log4net
            var patternLayout = new PatternLayout() {
                ConversionPattern = "%date [%thread] %-5level %logger{1} - %message%newline"
            };
            patternLayout.ActivateOptions();

            var hierarchy = (Hierarchy)LogManager.GetRepository();
            switch (pParams.LogLevel) {
                case LogLevel.Debug:
                    hierarchy.Root.Level = Level.Debug;
                    break;

                case LogLevel.Error:
                    hierarchy.Root.Level = Level.Error;
                    break;

                case LogLevel.Info:
                    hierarchy.Root.Level = Level.Info;
                    break;

                case LogLevel.Off:
                    hierarchy.Root.Level = Level.Off;
                    break;

                case LogLevel.Warn:
                    hierarchy.Root.Level = Level.Warn;
                    break;

                case LogLevel.Trace:
                    hierarchy.Root.Level = Level.Trace;
                    break;
            }

            if (!String.IsNullOrEmpty(pParams.LogFilePath)) {
                var roller = new RollingFileAppender {
                    AppendToFile = true,
                    File = pParams.LogFilePath,
                    Layout = patternLayout,
                    ImmediateFlush = true,
                    MaxSizeRollBackups = 5,
                    MaximumFileSize = "10MB",
                    RollingStyle = RollingFileAppender.RollingMode.Size,
                    StaticLogFileName = true
                };
                roller.ActivateOptions();
                hierarchy.Root.AddAppender(roller);
            } else {
                var consoleappender = new ConsoleAppender {
                    Layout = patternLayout
                };
                consoleappender.ActivateOptions();
                hierarchy.Root.AddAppender(consoleappender);
            }
            hierarchy.Configured = true;
        }

        public ILogging.ILog GetLogger(Type pType) {
            return new Log(LogManager.GetLogger(pType));
        }

        public ILogging.ILog GetLogger() {
            return new Log(LogManager.GetLogger("Main"));
        }
    }
}