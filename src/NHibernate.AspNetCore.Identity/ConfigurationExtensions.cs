using System;
using NHibernate.Cfg;
using NHibernate.Mapping.ByCode;

namespace NHibernate.AspNetCore.Identity {

    public static class ConfigurationExtensions {

        public static Configuration AddIdentityMappings(this Configuration cfg) {
            var dialect = cfg.GetProperty(NHibernate.Cfg.Environment.Dialect);
            if (dialect.IndexOf("PostgreSQL", StringComparison.OrdinalIgnoreCase) >= -1) {
                cfg.AddIdentityMappingsForPostgres();
            }
            else if (dialect.IndexOf("MySQL", StringComparison.OrdinalIgnoreCase) >= -1) {
                cfg.AddIdentityMappingsForMySql();
            }
            else if (dialect.IndexOf("MsSql", StringComparison.OrdinalIgnoreCase) >= -1) {
                cfg.AddIdentityMappingsForMsSql();
            }
            else if (dialect.IndexOf("Sqlite", StringComparison.OrdinalIgnoreCase) >= -1) {
                cfg.AddIdentityMappingsForSqlite();
            }
            else {
                throw new NotSupportedException(
                    "Only support PostgreSQL, MsSql, MySQL, Sqlite, your dialect must contain one of these words!"
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

        public static Configuration AddIdentityMappingsForSqlite(
            this Configuration cfg
        ) {
            var mapping = ConfigurationHelper.GetIdentityMappingForSqlite();
            cfg.AddXml(mapping.AsString());
            return cfg;
        }
    }

}
