using NHibernate;
using NHibernate.AspNetCore.Identity;
using WebTest.Entities;

namespace UnitTest;

[TestFixture]
public class IdentityTest : BaseTest {

    [Test]
    public void _01_CanExtendByCodeWithByCode() {
        var cfg = ConfigNHibernate();
        cfg.AddIdentityMappings();
        AddByCodeMapping(cfg);
        cfg.BuildMappings();
        var sf = cfg.BuildSessionFactory();
        That(sf, Is.Not.Null);
        QueryUsers(sf);
    }

    [Test]
    public void _02_CanExtendByCodeWithXml() {
        var cfg = ConfigNHibernate();
        cfg.AddIdentityMappings();
        AddXmlMapping(cfg);
        cfg.BuildMappings();
        var sf = cfg.BuildSessionFactory();
        That(sf, Is.Not.Null);
        QueryUsers(sf);
    }

    [Test]
    public void _03_CanExtendByByCodeWithAttributes() {
        var cfg = ConfigNHibernate();
        cfg.AddIdentityMappings();
        AddAttributesMapping(cfg);
        cfg.BuildMappings();
        var sf = cfg.BuildSessionFactory();
        That(sf, Is.Not.Null);
        QueryUsers(sf);
    }

    [Test]
    public void _04_CanQueryCities() {
        var cfg = ConfigNHibernate();
        cfg.AddIdentityMappings();
        AddAttributesMapping(cfg);
        cfg.BuildMappings();
        var sf = cfg.BuildSessionFactory();
        That(sf, Is.Not.Null);
        using var session = sf.OpenSession();
        var cities = session.Query<City>().ToList();
        That(sf, Is.Not.Empty);
        foreach (var city in cities) {
            That(city.Id, Is.GreaterThan(0));
            That(city.Name, Is.Not.Null);
        }
    }

    [Test]
    public void _05_CanInsertDeleteCities() {
        var cfg = ConfigNHibernate();
        cfg.AddIdentityMappings();
        AddAttributesMapping(cfg);
        cfg.BuildMappings();
        var sf = cfg.BuildSessionFactory();
        That(sf, Is.Not.Null);
        using var session = sf.OpenSession();
        var tx = session.BeginTransaction();
        var city = new City {
            Name = "Test City"
        };
        session.Save(city);
        session.Flush();
        That(city.Id, Is.GreaterThan(0));
        tx.Rollback();
    }

    private void QueryUsers(ISessionFactory sf) {
        using var session = sf.OpenSession();
        var users = session.Query<AppUser>()
            .Where(u => u.LoginCount > 0)
            .ToList();
        That(users.Count, Is.GreaterThanOrEqualTo(0));
    }

}
