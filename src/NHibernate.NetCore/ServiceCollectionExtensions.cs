using System;
using Microsoft.Extensions.DependencyInjection;
using NHibernate.Cfg;
using System.Xml;
using System.Reflection;

namespace NHibernate.NetCore {

    public static class ServiceCollectionExtensions {

        public static void AddHibernate(
            this IServiceCollection services
        ) {
            if (services == null) {
                throw new ArgumentNullException(nameof(services));
            }
            var cfg = new Configuration();
            cfg.Configure();
            AddHibernate(services, cfg);
        }

        public static void AddHibernate(
            this IServiceCollection services,
            string path
        ) {
            if (services == null) {
                throw new ArgumentNullException(nameof(services));
            }
            if (string.IsNullOrEmpty(path)) {
                throw new ArgumentNullException(nameof(path));
            }
            var cfg = new Configuration();
            cfg.Configure(path);
            AddHibernate(services, cfg);
        }

        public static void AddHibernate(
            this IServiceCollection services,
            XmlReader xmlReader
        ) {
            if (services == null) {
                throw new ArgumentNullException(nameof(services));
            }
            if (xmlReader == null) {
                throw new ArgumentNullException(nameof(xmlReader));
            }
            var cfg = new Configuration();
            cfg.Configure(xmlReader);
            AddHibernate(services, cfg);
        }

        public static void AddHibernate(
            this IServiceCollection services,
            Assembly assembly,
            string resourceName
        ) {
            if (services == null) {
                throw new ArgumentNullException(nameof(services));
            }
            if (assembly == null) {
                throw new ArgumentNullException(nameof(assembly));
            }
            if (resourceName == null) {
                throw new ArgumentNullException(nameof(resourceName));
            }
            var cfg = new Configuration();
            cfg.Configure(assembly, resourceName);
            AddHibernate(services, cfg);
        }

        public static void AddHibernate(
            this IServiceCollection services,
            Configuration cfg
        ) {
            if (services == null) {
                throw new ArgumentNullException(nameof(services));
            }
            if (cfg == null) {
                throw new ArgumentNullException(nameof(cfg));
            }
            // Add Configuration as singleton
            services.AddSingleton(cfg);
            // Add ISessionFactory as singleton
            services.AddSingleton(provider => {
                var config = provider.GetService<Configuration>();
                return config.BuildSessionFactory();
            });
            // Add ISession as scoped
            services.AddScoped(provider => {
                var factory = provider.GetService<ISessionFactory>();
                return factory.OpenSession();
            });
        }
    }
}
