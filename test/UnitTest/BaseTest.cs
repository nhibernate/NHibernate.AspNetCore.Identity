using System;
using System.IO;
using NHibernate.Cfg;
using NHibernate.Mapping.Attributes;
using NHibernate.Mapping.ByCode;
using WebTest.Entities;

namespace UnitTest {

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

        protected void AddByCodeMapping(Configuration cfg) {
            var modelMapper = new ModelMapper();
            modelMapper.AddMapping<AppRoleMapping>();
            modelMapper.AddMapping<AppUserMapping>();
            modelMapper.AddMapping<TodoItemMapping>();
            var mappings = modelMapper.CompileMappingForAllExplicitlyAddedEntities();
            cfg.AddMapping(mappings);
        }

        protected void AddXmlMapping(Configuration cfg) {
            cfg.AddAssembly(typeof(AppUser).Assembly);
        }

        protected void AddAttributesMapping(Configuration cfg) {
            HbmSerializer.Default.Validate = true;
            var stream = HbmSerializer.Default.Serialize(
                typeof(AppUser).Assembly
            );
            // cfg.AddInputStream(stream);
            var streamReader = new System.IO.StreamReader(stream);
            var xml = streamReader.ReadToEnd();
            Console.WriteLine(xml);
            cfg.AddXml(xml);
        }

    }

}
