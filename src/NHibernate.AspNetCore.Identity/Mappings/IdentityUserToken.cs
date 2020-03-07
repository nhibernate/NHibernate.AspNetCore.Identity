using NHibernate.Mapping.ByCode.Conformist;

namespace NHibernate.AspNetCore.Identity.Mappings {

    public class IdentityUserTokenMappingPostgreSql : ClassMapping<IdentityUserToken> {

        public IdentityUserTokenMappingPostgreSql() {
            Schema("public");
            Table("aspnet_user_tokens");
            ComposedId(id => {
                id.Property(e => e.UserId, prop => {
                    prop.Column("user_id");
                    prop.Type(NHibernateUtil.String);
                    prop.Length(32);
                });
                id.Property(e => e.LoginProvider, prop => {
                    prop.Column("login_provider");
                    prop.Type(NHibernateUtil.String);
                    prop.Length(32);
                });
                id.Property(e => e.Name, prop => {
                    prop.Column("name");
                    prop.Type(NHibernateUtil.String);
                    prop.Length(32);
                });
            });
            Property(e => e.Value, prop => {
                prop.Column("value");
                prop.Type(NHibernateUtil.String);
                prop.Length(256);
                prop.NotNullable(true);
            });
        }

    }

    public class IdentityUserTokenMappingMsSql : ClassMapping<IdentityUserToken> {

        public IdentityUserTokenMappingMsSql() {
            Schema("dbo");
            Table("AspNetUserTokens");
            ComposedId(id => {
                id.Property(e => e.UserId, prop => {
                    prop.Column("UserId");
                    prop.Type(NHibernateUtil.String);
                    prop.Length(32);
                });
                id.Property(e => e.LoginProvider, prop => {
                    prop.Column("LoginProvider");
                    prop.Type(NHibernateUtil.String);
                    prop.Length(32);
                });
                id.Property(e => e.Name, prop => {
                    prop.Column("Name");
                    prop.Type(NHibernateUtil.String);
                    prop.Length(32);
                });
            });
            Property(e => e.Value, prop => {
                prop.Column("Value");
                prop.Type(NHibernateUtil.String);
                prop.Length(256);
                prop.NotNullable(true);
            });
        }

    }

    public class IdentityUserTokenMappingMySql : ClassMapping<IdentityUserToken> {

        public IdentityUserTokenMappingMySql() {
            Table("aspnet_user_tokens");
            ComposedId(id => {
                id.Property(e => e.UserId, prop => {
                    prop.Column("user_id");
                    prop.Type(NHibernateUtil.String);
                    prop.Length(32);
                });
                id.Property(e => e.LoginProvider, prop => {
                    prop.Column("login_provider");
                    prop.Type(NHibernateUtil.String);
                    prop.Length(32);
                });
                id.Property(e => e.Name, prop => {
                    prop.Column("name");
                    prop.Type(NHibernateUtil.String);
                    prop.Length(32);
                });
            });
            Property(e => e.Value, prop => {
                prop.Column("value");
                prop.Type(NHibernateUtil.String);
                prop.Length(256);
                prop.NotNullable(true);
            });
        }

    }

    public class IdentityUserTokenMappingSqlite : ClassMapping<IdentityUserToken> {

        public IdentityUserTokenMappingSqlite() {
            Table("aspnet_user_tokens");
            ComposedId(id => {
                id.Property(e => e.UserId, prop => {
                    prop.Column("user_id");
                    prop.Type(NHibernateUtil.String);
                    prop.Length(32);
                });
                id.Property(e => e.LoginProvider, prop => {
                    prop.Column("login_provider");
                    prop.Type(NHibernateUtil.String);
                    prop.Length(32);
                });
                id.Property(e => e.Name, prop => {
                    prop.Column("name");
                    prop.Type(NHibernateUtil.String);
                    prop.Length(32);
                });
            });
            Property(e => e.Value, prop => {
                prop.Column("value");
                prop.Type(NHibernateUtil.String);
                prop.Length(256);
                prop.NotNullable(true);
            });
        }

    }

}
