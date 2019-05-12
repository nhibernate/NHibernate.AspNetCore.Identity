# ASP.NET Core Identity Provider for NHibernate

Nuget packages:

- [NHibernate.AspNetCore.Identity](https://www.nuget.org/packages/NHibernate.AspNetCore.Identity/)

## NHibernate.AspNetCore.Identity

ASPNET Core Identity provider that uses NHibernate for storage

```cs
public class Startup {

    public void ConfigureServices(
        IServiceCollection services
    ) {
        // Remove EFCore stores.
        // services.AddDbContext<ApplicationDbContext>(
        // options =>
        //     options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
        // services.AddDefaultIdentity<IdentityUser>()
        //     .AddEntityFrameworkStores<ApplicationDbContext>();

        // Add Hibernate stores
        var cfg = new Configuration();
        var file = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "hibernate.config"
        );
        cfg.Configure(file);
        // Add Identity Mappings for PostgreSQL
        cfg.AddIdentityMappingsForPostgres();
        // Add Identity Mappings for SqlServer
        // cfg.AddIdentityMappingsForSqlServer();

        services.AddHibernate(cfg);
        services.AddDefaultIdentity<WebTest.Entities.ApplicationUser>()
            .AddRoles<WebTest.Entities.ApplicationRole>()
            .AddHibernateStores();

    }
}
```

> Note: When using with SqlServer, you need add `System.Data.SqlClient` package to your project.

For more detailed samples, please look at the [WebTest](https://github.com/nhibernate/NHibernate.AspNetCore.Identity/tree/master/test/WebTest) project.

## Credits

Special thanks to the following individuals, organisations and projects whose work is so important to the success of NHibernate (in no particular order):

- [NUnit](https://nunit.org/) - unit testing;
- [JetBrains](https://www.jetbrains.com/?from=NHibernate.AspNetCore.Identity) - open source license;