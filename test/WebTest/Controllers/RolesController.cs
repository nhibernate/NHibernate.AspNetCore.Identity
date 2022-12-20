using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebTest.Entities;

namespace WebTest.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RolesController : ControllerBase {

    private readonly RoleManager<AppRole> manager;

    public RolesController(RoleManager<AppRole> manager) {
        this.manager = manager ?? throw new ArgumentNullException(nameof(manager));
    }

    [Route("")]
    public async Task<ActionResult<IList<AppRole>>> GetAll() {
        if (!await manager.RoleExistsAsync("TestRole")) {
            var role = new AppRole {
                Name = "TestRole",
                Description = "Test Role"
            };
            var result = await manager.CreateAsync(role);
        }
        return manager.Roles.ToList();
    }

}
