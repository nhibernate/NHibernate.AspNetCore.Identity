using System;
using Microsoft.Extensions.Logging;

namespace NHibernate.NetCore {

    public class NetCoreLoggerFactory : INHibernateLoggerFactory {

        private Microsoft.Extensions.Logging.ILoggerFactory loggerFactory;

        public NetCoreLoggerFactory(
            Microsoft.Extensions.Logging.ILoggerFactory loggerFactory
        ) {
            this.loggerFactory = loggerFactory;
        }

        public INHibernateLogger LoggerFor(string keyName) {
            var logger = loggerFactory.CreateLogger(keyName);
            return new NetCoreLogger(logger);
        }

        public INHibernateLogger LoggerFor(System.Type type) {
            var logger = loggerFactory.CreateLogger(type);
            return new NetCoreLogger(logger);
        }
    }

}
