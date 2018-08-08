using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NHibernate;
using NHibernate.AspNetCore.Identity;
using NHIdentityUser = NHibernate.AspNetCore.Identity.IdentityUser;

namespace WebTest.Controllers {

    [Route("api/[controller]")]
    public class UsersController : Controller {

        private UserManager<NHIdentityUser> manager;

        public UsersController(UserManager<NHIdentityUser> session) {
            this.manager = session;
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                manager = null;
            }
        }

        [HttpGet("")]
        public ActionResult GetAll() {
            var users = manager.Users.ToList();
            return Ok(users);
        }

    }

}
