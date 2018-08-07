using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace NHibernate.AspNetCore.Identity.Mappings
{
    public class IdentityUserMap : IdentityUserMap<IdentityUser>
    {

    }

    public class IdentityUserMap<TUser> : ClassMapping<TUser> where TUser : IdentityUser
    {
        public IdentityUserMap()
        {
            Id(x => x.Id, m => m.Generator(new GuidGeneratorDef()));
            Table("aspnet_users");
            Property(x => x.AccessFailedCount);
            Property(x => x.ConcurrencyStamp);
            Property(x => x.Email);
            Property(x => x.EmailConfirmed);
            Property(x => x.LockoutEnabled);
            Property(x => x.LockoutEndUnixTimeMilliseconds, m => m.Column("LockoutEnd"));
            Property(x => x.PasswordHash);
            Property(x => x.PhoneNumber);
            Property(x => x.PhoneNumberConfirmed);
            Property(x => x.TwoFactorEnabled);
            Property(x => x.UserName);
            Property(x => x.NormalizedUserName);
            Property(x => x.SecurityStamp);
        }
    }
}
