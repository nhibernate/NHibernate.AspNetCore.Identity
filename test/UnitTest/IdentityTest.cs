using System;
using System.IO;
using System.Linq;
using NHibernate;
using NHibernate.AspNetCore.Identity;
using NHibernate.Cfg;
using NHibernate.Mapping.ByCode;
using NUnit.Framework;
using WebTest.Entities;

namespace UnitTest {

    [TestFixture]
    public class IdentityTest {

        [Test]
        public void _01_CanExtendByCodeWithByCode() {
            var cfg = new Configuration();
            ConfigNHibernate(cfg);
            cfg.AddIdentityMappings();
            AddByCodeMapping(cfg);
            cfg.BuildMappings();
            var sf = cfg.BuildSessionFactory();
            Assert.IsNotNull(sf);
            using var session = sf.OpenSession();
            var users = session.Query<AppUser>()
                .Where(u => u.LoginCount > 0)
                .ToList();
            Assert.GreaterOrEqual(users.Count, 0);
        }

        [Test]
        public void _02_CanExtendByCodeWithXml() {
            var cfg = new Configuration();
            ConfigNHibernate(cfg);
            cfg.AddIdentityMappings();
            AddXmlMapping(cfg);
            cfg.BuildMappings();
            var sf = cfg.BuildSessionFactory();
            Assert.IsNotNull(sf);
            using var session = sf.OpenSession();
            var users = session.Query<AppUser>()
                .Where(u => u.LoginCount > 0)
                .ToList();
            Assert.GreaterOrEqual(users.Count, 0);
        }

        [Test]
        public void _03_CanExtendXmlByXml() {
            var cfg = new Configuration();
            ConfigNHibernate(cfg);
            var asm = typeof(IdentityRole).Assembly;
            var stream = asm.GetManifestResourceStream(
                "NHibernate.AspNetCore.Identity.Mappings.AspNetCoreIdentity.pg.xml"
            );
            cfg.AddInputStream(stream);
            AddXmlMapping(cfg);
            cfg.BuildMappings();
            using var sf = cfg.BuildSessionFactory();
            Assert.IsNotNull(sf);
            QueryUsers(sf);
        }

        [Test]
        public void _04_CanExtendXmlByByCode() {
            var cfg = new Configuration();
            ConfigNHibernate(cfg);
            var asm = typeof(IdentityRole).Assembly;
            var stream = asm.GetManifestResourceStream(
                "NHibernate.AspNetCore.Identity.Mappings.AspNetCoreIdentity.pg.xml"
            );
            cfg.AddInputStream(stream);
            AddByCodeMapping(cfg);
            cfg.BuildMappings();
            using var sf = cfg.BuildSessionFactory();
            Assert.IsNotNull(sf);
            QueryUsers(sf);
        }

        private void ConfigNHibernate(Configuration cfg) {
            var file = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "hibernate.config"
            );
            cfg.Configure(file);
        }

        private void AddByCodeMapping(Configuration cfg) {
            var modelMapper = new ModelMapper();
            modelMapper.AddMapping<AppRoleMapping>();
            modelMapper.AddMapping<AppUserMapping>();
            modelMapper.AddMapping<TodoItemMapping>();
            var mappings = modelMapper.CompileMappingForAllExplicitlyAddedEntities();
            cfg.AddMapping(mappings);
        }

        private void AddXmlMapping(Configuration cfg) {
            cfg.AddAssembly(typeof(AppUser).Assembly);
        }

        private void QueryUsers(ISessionFactory sf) {
            using var session = sf.OpenSession();
            var users = session.Query<AppUser>()
                .Where(u => u.LoginCount > 0)
                .ToList();
            Assert.GreaterOrEqual(users.Count, 0);
        }

    }

}
