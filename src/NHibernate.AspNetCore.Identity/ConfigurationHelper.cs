using NHibernate.AspNetCore.Identity.Mappings;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Mapping.ByCode;

namespace NHibernate.AspNetCore.Identity {

    public static class ConfigurationHelper {

        public static HbmMapping GetIdentityMappingForPostgreSql() {
            var mapper = new ModelMapper();
            mapper.AddMapping<IdentityRoleMappingPostgreSql>();
            mapper.AddMapping<IdentityRoleClaimMappingPostgreSql>();
            mapper.AddMapping<IdentityUserMappingPostgreSql>();
            mapper.AddMapping<IdentityUserClaimMappingPostgreSql>();
            mapper.AddMapping<IdentityUserLoginMappingPostgreSql>();
            mapper.AddMapping<IdentityUserRoleMappingPostgreSql>();
            mapper.AddMapping<IdentityUserTokenMappingPostgreSql>();
            return mapper.CompileMappingForAllExplicitlyAddedEntities();
        }

        public static HbmMapping GetIdentityMappingForMsSql() {
            var mapper = new ModelMapper();
            mapper.AddMapping<IdentityRoleMappingMsSql>();
            mapper.AddMapping<IdentityRoleClaimMappingMsSql>();
            mapper.AddMapping<IdentityUserMappingMsSql>();
            mapper.AddMapping<IdentityUserClaimMappingMsSql>();
            mapper.AddMapping<IdentityUserLoginMappingMsSql>();
            mapper.AddMapping<IdentityUserRoleMappingMsSql>();
            mapper.AddMapping<IdentityUserTokenMappingMsSql>();
            return mapper.CompileMappingForAllExplicitlyAddedEntities();
        }

        public static HbmMapping GetIdentityMappingForMySql() {
            var mapper = new ModelMapper();
            mapper.AddMapping<IdentityRoleMappingMySql>();
            mapper.AddMapping<IdentityRoleClaimMappingMySql>();
            mapper.AddMapping<IdentityUserMappingMySql>();
            mapper.AddMapping<IdentityUserClaimMappingMySql>();
            mapper.AddMapping<IdentityUserLoginMappingMySql>();
            mapper.AddMapping<IdentityUserRoleMappingMySql>();
            mapper.AddMapping<IdentityUserTokenMappingMySql>();
            return mapper.CompileMappingForAllExplicitlyAddedEntities();
        }

        public static HbmMapping GetIdentityMappingForSqlite() {
            var mapper = new ModelMapper();
            mapper.AddMapping<IdentityRoleMappingSqlite>();
            mapper.AddMapping<IdentityRoleClaimMappingSqlite>();
            mapper.AddMapping<IdentityUserMappingSqlite>();
            mapper.AddMapping<IdentityUserClaimMappingSqlite>();
            mapper.AddMapping<IdentityUserLoginMappingSqlite>();
            mapper.AddMapping<IdentityUserRoleMappingSqlite>();
            mapper.AddMapping<IdentityUserTokenMappingSqlite>();
            return mapper.CompileMappingForAllExplicitlyAddedEntities();
        }
    }

}
