using NHibernate.Mapping.ByCode.Conformist;

namespace NHibernate.AspNetCore.Identity.Mappings {

    public class IdentityUserRoleMappingPostgreSql : ClassMapping<IdentityUserRole> {

        public IdentityUserRoleMappingPostgreSql() {
            Schema("public");
            Table("aspnet_user_roles");
            ComposedId(id => {
                id.Property(e => e.UserId, prop => {
                    prop.Column("user_id");
                    prop.Type(NHibernateUtil.String);
                    prop.Length(32);
                });
                id.Property(e => e.RoleId, prop => {
                    prop.Column("role_id");
                    prop.Type(NHibernateUtil.String);
                    prop.Length(32);
                });
            });
        }

    }

    public class IdentityUserRoleMappingMsSql : ClassMapping<IdentityUserRole> {

        public IdentityUserRoleMappingMsSql() {
            Schema("dbo");
            Table("AspNetUserRoles");
            ComposedId(id => {
                id.Property(e => e.UserId, prop => {
                    prop.Column("UserId");
                    prop.Type(NHibernateUtil.String);
                    prop.Length(32);
                });
                id.Property(e => e.RoleId, prop => {
                    prop.Column("RoleId");
                    prop.Type(NHibernateUtil.String);
                    prop.Length(32);
                });
            });
        }

    }

    public class IdentityUserRoleMappingMySql : ClassMapping<IdentityUserRole> {

        public IdentityUserRoleMappingMySql() {
            Table("aspnet_user_roles");
            ComposedId(id => {
                id.Property(e => e.UserId, prop => {
                    prop.Column("user_id");
                    prop.Type(NHibernateUtil.String);
                    prop.Length(32);
                });
                id.Property(e => e.RoleId, prop => {
                    prop.Column("role_id");
                    prop.Type(NHibernateUtil.String);
                    prop.Length(32);
                });
            });
        }

    }

    public class IdentityUserRoleMappingSqlite : ClassMapping<IdentityUserRole> {

        public IdentityUserRoleMappingSqlite() {
            Table("aspnet_user_roles");
            ComposedId(id => {
                id.Property(e => e.UserId, prop => {
                    prop.Column("user_id");
                    prop.Type(NHibernateUtil.String);
                    prop.Length(32);
                });
                id.Property(e => e.RoleId, prop => {
                    prop.Column("role_id");
                    prop.Type(NHibernateUtil.String);
                    prop.Length(32);
                });
            });
        }

    }

}
