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
        IsNotNull(sf);
        QueryUsers(sf);
    }

    [Test]
    public void _02_CanExtendByCodeWithXml() {
        var cfg = ConfigNHibernate();
        cfg.AddIdentityMappings();
        AddXmlMapping(cfg);
        cfg.BuildMappings();
        var sf = cfg.BuildSessionFactory();
        IsNotNull(sf);
        QueryUsers(sf);
    }

    [Test]
    public void _03_CanExtendByByCodeWithAttributes() {
        var cfg = ConfigNHibernate();
        cfg.AddIdentityMappings();
        AddAttributesMapping(cfg);
        cfg.BuildMappings();
        var sf = cfg.BuildSessionFactory();
        IsNotNull(sf);
        QueryUsers(sf);
    }

    [Test]
    public void _04_CanQueryCities() {
        var cfg = ConfigNHibernate();
        cfg.AddIdentityMappings();
        AddAttributesMapping(cfg);
        cfg.BuildMappings();
        var sf = cfg.BuildSessionFactory();
        IsNotNull(sf);
        using var session = sf.OpenSession();
        var cities = session.Query<City>().ToList();
        IsNotEmpty(cities);
        foreach (var city in cities) {
            Greater(city.Id, 0);
            NotNull(city.Name);
        }
    }

    [Test]
    public void _05_CanInsertDeleteCities() {
        var cfg = ConfigNHibernate();
        cfg.AddIdentityMappings();
        AddAttributesMapping(cfg);
        cfg.BuildMappings();
        var sf = cfg.BuildSessionFactory();
        IsNotNull(sf);
        using var session = sf.OpenSession();
        var tx = session.BeginTransaction();
        var city = new City {
            Name = "Test City"
        };
        session.Save(city);
        session.Flush();
        Greater(city.Id, 0);
        tx.Rollback();
    }

    private void QueryUsers(ISessionFactory sf) {
        using var session = sf.OpenSession();
        var users = session.Query<AppUser>()
            .Where(u => u.LoginCount > 0)
            .ToList();
        GreaterOrEqual(users.Count, 0);
    }

}
