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

        private UserManager<AppUser> userMgr;
        RoleManager<AppRole> roleMgr;

        public UsersController(
            UserManager<AppUser> userMgr,
            RoleManager<AppRole> roleMgr
        ) {
            this.userMgr = userMgr;
            this.roleMgr = roleMgr;
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                userMgr = null;
                roleMgr = null;
            }
        }

        [HttpGet("")]
        public ActionResult<IList<AppUser>> GetAll() {
            var users = userMgr.Users.ToList();
            return (users);
        }

        [HttpPost]
        public ActionResult<AppUser> Create() {
            return null;
        }

    }

}
