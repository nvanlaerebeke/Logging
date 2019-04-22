using ILogging;

namespace Logging.Log4Net {

    public class Log4NetParams {

        public Log4NetParams() {
        }

        public LogLevel LogLevel { get; set; } = LogLevel.Debug;
        public string LogFilePath { get; set; } = "";
    }
}