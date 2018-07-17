using System;
using Microsoft.Extensions.Logging;

namespace NHibernate.NetCore {

    public class NetCoreLogger : INHibernateLogger {

        private ILogger logger;

        public NetCoreLogger(
            ILogger logger
        ) {
            this.logger = logger;
        }

        public bool IsEnabled(NHibernateLogLevel logLevel) {
            var level = (int)logLevel;
            var msLogLevel = (LogLevel)level;
            return logger.IsEnabled(msLogLevel);
        }

        public void Log(
            NHibernateLogLevel logLevel,
            NHibernateLogValues state,
            Exception exception
        ) {
            var level = (int)logLevel;
            var msLogLevel = (LogLevel)level;
            logger.Log(
                msLogLevel,
                default(EventId),
                state,
                exception,
                (s, ex) => {
                    var message = s.ToString();
                    if (ex != null) {
                        message += ex.ToString();
                    }
                    return message;
                }
            );
        }
    }

}
