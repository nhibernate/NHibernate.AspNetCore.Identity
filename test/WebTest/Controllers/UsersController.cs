using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NHibernate;
using NHibernate.AspNetCore.Identity;
using WebTest.Entities;

namespace WebTest.Controllers {

    [Route("api/[controller]")]
    public class UsersController : Controller {

        private UserManager<AppUser> manager;

        public UsersController(UserManager<AppUser> manager) {
            this.manager = manager;
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                manager = null;
            }
        }

        [HttpGet("")]
        public ActionResult<IList<AppUser>> GetAll() {
            var users = manager.Users.ToList();
            return (users);
        }

        [HttpPost]
        public ActionResult<AppUser> Create() {
            return null;
        }



    }

}
