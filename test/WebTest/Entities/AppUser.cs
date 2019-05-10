using System;
using NHIdentityUser = NHibernate.AspNetCore.Identity.IdentityUser;

namespace WebTest.Entities {

    public class AppUser : NHIdentityUser {

        public virtual DateTime CreateTime { get; set; }

        public virtual DateTime? LastLogin { get; set; }

        public virtual int LoginCount { get; set; }

    }

}
