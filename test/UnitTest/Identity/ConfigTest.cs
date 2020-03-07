using System;
using System.Linq;
using NUnit.Framework;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.AspNetCore.Identity;
using NHibernate.NetCore;
using NHibernate.Tool.hbm2ddl;
using WebTest.Entities;

namespace UnitTest.Identity {

    [TestFixture]
    public class ConfigTest : BaseTest, IDisposable {

        private ISessionFactory sessionFactory;
        private Configuration cfg;

        [OneTimeSetUp]
        public void OneTimeSetUp() {
            var builder = new LoggingBuilder();
            var loggerFactory = builder.BuildLoggerFactory();
            loggerFactory.UseAsHibernateLoggerFactory();
            cfg = ConfigNHibernate();
            cfg.AddIdentityMappings();
            AddAttributesMapping(cfg);
            sessionFactory = cfg.BuildSessionFactory();
        }

        public void Dispose() {
            sessionFactory.Dispose();
        }

        [Test]
        public void _00_CanDoExport() {
            var export = new SchemaExport(cfg);
            export.Execute(true, false, false);
        }

        [Test]
        public void _01_CanQueryUsers() {
            using (var session = sessionFactory.OpenSession()) {
                var query = session.Query<IdentityUser>();
                var count = query.ToList().Count;
                Assert.GreaterOrEqual(count, 0);
            }
        }

        [Test]
        public void _02_CanQueryRoles() {
            using (var session = sessionFactory.OpenSession()) {
                var query = session.Query<IdentityRole>();
                var count = query.ToList().Count;
                Assert.GreaterOrEqual(count, 0);
            }
        }

        [Test]
        public void _03_CanQueryRoleClaims() {
            using (var session = sessionFactory.OpenSession()) {
                var query = session.Query<IdentityRoleClaim>();
                var count = query.ToList().Count;
                Assert.GreaterOrEqual(count, 0);
            }
        }

        [Test]
        public void _04_CanQueryUserClaims() {
            using (var session = sessionFactory.OpenSession()) {
                var query = session.Query<IdentityUserClaim>();
                var count = query.ToList().Count;
                Assert.GreaterOrEqual(count, 0);
            }
        }

        [Test]
        public void _05_CanQueryUserLogins() {
            using (var session = sessionFactory.OpenSession()) {
                var query = session.Query<IdentityUserLogin>();
                var count = query.ToList().Count;
                Assert.GreaterOrEqual(count, 0);
            }
        }

        [Test]
        public void _06_CanQueryUserTokens() {
            using (var session = sessionFactory.OpenSession()) {
                var query = session.Query<IdentityUserToken>();
                var count = query.ToList().Count;
                Assert.GreaterOrEqual(count, 0);
            }
        }

        [Test]
        public void _07_CanQueryUserRoles() {
            using (var session = sessionFactory.OpenSession()) {
                var query = session.Query<IdentityUserRole>();
                var count = query.ToList().Count;
                Assert.GreaterOrEqual(count, 0);
            }
        }

        [Test]
        public void _08_CanGetFullName() {
            var type = typeof(IdentityRole);
            Assert.IsNotNull(type);
        }

        [Test]
        public void _09_CanQueryUserWithCity() {
            using (var session = sessionFactory.OpenSession()) {
                var user = session.Query<AppUser>()
                    .First();
                Assert.NotNull(user);
                Assert.IsNotNull(user.City.Name);
            }
        }

    }

}
