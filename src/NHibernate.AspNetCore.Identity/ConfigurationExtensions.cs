using NHibernate.Cfg;
using NHibernate.Mapping.ByCode;

namespace NHibernate.AspNetCore.Identity;

public static class ConfigurationExtensions {

    extension(Configuration cfg) {

        public Configuration AddIdentityMappings() {
            var dialect = cfg.GetProperty(NHibernate.Cfg.Environment.Dialect);
            if (dialect.IndexOf("PostgreSQL", StringComparison.OrdinalIgnoreCase) > -1 || dialect.IndexOf("Npgsql", StringComparison.OrdinalIgnoreCase) > -1) {
                cfg.AddIdentityMappingsForPostgres();
            }
            else if (dialect.IndexOf("MySQL", StringComparison.OrdinalIgnoreCase) > -1) {
                cfg.AddIdentityMappingsForMySql();
            }
            else if (dialect.IndexOf("MsSql", StringComparison.OrdinalIgnoreCase) > -1) {
                cfg.AddIdentityMappingsForMsSql();
            }
            else if (dialect.IndexOf("Sqlite", StringComparison.OrdinalIgnoreCase) > -1) {
                cfg.AddIdentityMappingsForSqlite();
            }
            else {
                throw new NotSupportedException(
                    "Only support PostgreSQL, MsSql, MySQL, Sqlite, your dialect must contain one of these words!"
                );
            }
            return cfg;
        }

        public Configuration AddIdentityMappingsForPostgres() {
            var mapping = ConfigurationHelper.GetIdentityMappingForPostgreSql();
            cfg.AddXml(mapping.AsString());
            return cfg;
        }

        public Configuration AddIdentityMappingsForMsSql() {
            var mapping = ConfigurationHelper.GetIdentityMappingForMsSql();
            cfg.AddXml(mapping.AsString());
            return cfg;
        }

        public Configuration AddIdentityMappingsForMySql() {
            var mapping = ConfigurationHelper.GetIdentityMappingForMySql();
            cfg.AddXml(mapping.AsString());
            return cfg;
        }

        public Configuration AddIdentityMappingsForSqlite() {
            var mapping = ConfigurationHelper.GetIdentityMappingForSqlite();
            cfg.AddXml(mapping.AsString());
            return cfg;
        }

    }

}
