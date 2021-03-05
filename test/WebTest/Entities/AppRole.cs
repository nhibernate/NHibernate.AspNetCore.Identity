using NHibernate.Mapping.Attributes;
using NHIdentityRole = NHibernate.AspNetCore.Identity.IdentityRole;

namespace WebTest.Entities {

    [JoinedSubclass(0, Schema = "public", Table = "app_roles", ExtendsType = typeof(NHIdentityRole))]
    [Key(1, Column = "id")]
    public class AppRole : NHIdentityRole {

        [Property(Column = "description", Type = "string", Length = 256, NotNull = false)]
        public virtual string Description { get; set; }

    }

}
