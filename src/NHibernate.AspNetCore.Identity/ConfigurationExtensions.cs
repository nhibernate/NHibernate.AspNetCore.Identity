using System.Reflection;
using NHibernate.Cfg;
using NHibernate.Mapping.ByCode;

namespace NHibernate.AspNetCore.Identity
{
    public static class ConfigurationExtensions
    {
        public static Configuration AddIdentityMappings(this Configuration cfg)
        {
            var mapper = new ModelMapper();
            mapper.AddMappings(Assembly.GetExecutingAssembly().GetExportedTypes());
            cfg.AddMapping(mapper.CompileMappingForAllExplicitlyAddedEntities());
            return cfg;
        }
    }
}
