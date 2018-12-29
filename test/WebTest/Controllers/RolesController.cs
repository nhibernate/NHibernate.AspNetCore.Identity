using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebTest.Entities;

namespace WebTest.Controllers {

    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase {

        private RoleManager<ApplicationRole> manager;

        public RolesController(RoleManager<ApplicationRole> manager) {
            this.manager = manager;
        }

        [Route("")]
        public ActionResult<IList<ApplicationRole>> GetAll() {
            return manager.Roles.ToList();
        }

    }

}
