using System;

namespace ILogging {
	public interface ILogProvider {
		ILog GetLogger(Type pType);
		ILog GetLogger();
	}
}