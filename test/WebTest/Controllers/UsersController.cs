using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NHIdentityUser = NHibernate.AspNetCore.Identity.IdentityUser;

namespace WebTest.Controllers;

[Route("api/[controller]")]
public class UsersController : Controller {

    private readonly UserManager<NHIdentityUser> userMgr;

    public UsersController(
        UserManager<NHIdentityUser> userMgr
    ) {
        this.userMgr = userMgr ?? throw new ArgumentNullException(nameof(userMgr));
    }

    protected override void Dispose(bool disposing) {
        if (disposing) {
            // dispose managed resource here.
        }
    }

    [HttpGet("")]
    public ActionResult<IList<NHIdentityUser>> GetAll() {
        var users = userMgr.Users.ToList();
        return users;
    }

    [HttpPost]
    public ActionResult Create() {
        var provider = HttpContext.RequestServices;
        var session = provider.GetService<NHibernate.ISession>();
        if (session == null) {
            return StatusCode(StatusCodes.Status500InternalServerError, "Can not get sql session");
        }
        var user = new NHIdentityUser {
            UserName = "newyork_user",
            Email = "newyork_user@newyork.city",
            EmailConfirmed = true,
            PhoneNumber = "12300000000",
            PhoneNumberConfirmed = true
        };
        var task = userMgr.CreateAsync(user);
        task.Wait();
        var result = task.Result;
        return Ok(result);
    }

    [HttpGet("batch-create")]
    public async Task<ActionResult> BatchCreate() {
        var provider = HttpContext.RequestServices;
        using var scope = provider.CreateScope();
        var session = provider.GetService<NHibernate.ISession>();
        if (session == null) {
            return StatusCode(StatusCodes.Status500InternalServerError, "Can not get sql connection");
        }
        var manager = provider.GetService<UserManager<NHIdentityUser>>();
        if (manager == null) {
            return StatusCode(StatusCodes.Status500InternalServerError, "Can not get user manager");
        }
        using var tx = session.BeginTransaction();
        try {
            var user1 = new NHIdentityUser {
                UserName = "user1",
                Email = "user1@nonexit.com"
            };
            await manager.CreateAsync(user1);
            var user2 = new NHIdentityUser {
                UserName = "user2",
                Email = "user2@nonexit.com"
            };
            await manager.CreateAsync(user2);
            await tx.CommitAsync();
            return Ok();
        }
        catch (Exception ex) {
            await tx.RollbackAsync();
            Console.WriteLine(ex);
            return StatusCode(500);
        }
    }

}
