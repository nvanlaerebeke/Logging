using ILogging;
using System;

namespace Logging {

    public class LogFactory {
        private static ILogProvider Provider { get; set; }

        public static void Initialize(ILogProvider pProvider) {
            Provider = pProvider;
        }

        public static ILogProvider GetProvider() {
            return Provider;
        }

        public static ILog GetLogger(Type pType) {
            return Provider.GetLogger(pType);
        }

        public static ILog GetLogger() {
            return Provider.GetLogger();
        }
    }
}