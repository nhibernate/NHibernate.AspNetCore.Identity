using NHibernate.Cfg;

namespace UnitTest;

public abstract class BaseTest {

    protected Configuration ConfigNHibernate() {
        var cfg = new Configuration();
        var file = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "hibernate.config"
        );
        cfg.Configure(file);
        return cfg;
    }

}
