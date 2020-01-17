using System;
using System.Reflection;
using NHibernate.Cfg;
using NHibernate.Mapping;
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
            var mapper = new ModelMapper();
            mapper.AddMapping<IdentityRoleMappingPostgreSql>();
            mapper.AddMapping<IdentityRoleClaimMappingPostgreSql>();
            mapper.AddMapping<IdentityUserMappingPostgreSql>();
            mapper.AddMapping<IdentityUserClaimMappingPostgreSql>();
            mapper.AddMapping<IdentityUserLoginMappingPostgreSql>();
            mapper.AddMapping<IdentityUserRoleMappingPostgreSql>();
            mapper.AddMapping<IdentityUserTokenMappingPostgreSql>();
            var mapping = mapper.CompileMappingForAllExplicitlyAddedEntities();
            cfg.AddMapping(mapping);
            return cfg;
        }

        public static Configuration AddIdentityMappingsForMsSql(
            this Configuration cfg
        ) {
            var mapper = new ModelMapper();
            mapper.AddMapping<IdentityRoleMappingMsSql>();
            mapper.AddMapping<IdentityRoleClaimMappingMsSql>();
            mapper.AddMapping<IdentityUserMappingMsSql>();
            mapper.AddMapping<IdentityUserClaimMappingMsSql>();
            mapper.AddMapping<IdentityUserLoginMappingMsSql>();
            mapper.AddMapping<IdentityUserRoleMappingMsSql>();
            mapper.AddMapping<IdentityUserTokenMappingMsSql>();
            var mapping = mapper.CompileMappingForAllExplicitlyAddedEntities();
            cfg.AddMapping(mapping);
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
