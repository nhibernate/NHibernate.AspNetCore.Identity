using Microsoft.AspNetCore.Mvc;
using NHibernate.Extensions.NetCore;
using NHibernate.AspNetCore.Identity;
using NHibernate.Cfg;

using NHIdentityUser = NHibernate.AspNetCore.Identity.IdentityUser;
using NHIdentityRole = NHibernate.AspNetCore.Identity.IdentityRole;

namespace WebTest;

public class Startup {

    public Startup(IConfiguration configuration) {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(
        IServiceCollection services
    ) {
        var cfg = new Configuration();
        var file = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "hibernate.config"
        );
        cfg.Configure(file);
        // Add identity mapping based on dialect config (dialet must contains
        // PostgreSQL, MySQL or MsSql)
        cfg.AddIdentityMappings();
        // using default xml mapping.
        cfg.AddAssembly(typeof(Startup).Assembly);

        services.AddHibernate(cfg);
        services.AddDefaultIdentity<NHIdentityUser>()
            .AddRoles<NHIdentityRole>()
            .AddHibernateStores();

        services.AddControllersWithViews();

        services.Configure<ApiBehaviorOptions>(options => {
            options.SuppressConsumesConstraintForFormFileParameters = true;
            options.SuppressInferBindingSourcesForParameters = true;
            options.SuppressModelStateInvalidFilter = true;
        });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(
        WebApplication app,
        IWebHostEnvironment env,
        Microsoft.Extensions.Logging.ILoggerFactory loggerFactory
    ) {
        loggerFactory.UseAsHibernateLoggerFactory();
        if (env.IsDevelopment()) {
            app.UseDeveloperExceptionPage();
        }
        else {
            app.UseExceptionHandler("/Home/Error");
        }

        app.MapStaticAssets();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}"
        ).WithStaticAssets();
        app.MapRazorPages()
            .WithStaticAssets();
    }

}
