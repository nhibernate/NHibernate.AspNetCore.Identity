using System;
using System.IO;
using System.Linq;
using Xunit;
using Microsoft.Extensions.Logging;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.AspNetCore.Identity;
using NHibernate.NetCore;

namespace UnitTest.Identity {

    public class ConfigTest : IDisposable {

        private ISessionFactory sessionFactory;

        public ConfigTest() {
            var loggerFactory = new LoggerFactory();
            loggerFactory.AddConsole(
                minLevel: LogLevel.Error,
                includeScopes: false
            );
            loggerFactory.UseAsHibernateLoggerFactory();
            //
            var cfg = new Configuration();
            var file = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "hibernate.config"
            );
            cfg.Configure(file);
            cfg.AddIdentityMappingsForPostgres();
            sessionFactory = cfg.BuildSessionFactory();
        }

        public void Dispose() {
            sessionFactory.Dispose();
        }

        [Fact]
        public void _01_CanQueryUsers() {
            using (var session = sessionFactory.OpenSession()) {
                var query = session.Query<IdentityUser>();
                var count = query.ToList().Count;
                Assert.True(count >= 0);
            }
        }

        [Fact]
        public void _02_CanQueryRoles() {
            using (var session = sessionFactory.OpenSession()) {
                var query = session.Query<IdentityRole>();
                var count = query.ToList().Count;
                Assert.True(count >= 0);
            }
        }

        [Fact]
        public void _03_CanQueryRoleClaims() {
            using (var session = sessionFactory.OpenSession()) {
                var query = session.Query<IdentityRoleClaim>();
                var count = query.ToList().Count;
                Assert.True(count >= 0);
            }
        }

        [Fact]
        public void _04_CanQueryUserClaims() {
            using (var session = sessionFactory.OpenSession()) {
                var query = session.Query<IdentityUserClaim>();
                var count = query.ToList().Count;
                Assert.True(count >= 0);
            }
        }

        [Fact]
        public void _05_CanQueryUserLogins() {
            using (var session = sessionFactory.OpenSession()) {
                var query = session.Query<IdentityUserLogin>();
                var count = query.ToList().Count;
                Assert.True(count >= 0);
            }
        }

        [Fact]
        public void _06_CanQueryUserTokens() {
            using (var session = sessionFactory.OpenSession()) {
                var query = session.Query<IdentityUserToken>();
                var count = query.ToList().Count;
                Assert.True(count >= 0);
            }
        }

        [Fact]
        public void _07_CanQueryUserRoles() {
            using (var session = sessionFactory.OpenSession()) {
                var query = session.Query<IdentityUserRole>();
                var count = query.ToList().Count;
                Assert.True(count >= 0);
            }
        }

    }

}
