using System;
using Microsoft.AspNetCore.Mvc;
using NHibernate;
using NHibernate.AspNetCore.Identity;

namespace WebTest.Controllers {

    [Route("api/[controller]")]
    public class UsersController : Controller {

        private ISession session;

        public UsersController(ISession session) {
            this.session = session;
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                session = null;
            }
        }

        [HttpGet("")]
        public ActionResult GetAll() {
            var users = session.Query<IdentityUser>();
            return Ok(users);
        }

    }

}
