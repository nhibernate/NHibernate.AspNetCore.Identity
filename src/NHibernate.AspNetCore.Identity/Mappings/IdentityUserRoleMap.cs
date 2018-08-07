using NHibernate.Mapping.ByCode.Conformist;

namespace NHibernate.AspNetCore.Identity.Mappings
{
    public class IdentityUserRoleMap : ClassMapping<IdentityUserRole>
    {
        public IdentityUserRoleMap()
        {
            Table("aspnet_user_roles");
            ComposedId(m =>
            {
                m.Property(x => x.UserId);
                m.Property(x => x.RoleId);
            });
        }
    }
}
