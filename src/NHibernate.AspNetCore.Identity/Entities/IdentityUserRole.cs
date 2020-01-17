using Microsoft.AspNetCore.Identity;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace NHibernate.AspNetCore.Identity {

    public class IdentityUserRole : IdentityUserRole<string> {

        protected bool Equals(IdentityUserRole other) {
            return RoleId == other.RoleId
                && UserId == other.UserId;
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
            return Equals((IdentityUserRole)obj);
        }

        public override int GetHashCode() {
            unchecked {
                var hashCode = 0;
                hashCode = RoleId.GetHashCode();
                hashCode = (hashCode * 397) ^ UserId.GetHashCode();
                return hashCode;
            }
        }

    }

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

}
