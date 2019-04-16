using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<ActionResult<IList<ApplicationRole>>> GetAll() {
            if (!await manager.RoleExistsAsync("TestRole")) {
                var role = new ApplicationRole {
                    Name = "TestRole",
                    Description = "Test Role"
                };
                var result = await manager.CreateAsync(role);
            }
            return manager.Roles.ToList();
        }

    }

}
