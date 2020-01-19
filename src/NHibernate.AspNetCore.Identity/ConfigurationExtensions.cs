using System;
using NHibernate.Cfg;
using NHibernate.Mapping.ByCode;

namespace NHibernate.AspNetCore.Identity {

    public static class ConfigurationExtensions {

        public static Configuration AddIdentityMappings(
            this Configuration cfg
        ) {
            var dialect = cfg.GetProperty(NHibernate.Cfg.Environment.Dialect);
            if (dialect.Contains("PostgreSQL", StringComparison.OrdinalIgnoreCase)) {
                cfg.AddIdentityMappingsForPostgres();
            }
            else if (dialect.Contains("MySQL", StringComparison.OrdinalIgnoreCase)) {
                cfg.AddIdentityMappingsForMySql();
            }
            else if (dialect.Contains("MsSql", StringComparison.OrdinalIgnoreCase)) {
                cfg.AddIdentityMappingsForMsSql();
            }
            else {
                throw new NotSupportedException(
                    "Only support PostgreSQL, MsSql, MySQL, your dialet must contains one of these words!"
                );
            }
            return cfg;
        }

        public static Configuration AddIdentityMappingsForPostgres(
            this Configuration cfg
        ) {
            var mapping = ConfigurationHelper.GetIdentityMappingForPostgreSql();
            cfg.AddXml(mapping.AsString());
            return cfg;
        }

        public static Configuration AddIdentityMappingsForMsSql(
            this Configuration cfg
        ) {
            var mapping = ConfigurationHelper.GetIdentityMappingForMsSql();
            cfg.AddXml(mapping.AsString());
            return cfg;
        }

        public static Configuration AddIdentityMappingsForMySql(
            this Configuration cfg
        ) {
            var mapping = ConfigurationHelper.GetIdentityMappingForMySql();
            cfg.AddXml(mapping.AsString());
            return cfg;
        }

    }

}
