using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NHibernate.NetCore;
using NHibernate.AspNetCore.Identity;

namespace WebTest {

    public class Startup {

        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(
            IServiceCollection services
        ) {
            services.Configure<CookiePolicyOptions>(
                options => {
                    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                    options.CheckConsentNeeded = context => true;
                    options.MinimumSameSitePolicy = SameSiteMode.None;
                });

//            services.AddDbContext<ApplicationDbContext>(
//                options =>
//                    options.UseSqlite(
//                        Configuration.GetConnectionString(
//                            "DefaultConnection")));
//            services.AddDefaultIdentity<IdentityUser>()
//                .AddEntityFrameworkStores<ApplicationDbContext>();

            var path = System.IO.Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "hibernate.config"
            );

            services.AddHibernate(path);
            services.AddDefaultIdentity<NHibernate.AspNetCore.Identity.IdentityUser>()
                .AddRoles<NHibernate.AspNetCore.Identity.IdentityRole>()
                .AddHibernateStores();


            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.Configure<ApiBehaviorOptions>(options => {
                options.SuppressConsumesConstraintForFormFileParameters = true;
                options.SuppressInferBindingSourcesForParameters = true;
                options.SuppressModelStateInvalidFilter = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IHostingEnvironment env,
            Microsoft.Extensions.Logging.ILoggerFactory loggerFactory
        ) {
            loggerFactory.UseAsHibernateLoggerFactory();

            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

//            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(
                routes => {
                    routes.MapRoute(
                        name: "default",
                        template: "{controller=Home}/{action=Index}/{id?}");
                });
        }

    }

}
