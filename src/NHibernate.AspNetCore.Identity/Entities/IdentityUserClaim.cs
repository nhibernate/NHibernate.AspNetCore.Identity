using Microsoft.AspNetCore.Identity;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace NHibernate.AspNetCore.Identity {

    public class IdentityUserClaim : IdentityUserClaim<string> { }

    public class IdentityUserClaimMapping : ClassMapping<IdentityUserClaim> {

        public IdentityUserClaimMapping() { }

    }

}
