using NHIdentityUser = NHibernate.AspNetCore.Identity.IdentityUser;

namespace WebTest.Entities {

    public class ApplicationUser : NHIdentityUser {

        public virtual string CustomProperty { get; set; }

    }

}