using NHibernate.Mapping.ByCode.Conformist;

namespace NHibernate.AspNetCore.Identity.Mappings
{
    public class IdentityUserLoginMap : ClassMapping<IdentityUserLogin>
    {
        public IdentityUserLoginMap()
        {
            ComposedId(m =>
            {
                m.Property(x => x.LoginProvider);
                m.Property(x => x.ProviderKey);
            });

            Table("aspnet_user_logins");
            Property(x => x.ProviderDisplayName);

            Property(x => x.UserId);
            // TODO User relation
        }
    }
}
