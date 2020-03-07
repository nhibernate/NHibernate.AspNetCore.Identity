using NHibernate.Mapping.ByCode.Conformist;

namespace NHibernate.AspNetCore.Identity.Mappings {

    public class IdentityUserLoginMappingPostgreSql : ClassMapping<IdentityUserLogin> {

        public IdentityUserLoginMappingPostgreSql() {
            Schema("public");
            Table("aspnet_user_logins");
            ComposedId(id => {
                id.Property(e => e.LoginProvider, prop => {
                    prop.Column("login_provider");
                    prop.Type(NHibernateUtil.String);
                    prop.Length(32);
                });
                id.Property(e => e.ProviderKey, prop => {
                    prop.Column("provider_key");
                    prop.Type(NHibernateUtil.String);
                    prop.Length(32);
                });
            });
            Property(e => e.ProviderDisplayName, prop => {
                prop.Column("provider_display_name");
                prop.Type(NHibernateUtil.String);
                prop.Length(32);
                prop.NotNullable(true);
            });
            Property(e => e.UserId, prop => {
                prop.Column("user_id");
                prop.Type(NHibernateUtil.String);
                prop.Length(32);
                prop.NotNullable(true);
            });
        }

    }

    public class IdentityUserLoginMappingMsSql : ClassMapping<IdentityUserLogin> {

        public IdentityUserLoginMappingMsSql() {
            Schema("dbo");
            Table("AspNetUserLogins");
            ComposedId(id => {
                id.Property(e => e.LoginProvider, prop => {
                    prop.Column("LoginProvider");
                    prop.Type(NHibernateUtil.String);
                    prop.Length(32);
                });
                id.Property(e => e.ProviderKey, prop => {
                    prop.Column("ProviderKey");
                    prop.Type(NHibernateUtil.String);
                    prop.Length(32);
                });
            });
            Property(e => e.ProviderDisplayName, prop => {
                prop.Column("ProviderDisplayName");
                prop.Type(NHibernateUtil.String);
                prop.Length(32);
                prop.NotNullable(true);
            });
            Property(e => e.UserId, prop => {
                prop.Column("UserId");
                prop.Type(NHibernateUtil.String);
                prop.Length(32);
                prop.NotNullable(true);
            });
        }

    }

    public class IdentityUserLoginMappingMySql : ClassMapping<IdentityUserLogin> {

        public IdentityUserLoginMappingMySql() {
            Table("aspnet_user_logins");
            ComposedId(id => {
                id.Property(e => e.LoginProvider, prop => {
                    prop.Column("login_provider");
                    prop.Type(NHibernateUtil.String);
                    prop.Length(32);
                });
                id.Property(e => e.ProviderKey, prop => {
                    prop.Column("provider_key");
                    prop.Type(NHibernateUtil.String);
                    prop.Length(32);
                });
            });
            Property(e => e.ProviderDisplayName, prop => {
                prop.Column("provider_display_name");
                prop.Type(NHibernateUtil.String);
                prop.Length(32);
                prop.NotNullable(true);
            });
            Property(e => e.UserId, prop => {
                prop.Column("user_id");
                prop.Type(NHibernateUtil.String);
                prop.Length(32);
                prop.NotNullable(true);
            });
        }

    }

    public class IdentityUserLoginMappingSqlite : ClassMapping<IdentityUserLogin> {

        public IdentityUserLoginMappingSqlite() {
            Table("aspnet_user_logins");
            ComposedId(id => {
                id.Property(e => e.LoginProvider, prop => {
                    prop.Column("login_provider");
                    prop.Type(NHibernateUtil.String);
                    prop.Length(32);
                });
                id.Property(e => e.ProviderKey, prop => {
                    prop.Column("provider_key");
                    prop.Type(NHibernateUtil.String);
                    prop.Length(32);
                });
            });
            Property(e => e.ProviderDisplayName, prop => {
                prop.Column("provider_display_name");
                prop.Type(NHibernateUtil.String);
                prop.Length(32);
                prop.NotNullable(true);
            });
            Property(e => e.UserId, prop => {
                prop.Column("user_id");
                prop.Type(NHibernateUtil.String);
                prop.Length(32);
                prop.NotNullable(true);
            });
        }

    }

}
