using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebTest.Entities;

namespace WebTest.Controllers;

[Route("api/[controller]")]
public class UsersController : Controller {

    private readonly UserManager<AppUser> userMgr;

    public UsersController(
        UserManager<AppUser> userMgr
    ) {
        this.userMgr = userMgr ?? throw new ArgumentNullException(nameof(userMgr));
    }

    protected override void Dispose(bool disposing) {
        if (disposing) {
            // dispose managed resource here.
        }
    }

    [HttpGet("")]
    public ActionResult<IList<AppUser>> GetAll() {
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
        var newYork = session.Query<City>()
            .First(c => c.Name == "new york");
        var user = new AppUser() {
            UserName = "newyork_user",
            Email = "newyork_user@newyork.city",
            City = newYork
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
        var manager = provider.GetService<UserManager<AppUser>>();
        if (manager == null) {
            return StatusCode(StatusCodes.Status500InternalServerError, "Can not get user manager");
        }
        using var tx = session.BeginTransaction();
        try {
            var user1 = new AppUser {
                UserName = "user1",
                Email = "user1@nonexit.com"
            };
            await manager.CreateAsync(user1);
            var user2 = new AppUser {
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
