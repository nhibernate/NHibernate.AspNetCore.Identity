using NHibernate.Mapping.ByCode.Conformist;

namespace NHibernate.AspNetCore.Identity.Mappings
{
    public class IdentityUserClaimMap : ClassMapping<IdentityUserClaim>
    {
        public IdentityUserClaimMap()
        {
            Id(x => x.Id);
            Table("aspnet_user_claims");
            Property(x => x.ClaimType);
            Property(x => x.ClaimValue);
            Property(x => x.UserId);
        }
    }
}
