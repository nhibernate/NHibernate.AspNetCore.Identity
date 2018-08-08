using System;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Extensions.Logging;
using NHibernate.Cfg;
using NHibernate.AspNetCore.Identity;
using NHibernate.NetCore;
using NHibernate.Linq;
using System.Threading;

namespace UnitTest.Identity {

    public class RoleStoreTest : IDisposable {

        private RoleStore store;

        public RoleStoreTest() {
            var loggerFactory = new LoggerFactory();
            loggerFactory.AddConsole(
                minLevel: LogLevel.Error,
                includeScopes: false
            );
            loggerFactory.UseAsHibernateLoggerFactory();
            var cfg = new Configuration();
            var file = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "hibernate.config"
            );
            cfg.Configure(file);
            cfg.AddIdentityMappingsForPostgres();
            var sessionFactory = cfg.BuildSessionFactory();
            store = new RoleStore(sessionFactory.OpenSession());
        }

        public void Dispose() {
            store?.Dispose();
        }

        [Fact]
        public async Task _01_CanQueryAllRoles() {
            var roleList = await store.Roles.ToListAsync();
            Assert.NotNull(roleList);
            Assert.True(roleList.Count >= 0);
        }

        [Fact]
        public async Task _02_CanDoCURD() {
            var role = new IdentityRole();
            role.Name = "Role 1";
            role.NormalizedName = role.Name.ToUpperInvariant();
            var result = await store.CreateAsync(role, CancellationToken.None);
            Assert.True(result.Succeeded);
            Assert.NotEmpty(role.Id);
            Assert.NotEmpty(role.ConcurrencyStamp);

            role.Name = "role 1 updated";
            role.NormalizedName = role.Name.ToUpperInvariant();
            result = await store.UpdateAsync(role, CancellationToken.None);
            Assert.True(result.Succeeded);

            role = await store.FindByIdAsync(role.Id, CancellationToken.None);
            Assert.Equal(role.Id, role.Id);
            Assert.Equal(role.Name, role.Name);

            var normalizedName = role.NormalizedName;
            role = await store.FindByNameAsync(normalizedName, CancellationToken.None);
            Assert.Equal(normalizedName, role.NormalizedName);

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
