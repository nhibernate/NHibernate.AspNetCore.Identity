using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NHibernate;
using NHibernate.AspNetCore.Identity;
using WebTest.Entities;

namespace WebTest.Controllers {

    [Route("api/[controller]")]
    public class UsersController : Controller {

        private UserManager<ApplicationUser> manager;

        public UsersController(UserManager<ApplicationUser> session) {
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
