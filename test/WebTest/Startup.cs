using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NHibernate.NetCore;
using NHibernate.AspNetCore.Identity;
using NHibernate.Cfg;
using System.IO;
using WebTest.Repositories;
using WebTest.Entities;
using WebTest.Models;

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
                    // This lambda determines whether user consent for
                    // non-essential cookies is needed for a given request.
                    options.CheckConsentNeeded = context => true;
                    options.MinimumSameSitePolicy = SameSiteMode.None;
                });
            var cfg = new Configuration();
            var file = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "hibernate.config"
            );
            cfg.Configure(file);
            cfg.AddIdentityMappingsForPostgres();

            services.AddHibernate(cfg);
            services.AddDefaultIdentity<WebTest.Entities.AppUser>()
                .AddRoles<WebTest.Entities.AppRole>()
                .AddHibernateStores();

            services.AddRouting(opts => {
                opts.LowercaseUrls = true;
            });

            services.AddControllersWithViews();
            services.AddRazorPages();

            services.Configure<ApiBehaviorOptions>(options => {
                options.SuppressConsumesConstraintForFormFileParameters = true;
                options.SuppressInferBindingSourcesForParameters = true;
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddSingleton<AutoMapper.IMapper>(serviceProvider => {
                var mapperConfig = new AutoMapper.MapperConfiguration(
                    configure => {
                        configure.AddMaps("WebTest");
                    });
                var mapper = mapperConfig.CreateMapper();
                return mapper;
            });

            services.AddScoped<ITodoItemRepository, TodoItemRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env,
            Microsoft.Extensions.Logging.ILoggerFactory loggerFactory
        ) {
            loggerFactory.UseAsHibernateLoggerFactory();
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                // app.UseDatabaseErrorPage();
            }
            else {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            // app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }

    }

}
