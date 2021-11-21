using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using NHibernate;
using WebTest.Entities;

namespace WebTest.Controllers;

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
    public ActionResult Create() {
        var provider = HttpContext.RequestServices;
        var session = provider.GetService<NHibernate.ISession>();
        var newyork = session.Query<City>()
            .First(c => c.Name == "new york");
        var user = new AppUser() {
            UserName = "newyork_user",
            Email = "newyork_user@newyork.city",
            City = newyork
        };
        var task = userMgr.CreateAsync(user);
        task.Wait();
        var result = task.Result;
        return Ok(user);
    }

    [HttpGet("batch-create")]
    public async Task<ActionResult> BatchCreate() {
        var provider = HttpContext.RequestServices;
        using var scope = provider.CreateScope();
        var session = provider.GetService<NHibernate.ISession>();
        var manager = provider.GetService<UserManager<AppUser>>();
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
