using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using NHibernate.AspNetCore.Identity;
using NHibernate.NetCore;
using NHibernate;
using NHibernate.Linq;
using NHIdentityUser = NHibernate.AspNetCore.Identity.IdentityUser;
using NHIdentityRole = NHibernate.AspNetCore.Identity.IdentityRole;

namespace UnitTest.Identity;

[TestFixture]
public class UserStoreTest : BaseTest, IDisposable {

    private readonly UserStore<NHIdentityUser, NHIdentityRole> store;
    private readonly ISessionFactory sessionFactory;

    public UserStoreTest() {
        var builder = new LoggingBuilder();
        var loggerFactory = builder.BuildLoggerFactory();
        loggerFactory.UseAsHibernateLoggerFactory();
        var cfg = ConfigNHibernate();
        cfg.AddIdentityMappings();
        AddXmlMapping(cfg);
        sessionFactory = cfg.BuildSessionFactory();
        store = new UserStore<NHIdentityUser, NHIdentityRole>(
            sessionFactory.OpenSession(),
            new IdentityErrorDescriber()
        );
    }

    public void Dispose() {
        store.Dispose();
        sessionFactory.Dispose();
    }

    [Test]
    public async Task _01_CanQueryAllUsers() {
        var users = await store.Users.ToListAsync();
        That(users, Is.Not.Null);
        That(users.Count, Is.GreaterThan(0));
    }

    [Test]
    public async Task _02_CanDoCurd() {
        var user = new NHIdentityUser {
            UserName = "Beginor",
            NormalizedUserName = "BEGINOR",
            Email = "beginor@qq.com",
            PhoneNumber = "02000000000",
            PhoneNumberConfirmed = true,
            LockoutEnabled = false,
            LockoutEnd = null,
            AccessFailedCount = 0,
            NormalizedEmail = "BEGINOR@QQ.COM",
            PasswordHash = null,
            SecurityStamp = null
        };
        var result = await store.CreateAsync(user);
        That(result.Succeeded);
        var id = user.Id;
        That(id, Is.Not.Empty);
        That(user.ConcurrencyStamp, Is.Not.Empty);

        user.LockoutEnabled = true;
        user.LockoutEnd = DateTimeOffset.UtcNow.AddMinutes(20);
        result = await store.UpdateAsync(user);
        That(result.Succeeded);

        var lockouts = await store.Users
            .Where(u => u.LockoutEnabled)
            .CountAsync();
        That(lockouts > 0);

        user = await store.FindByEmailAsync(user.NormalizedEmail);
        That(user, Is.Not.Null);
        That(user!.Id == id);

        user = await store.FindByNameAsync(user.NormalizedUserName!);
        That(user, Is.Not.Null);
        That(user!.Id == id);

        user = await store.FindByIdAsync(id);
        That(user!.Id == id);

        var claim = new Claim("Test", Guid.NewGuid().ToString("N"));
        await store.AddClaimsAsync(user, new [] { claim });
        var claims = await store.GetClaimsAsync(user);
        That(claims.Count > 0);

        var users = await store.GetUsersForClaimAsync(claim);
        That(users, Is.Not.Empty);

        await store.RemoveClaimsAsync(user, claims);

        var loginInfo = new Microsoft.AspNetCore.Identity.UserLoginInfo(
            "test",
            Guid.NewGuid().ToString("N"),
            "Test"
        );
        await store.AddLoginAsync(user, loginInfo);
        await store.SetTokenAsync(
            user,
            loginInfo.LoginProvider,
            loginInfo.ProviderDisplayName!,
            loginInfo.ProviderKey,
            CancellationToken.None
        );

        await store.RemoveTokenAsync(
            user,
            loginInfo.LoginProvider,
            loginInfo.ProviderDisplayName!,
            CancellationToken.None
        );

        await store.RemoveLoginAsync(
            user,
            loginInfo.LoginProvider,
            loginInfo.ProviderKey
        );

        result = await store.DeleteAsync(user);
        That(result.Succeeded);
    }

    [Test]
    public async Task _03_CanGetRolesForUser() {
        using var session = sessionFactory.OpenSession();
        var user = new NHIdentityUser { Id = "1579928865223010012" };
        // IsNotNull(user);
        // var userId = user.Id;
        // var query = from userRole in session.Query<IdentityUserRole>()
        //     join role in session.Query<NHIdentityRole>() on userRole.RoleId equals role.Id
        //     where userRole.UserId == userId
        //     select role.Name;
        // var roles = await query.ToListAsync(CancellationToken.None);
        var roles = await store.GetRolesAsync(user);
        Console.WriteLine(roles.Count);
    }
}
