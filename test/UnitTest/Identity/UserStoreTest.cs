using System;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using NUnit.Framework;
using NHibernate.Cfg;
using NHibernate.AspNetCore.Identity;
using NHibernate.NetCore;
using NHibernate;
using NHibernate.Linq;
using WebTest.Entities;
using NHIdentityUser = NHibernate.AspNetCore.Identity.IdentityUser;
using NHIdentityRole = NHibernate.AspNetCore.Identity.IdentityRole;

namespace UnitTest.Identity {

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
            store?.Dispose();
            sessionFactory?.Dispose();
        }

        [Test]
        public async Task _01_CanQueryAllUsers() {
            var users = await store.Users.ToListAsync();
            Assert.NotNull(users);
            Assert.True(users.Count >= 0);
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
            Assert.True(result.Succeeded);
            var id = user.Id;
            Assert.IsNotEmpty(id);
            Assert.IsNotEmpty(user.ConcurrencyStamp);

            user.LockoutEnabled = true;
            user.LockoutEnd = DateTimeOffset.UtcNow.AddMinutes(20);
            result = await store.UpdateAsync(user);
            Assert.True(result.Succeeded);

            var lockouts = await store.Users
                .Where(u => u.LockoutEnabled)
                .CountAsync();
            Assert.True(lockouts > 0);

            user = await store.FindByEmailAsync(user.NormalizedEmail);
            Assert.True(user.Id == id);

            user = await store.FindByNameAsync(user.NormalizedUserName);
            Assert.True(user.Id == id);

            user = await store.FindByIdAsync(id);
            Assert.True(user.Id == id);

            var claim = new Claim("Test", Guid.NewGuid().ToString("N"));
            await store.AddClaimsAsync(user, new [] { claim });
            var claims = await store.GetClaimsAsync(user);
            Assert.True(claims.Count > 0);

            var users = await store.GetUsersForClaimAsync(claim);
            Assert.IsNotEmpty(users);

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
                loginInfo.ProviderDisplayName,
                loginInfo.ProviderKey,
                CancellationToken.None
            );

            await store.RemoveTokenAsync(
                user,
                loginInfo.LoginProvider,
                loginInfo.ProviderDisplayName,
                CancellationToken.None
            );

            await store.RemoveLoginAsync(
                user,
                loginInfo.LoginProvider,
                loginInfo.ProviderKey
            );

            result = await store.DeleteAsync(user);
            Assert.True(result.Succeeded);
        }

        [Test]
        public async Task _03_CanGetRolesForUser() {
            using var session = sessionFactory.OpenSession();
            var user = new NHIdentityUser { Id = "1579928865223010012" };
            // Assert.IsNotNull(user);
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

}
