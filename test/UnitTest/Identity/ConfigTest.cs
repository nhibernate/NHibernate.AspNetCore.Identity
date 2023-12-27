using NHibernate;
using NHibernate.Cfg;
using NHibernate.AspNetCore.Identity;
using NHibernate.NetCore;
using NHibernate.Tool.hbm2ddl;
using WebTest.Entities;

namespace UnitTest.Identity;

[TestFixture]
public class ConfigTest : BaseTest, IDisposable {

    private ISessionFactory? sessionFactory;
    private Configuration? cfg;

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
        sessionFactory?.Dispose();
    }

    private ISessionFactory GetSessionFactory() {
        if (sessionFactory == null) {
            throw new Exception("sessionFactory is null!");
        }
        return sessionFactory;
    }

    [Test]
    public void _00_CanDoExport() {
        var export = new SchemaExport(cfg);
        export.Execute(true, false, false);
    }

    [Test]
    public void _01_CanQueryUsers() {
        using var session = GetSessionFactory().OpenSession();
        var query = session.Query<IdentityUser>();
        var count = query.ToList().Count;
        That(count, Is.GreaterThanOrEqualTo(0));
    }

    [Test]
    public void _02_CanQueryRoles() {
        using var session = GetSessionFactory().OpenSession();
        var query = session.Query<IdentityRole>();
        var count = query.ToList().Count;
        That(count, Is.GreaterThanOrEqualTo(0));
    }

    [Test]
    public void _03_CanQueryRoleClaims() {
        using var session = GetSessionFactory().OpenSession();
        var query = session.Query<IdentityRoleClaim>();
        var count = query.ToList().Count;
        That(count, Is.GreaterThanOrEqualTo(0));
    }

    [Test]
    public void _04_CanQueryUserClaims() {
        using var session = GetSessionFactory().OpenSession();
        var query = session.Query<IdentityUserClaim>();
        var count = query.ToList().Count;
        That(count, Is.GreaterThanOrEqualTo(0));
    }

    [Test]
    public void _05_CanQueryUserLogins() {
        using var session = GetSessionFactory().OpenSession();
        var query = session.Query<IdentityUserLogin>();
        var count = query.ToList().Count;
        That(count, Is.GreaterThanOrEqualTo(0));
    }

    [Test]
    public void _06_CanQueryUserTokens() {
        using var session = GetSessionFactory().OpenSession();
        var query = session.Query<IdentityUserToken>();
        var count = query.ToList().Count;
        That(count, Is.GreaterThanOrEqualTo(0));
    }

    [Test]
    public void _07_CanQueryUserRoles() {
        using var session = GetSessionFactory().OpenSession();
        var query = session.Query<IdentityUserRole>();
        var count = query.ToList().Count;
        That(count, Is.GreaterThanOrEqualTo(0));
    }

    [Test]
    public void _08_CanGetFullName() {
        var type = typeof(IdentityRole);
        That(type, Is.Not.Null);
    }

    [Test]
    public void _09_CanQueryUserWithCity() {
        using var session = GetSessionFactory().OpenSession();
        var user = session.Query<AppUser>()
            .First();
        That(user, Is.Not.Null);
        That(user?.City?.Name, Is.Not.Null);
    }

}
