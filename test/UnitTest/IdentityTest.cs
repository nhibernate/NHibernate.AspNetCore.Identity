using NHibernate;
using NHibernate.AspNetCore.Identity;
using NHIdentityUser = NHibernate.AspNetCore.Identity.IdentityUser;

namespace UnitTest;

[TestFixture]
public class IdentityTest : BaseTest {

    [Test]
    public void _01_CanExtendByCodeWithByCode() {
        var cfg = ConfigNHibernate();
        cfg.AddIdentityMappings();
        cfg.BuildMappings();
        var sf = cfg.BuildSessionFactory();
        That(sf, Is.Not.Null);
        QueryUsers(sf);
    }

    private void QueryUsers(ISessionFactory sf) {
        using var session = sf.OpenSession();
        var users = session.Query<NHIdentityUser>()
            .Where(u => u.LockoutEnabled == true)
            .ToList();
        That(users.Count, Is.GreaterThanOrEqualTo(0));
    }

}
