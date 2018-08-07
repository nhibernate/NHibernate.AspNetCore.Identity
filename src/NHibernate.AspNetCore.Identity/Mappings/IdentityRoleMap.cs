using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace NHibernate.AspNetCore.Identity.Mappings
{
    public class IdentityRoleMap : IdentityRoleMap<IdentityRole>
    {

    }

    public class IdentityRoleMap<TRole> : ClassMapping<TRole> where TRole : IdentityRole
    {
        public IdentityRoleMap()
        {
            Id(x => x.Id, m => m.Generator(new GuidGeneratorDef()));
            Table("aspnet_roles");
            Property(x => x.Name);
            Property(x => x.NormalizedName);
            Property(x => x.ConcurrencyStamp);
        }
    }
}
