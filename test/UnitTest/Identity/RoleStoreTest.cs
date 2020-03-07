using System;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using NUnit.Framework;
using NHibernate.Cfg;
using NHibernate.AspNetCore.Identity;
using NHibernate.NetCore;
using NHibernate.Linq;
using System.Threading;
using Microsoft.AspNetCore.Identity;
using NHIdentityRole = NHibernate.AspNetCore.Identity.IdentityRole;

namespace UnitTest.Identity {

    [TestFixture]
    public class RoleStoreTest : BaseTest, IDisposable {

        private RoleStore<NHIdentityRole> store;

        public RoleStoreTest() {
            var builder = new LoggingBuilder();
            var loggerFactory = builder.BuildLoggerFactory();
            loggerFactory.UseAsHibernateLoggerFactory();
            var cfg = ConfigNHibernate();
            cfg.AddIdentityMappings();
            AddXmlMapping(cfg);
            var sessionFactory = cfg.BuildSessionFactory();
            store = new RoleStore<NHIdentityRole>(
                sessionFactory.OpenSession(),
                new IdentityErrorDescriber()
            );
        }

        public void Dispose() {
            store?.Dispose();
        }

        [Test]
        public async Task _01_CanQueryAllRoles() {
            var roleList = await store.Roles.ToListAsync();
            Assert.NotNull(roleList);
            Assert.True(roleList.Count >= 0);
        }

        [Test]
        public async Task _02_CanDoCURD() {
            var role = new NHIdentityRole();
            role.Name = "Role 1";
            role.NormalizedName = role.Name.ToUpperInvariant();
            var result = await store.CreateAsync(role, CancellationToken.None);
            Assert.True(result.Succeeded);
            Assert.IsNotEmpty(role.Id);
            Assert.IsNotEmpty(role.ConcurrencyStamp);

            role.Name = "role 1 updated";
            role.NormalizedName = role.Name.ToUpperInvariant();
            result = await store.UpdateAsync(role, CancellationToken.None);
            Assert.True(result.Succeeded);

            role = await store.FindByIdAsync(role.Id, CancellationToken.None);
            Assert.AreEqual(role.Id, role.Id);
            Assert.AreEqual(role.Name, role.Name);

            var normalizedName = role.NormalizedName;
            role = await store.FindByNameAsync(normalizedName, CancellationToken.None);
            Assert.AreEqual(normalizedName, role.NormalizedName);

            var claim = new Claim("test", "test");

            await store.AddClaimAsync(role, claim, CancellationToken.None);

            var roleClaims = await store.GetClaimsAsync(role, CancellationToken.None);
            Assert.NotNull(roleClaims);
            Assert.True(roleClaims.Count > 0);

            await store.RemoveClaimAsync(role, claim, CancellationToken.None);

            roleClaims = await store.GetClaimsAsync(role, CancellationToken.None);
            Assert.NotNull(roleClaims);
            Assert.True(roleClaims.Count == 0);

            result = await store.DeleteAsync(role, CancellationToken.None);
            Assert.True(result.Succeeded);
        }

    }

}
