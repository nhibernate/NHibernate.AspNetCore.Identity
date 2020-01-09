using Microsoft.AspNetCore.Identity;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace NHibernate.AspNetCore.Identity {

    public class IdentityUserToken : IdentityUserToken<string> {

        protected bool Equals(IdentityUserToken other) {
            return UserId == other.UserId
                && LoginProvider == other.LoginProvider
                && Name == other.Name;
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
            return Equals((IdentityUserToken)obj);
        }

        public override int GetHashCode() {
            unchecked {
                var hashCode = 0;
                hashCode = UserId.GetHashCode();
                hashCode = (hashCode * 397) ^ LoginProvider.GetHashCode();
                hashCode = (hashCode * 397) ^ Name.GetHashCode();
                return hashCode;
            }
        }
    }

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

}
