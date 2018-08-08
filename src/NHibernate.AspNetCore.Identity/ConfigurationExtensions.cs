using System.Reflection;
using NHibernate.Cfg;
using NHibernate.Mapping.ByCode;

namespace NHibernate.AspNetCore.Identity {

    public static class ConfigurationExtensions {

        public static Configuration AddIdentityMappingsForPostgres(
            this Configuration cfg
        ) {
            var asm = typeof(IdentityUser).Assembly;
            var stream = asm.GetManifestResourceStream(
                "NHibernate.AspNetCore.Identity.Mappings.AspNetIdentity.pg.xml"
            );
            cfg.AddInputStream(stream);
            return cfg;
        }

    }

}
