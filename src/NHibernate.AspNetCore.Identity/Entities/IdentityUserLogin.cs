using Microsoft.AspNetCore.Identity;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace NHibernate.AspNetCore.Identity {

    public class IdentityUserLogin : IdentityUserLogin<string> {

        protected bool Equals(IdentityUserLogin other) {
            return LoginProvider == other.LoginProvider
                && ProviderKey == other.ProviderKey;
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) {
                return false;
            }
            if (ReferenceEquals(this, obj)) {
                return true;
            }
            if (obj.GetType() != GetType()) {
                return false;
            }
            return Equals((IdentityUserLogin)obj);
        }

        public override int GetHashCode() {
            unchecked {
                var hashCode = 0;
                hashCode = LoginProvider.GetHashCode();
                hashCode = (hashCode * 397) ^ ProviderKey.GetHashCode();
                return hashCode;
            }
        }

    }

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

}
