using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.AspNetCore.Identity;
using NHibernate.NetCore;
using NHibernate.Tool.hbm2ddl;

namespace UnitTest.Identity {

    [TestFixture]
    public abstract class ConfigTest : IDisposable {

        private ISessionFactory sessionFactory;
        private Configuration cfg;

        [OneTimeSetUp]
        public void OneTimeSetUp() {
            var builder = new LoggingBuilder();
            var loggerFactory = builder.BuildLoggerFactory();
            loggerFactory.UseAsHibernateLoggerFactory();
            var config = new Configuration();
            ConfigNHibernate(config);
            sessionFactory = config.BuildSessionFactory();
            cfg = config;
        }

        protected abstract void ConfigNHibernate(Configuration cfg);

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

    }

    [TestFixture]
    public class PgConfigTest : ConfigTest {

        protected override void ConfigNHibernate(Configuration cfg) {
            var file = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "hibernate.pg.config"
            );
            cfg.Configure(file);
            cfg.AddIdentityMappingsForPostgres();
        }

    }

    [TestFixture]
    public class MsSqlConfigTest : ConfigTest {

        protected override void ConfigNHibernate(Configuration cfg) {
            var file = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "hibernate.mssql.config"
            );
            cfg.Configure(file);
            cfg.AddIdentityMappingsForSqlServer();
        }

    }

}
