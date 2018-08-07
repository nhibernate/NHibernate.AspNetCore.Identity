using NHibernate.Mapping.ByCode.Conformist;

namespace NHibernate.AspNetCore.Identity.Mappings
{
    public class IdentityRoleClaimMap : ClassMapping<IdentityRoleClaim>
    {
        public IdentityRoleClaimMap()
        {
            Id(x => x.Id, m => m.Column("Id"));
            Table("aspnet_role_claims");
            Property(x => x.ClaimType);
            Property(x => x.ClaimValue);
        }
    }
}
