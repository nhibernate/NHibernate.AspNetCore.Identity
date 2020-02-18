using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using NHIdentityUser = NHibernate.AspNetCore.Identity.IdentityUser;

namespace WebTest.Entities {

    public class AppUserMapping : JoinedSubclassMapping<AppUser> {

        public AppUserMapping() {
            Extends(typeof(NHIdentityUser));
            ExplicitDeclarationsHolder
                .AddAsRootEntity(typeof(NHIdentityUser));
            Schema("public");
            Table("app_users");
            Key(k => k.Column("id"));
            Property(
                e => e.CreateTime,
                mapping => {
                    mapping.Column("create_time");
                    mapping.Type(NHibernateUtil.DateTime);
                    mapping.NotNullable(true);
                    mapping.Generated(PropertyGeneration.Insert);
                    mapping.Update(false);
                    mapping.Insert(false);
                }
            );
            Property(
                e => e.LastLogin,
                mapping => {
                    mapping.Column("last_login");
                    mapping.Type(NHibernateUtil.DateTime);
                    mapping.NotNullable(false);
                }
            );
            Property(
                e => e.LoginCount,
                mapping => {
                    mapping.Column("login_count");
                    mapping.Type(NHibernateUtil.Int32);
                    mapping.NotNullable(true);
                }
            );
            ManyToOne(
                e => e.City,
                mapping => {
                    mapping.Column("city_id");
                    mapping.NotFound(NotFoundMode.Ignore);
                }
            );
        }

    }

}
