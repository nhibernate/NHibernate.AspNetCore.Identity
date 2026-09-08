using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using IdentityRole = NHibernate.AspNetCore.Identity.IdentityRole;

namespace WebTest.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RolesController : ControllerBase {

    private readonly RoleManager<IdentityRole> manager;

    public RolesController(RoleManager<IdentityRole> manager) {
        this.manager = manager ?? throw new ArgumentNullException(nameof(manager));
    }

    [Route("")]
    public async Task<ActionResult<IList<IdentityRole>>> GetAll() {
        if (!await manager.RoleExistsAsync("TestRole")) {
            var role = new IdentityRole {
                Name = "TestRole"
            };
            var result = await manager.CreateAsync(role);
            if (!result.Succeeded) {
                return StatusCode(500, result.Errors);
            }
        }
        return manager.Roles.ToList();
    }

}
