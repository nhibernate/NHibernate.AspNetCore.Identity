using NHibernate;
using NHibernate.Mapping.ByCode.Conformist;
using NHIdentityRole = NHibernate.AspNetCore.Identity.IdentityRole;

namespace WebTest.Entities {

    public class AppRoleMapping : JoinedSubclassMapping<AppRole> {

        public AppRoleMapping() {
            ExplicitDeclarationsHolder
                .AddAsRootEntity(typeof(NHIdentityRole));
            Extends(typeof(NHIdentityRole));
            Schema("public");
            Table("app_roles");
            Key(k => k.Column("id"));
            Property(
                p => p.Description,
                maping => {
                    maping.Column("description");
                    maping.Type(NHibernateUtil.String);
                    maping.Length(256);
                }
            );
        }

    }

}
