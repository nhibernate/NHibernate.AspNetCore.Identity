using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace NHibernate.AspNetCore.Identity {

    public class UserOnlyStore
        : UserStoreBase<IdentityUser, string, IdentityUserClaim, IdentityUserLogin, IdentityUserToken>, IUserLoginStore<IdentityUser>, IUserStore<IdentityUser>, IDisposable, IUserClaimStore<IdentityUser>, IUserPasswordStore<IdentityUser>, IUserSecurityStampStore<IdentityUser>, IUserEmailStore<IdentityUser>, IUserLockoutStore<IdentityUser>, IUserPhoneNumberStore<IdentityUser>, IQueryableUserStore<IdentityUser>, IUserTwoFactorStore<IdentityUser>, IUserAuthenticationTokenStore<IdentityUser>, IUserAuthenticatorKeyStore<IdentityUser>, IUserTwoFactorRecoveryCodeStore<IdentityUser>, IProtectedUserStore<IdentityUser> {

        public UserOnlyStore(IdentityErrorDescriber describer) : base(describer) { }

        public override Task<IdentityResult> CreateAsync(
            IdentityUser user,
            CancellationToken cancellationToken = new CancellationToken()
        ) {
            throw new NotImplementedException();
        }

        public override Task<IdentityResult> UpdateAsync(
            IdentityUser user,
            CancellationToken cancellationToken = new CancellationToken()) {
            throw new NotImplementedException();
        }

        public override Task<IdentityResult> DeleteAsync(
            IdentityUser user,
            CancellationToken cancellationToken = new CancellationToken()) {
            throw new NotImplementedException();
        }

        public override Task<IdentityUser> FindByIdAsync(
            string userId,
            CancellationToken cancellationToken = new CancellationToken()) {
            throw new NotImplementedException();
        }

        public override Task<IdentityUser> FindByNameAsync(
            string normalizedUserName,
            CancellationToken cancellationToken = new CancellationToken()) {
            throw new NotImplementedException();
        }

        protected override Task<IdentityUser> FindUserAsync(string userId, CancellationToken cancellationToken) {
            throw new NotImplementedException();
        }

        protected override Task<IdentityUserLogin> FindUserLoginAsync(
            string userId, string loginProvider, string providerKey,
            CancellationToken cancellationToken) {
            throw new NotImplementedException();
        }

        protected override Task<IdentityUserLogin> FindUserLoginAsync(
            string loginProvider, string providerKey,
            CancellationToken cancellationToken) {
            throw new NotImplementedException();
        }

        public override Task<IList<Claim>> GetClaimsAsync(
            IdentityUser user,
            CancellationToken cancellationToken = new CancellationToken()) {
            throw new NotImplementedException();
        }

        public override Task AddClaimsAsync(
            IdentityUser user, IEnumerable<Claim> claims,
            CancellationToken cancellationToken = new CancellationToken()) {
            throw new NotImplementedException();
        }

        public override Task ReplaceClaimAsync(
            IdentityUser user, Claim claim, Claim newClaim,
            CancellationToken cancellationToken = new CancellationToken()) {
            throw new NotImplementedException();
        }

        public override Task RemoveClaimsAsync(
            IdentityUser user, IEnumerable<Claim> claims,
            CancellationToken cancellationToken = new CancellationToken()) {
            throw new NotImplementedException();
        }

        public override Task<IList<IdentityUser>> GetUsersForClaimAsync(
            Claim claim, CancellationToken cancellationToken = new CancellationToken()) {
            throw new NotImplementedException();
        }

        protected override Task<IdentityUserToken> FindTokenAsync(
            IdentityUser user, string loginProvider, string name,
            CancellationToken cancellationToken) {
            throw new NotImplementedException();
        }

        protected override Task AddUserTokenAsync(IdentityUserToken token) {
            throw new NotImplementedException();
        }

        protected override Task RemoveUserTokenAsync(IdentityUserToken token) {
            throw new NotImplementedException();
        }

        public override IQueryable<IdentityUser> Users => throw new NotImplementedException();

        public override Task AddLoginAsync(
            IdentityUser user, UserLoginInfo login,
            CancellationToken cancellationToken = new CancellationToken()) {
            throw new NotImplementedException();
        }

        public override Task RemoveLoginAsync(
            IdentityUser user, string loginProvider, string providerKey,
            CancellationToken cancellationToken = new CancellationToken()) {
            throw new NotImplementedException();
        }

        public override Task<IList<UserLoginInfo>> GetLoginsAsync(
            IdentityUser user,
            CancellationToken cancellationToken = new CancellationToken()) {
            throw new NotImplementedException();
        }

        public override Task<IdentityUser> FindByEmailAsync(
            string normalizedEmail,
            CancellationToken cancellationToken = new CancellationToken()) {
            throw new NotImplementedException();
        }

    }
}
