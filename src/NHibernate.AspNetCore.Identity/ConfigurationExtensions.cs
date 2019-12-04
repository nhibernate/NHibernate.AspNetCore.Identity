using System;
using System.Reflection;
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
                cfg.AddIdentityMappingsForSqlServer();
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
            var asm = typeof(IdentityUser).Assembly;
            var stream = asm.GetManifestResourceStream(
                "NHibernate.AspNetCore.Identity.Mappings.AspNetCoreIdentity.pg.xml"
            );
            cfg.AddInputStream(stream);
            return cfg;
        }

        public static Configuration AddIdentityMappingsForSqlServer(
            this Configuration cfg
        ) {
            var asm = typeof(IdentityUser).Assembly;
            var stream = asm.GetManifestResourceStream(
                "NHibernate.AspNetCore.Identity.Mappings.AspNetCoreIdentity.mssql.xml"
            );
            cfg.AddInputStream(stream);
            return cfg;
        }

        public static Configuration AddIdentityMappingsForMySql(
            this Configuration cfg
        ) {
            var asm = typeof(IdentityUser).Assembly;
            var stream = asm.GetManifestResourceStream(
                "NHibernate.AspNetCore.Identity.Mappings.AspNetCoreIdentity.mysql.xml"
            );
            cfg.AddInputStream(stream);
            return cfg;
        }

    }

}
