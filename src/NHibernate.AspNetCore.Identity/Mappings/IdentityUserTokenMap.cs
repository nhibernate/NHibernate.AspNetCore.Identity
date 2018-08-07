using NHibernate.Mapping.ByCode.Conformist;

namespace NHibernate.AspNetCore.Identity.Mappings
{
    public class IdentityUserTokenMap : ClassMapping<IdentityUserToken>
    {
        public IdentityUserTokenMap()
        {
            Table("aspnet_user_tokens");
            ComposedId(m =>
            {
                m.Property(x => x.UserId);
                m.Property(x => x.LoginProvider);
                m.Property(x => x.Name);
            });

            Property(x => x.Value);
        }
    }
}
