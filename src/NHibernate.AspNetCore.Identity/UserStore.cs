using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using NHibernate.Linq;

namespace NHibernate.AspNetCore.Identity {

    public class UserStore : UserStore<IdentityUser, IdentityRole> {
        public UserStore(
            ISession session,
            IdentityErrorDescriber errorDescriber = null
        ) : base(session, errorDescriber) { }
    }

    public class UserStore<TUser, TRole> :
        UserStoreBase<TUser, TRole, string, IdentityUserClaim, IdentityUserRole, IdentityUserLogin, IdentityUserToken, IdentityRoleClaim>,
        IProtectedUserStore<TUser> where TUser : IdentityUser where TRole : IdentityRole {

        public ISession Session { get; }

        public bool AutoFlushChanges { get; set; } = true;

        public UserStore(
            ISession session,
            IdentityErrorDescriber errorDescriber = null
        ) : base(errorDescriber ?? new IdentityErrorDescriber()) {
            if (session == null) {
                throw new ArgumentNullException(nameof(session));
            }
            Session = session;
        }

        public override IQueryable<TUser> Users => Session.Query<TUser>();

        private IQueryable<TRole> Roles => Session.Query<TRole>();

        private IQueryable<IdentityUserClaim> UserClaims => Session.Query<IdentityUserClaim>();

        private IQueryable<IdentityUserRole> UserRoles => Session.Query<IdentityUserRole>();

        private IQueryable<IdentityUserLogin> UserLogins => Session.Query<IdentityUserLogin>();

        private IQueryable<IdentityUserToken> UserTokens => Session.Query<IdentityUserToken>();

        public override async Task<IdentityResult> CreateAsync(
            TUser user, CancellationToken cancellationToken = new CancellationToken()
        ) {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null) {
                throw new ArgumentNullException(nameof(user));
            }
            await Session.SaveAsync(user, cancellationToken);
            await FlushChanges(cancellationToken);
            return IdentityResult.Success;
        }

        public override async Task<IdentityResult> UpdateAsync(
            TUser user,
            CancellationToken cancellationToken = new CancellationToken()
        ) {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null) {
                throw new ArgumentNullException(nameof(user));
            }
            var dbUser = await Users.FirstOrDefaultAsync(
                u => u.Id.Equals(user.Id),
                cancellationToken
            );
            if (dbUser == null) {
                return IdentityResult.Failed();
            }
            dbUser.AccessFailedCount = user.AccessFailedCount;
            dbUser.ConcurrencyStamp = Guid.NewGuid().ToString("N");
            dbUser.Email = user.Email;
            dbUser.EmailConfirmed = user.EmailConfirmed;
            dbUser.LockoutEnabled = user.LockoutEnabled;
            dbUser.LockoutEnd = user.LockoutEnd;
            dbUser.NormalizedEmail = user.NormalizedEmail;
            dbUser.NormalizedUserName = user.NormalizedEmail;
            dbUser.PasswordHash = user.PasswordHash;
            dbUser.PhoneNumber = user.PhoneNumber;
            dbUser.PhoneNumberConfirmed = user.PhoneNumberConfirmed;
            dbUser.SecurityStamp = user.SecurityStamp;
            dbUser.TwoFactorEnabled = user.TwoFactorEnabled;
            dbUser.UserName = user.UserName;
            await Session.UpdateAsync(dbUser, cancellationToken);
            await FlushChanges(cancellationToken);
            return IdentityResult.Success;
        }

        public override async Task<IdentityResult> DeleteAsync(
            TUser user,
            CancellationToken cancellationToken = new CancellationToken()
        ) {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null) {
                throw new ArgumentNullException(nameof(user));
            }
            await Session.DeleteAsync(user, cancellationToken);
            await FlushChanges(cancellationToken);
            return IdentityResult.Success;
        }

        public override async Task<TUser> FindByIdAsync(
            string userId,
            CancellationToken cancellationToken = new CancellationToken()
        ) {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            var id = ConvertIdFromString(userId);
            var user = await Session.GetAsync<TUser>(id, cancellationToken);
            return user;
        }

        public override async Task<TUser> FindByNameAsync(
            string normalizedUserName,
            CancellationToken cancellationToken = new CancellationToken()
        ) {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            var user = await Users.FirstOrDefaultAsync(
                u => u.NormalizedUserName == normalizedUserName,
                cancellationToken
            );
            return user;
        }

        protected override async Task<TRole> FindRoleAsync(
            string normalizedRoleName,
            CancellationToken cancellationToken
        ) {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            var role = await Roles.FirstOrDefaultAsync(
                r => r.NormalizedName == normalizedRoleName,
                cancellationToken
            );
            return role;
        }

        protected override async Task<IdentityUserRole> FindUserRoleAsync(
            string userId,
            string roleId,
            CancellationToken cancellationToken
        ) {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            var userRole = await UserRoles.FirstOrDefaultAsync(
                ur => ur.UserId.Equals(userId) && ur.RoleId.Equals(roleId),
                cancellationToken
            );
            return userRole;
        }

        protected override async Task<TUser> FindUserAsync(
            string userId,
            CancellationToken cancellationToken
        ) {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            var user = await Users.FirstOrDefaultAsync(u => u.Id.Equals(userId));
            return user;
        }

        protected override async Task<IdentityUserLogin> FindUserLoginAsync(
            string userId,
            string loginProvider,
            string providerKey,
            CancellationToken cancellationToken
        ) {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            var userLogin = await UserLogins.FirstOrDefaultAsync(
                ul => ul.UserId.Equals(userId) && ul.LoginProvider == loginProvider
                    && ul.ProviderKey == providerKey,
                cancellationToken
            );
            return userLogin;
        }

        protected override async Task<IdentityUserLogin> FindUserLoginAsync(
            string loginProvider,
            string providerKey,
            CancellationToken cancellationToken
        ) {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            var userLogin = await UserLogins.FirstOrDefaultAsync(
                ul => ul.LoginProvider == loginProvider
                    && ul.ProviderKey == providerKey,
                cancellationToken
            );
            return userLogin;
        }

        public override async Task AddToRoleAsync(
            TUser user,
            string normalizedRoleName,
            CancellationToken cancellationToken = new CancellationToken()
        ) {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null) {
                throw new ArgumentNullException(nameof(user));
            }
            if (string.IsNullOrWhiteSpace(normalizedRoleName)) {
                throw new ArgumentNullException(nameof(normalizedRoleName));
            }
            var role = await FindRoleAsync(
                normalizedRoleName,
                cancellationToken
            );
            if (role == null) {
                throw new InvalidOperationException($"Role {normalizedRoleName} not found!");
            }
            await Session.SaveAsync(CreateUserRole(user, role));
            await FlushChanges(cancellationToken);
        }

        public override async Task RemoveFromRoleAsync(
            TUser user,
            string normalizedRoleName,
            CancellationToken cancellationToken = new CancellationToken()
        ) {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null) {
                throw new ArgumentNullException(nameof(user));
            }
            if (string.IsNullOrWhiteSpace(normalizedRoleName)) {
                throw new ArgumentNullException(nameof(normalizedRoleName));
            }
            var role = await FindRoleAsync(
                normalizedRoleName,
                cancellationToken
            );
            if (role != null) {
                var userRole = await FindUserRoleAsync(
                    user.Id,
                    role.Id,
                    cancellationToken
                );
                if (userRole != null) {
                    await Session.DeleteAsync(userRole, cancellationToken);
                    await FlushChanges(cancellationToken);
                }
            }
        }

        public override async Task<IList<string>> GetRolesAsync(
            TUser user,
            CancellationToken cancellationToken = new CancellationToken()
        ) {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null) {
                throw new ArgumentNullException(nameof(user));
            }
            var userId = user.Id;
            var query = from userRole in UserRoles
                join role in Roles on userRole.RoleId equals role.Id
                where userRole.UserId.Equals(userId)
                select role.Name;
            var roles = await query.ToListAsync(cancellationToken);
            return roles;
        }

        public override async Task<bool> IsInRoleAsync(
            TUser user,
            string normalizedRoleName,
            CancellationToken cancellationToken = new CancellationToken()
        ) {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null) {
                throw new ArgumentNullException(nameof(user));
            }
            if (string.IsNullOrWhiteSpace(normalizedRoleName)) {
                throw new ArgumentNullException(nameof(normalizedRoleName));
            }
            var role = await FindRoleAsync(normalizedRoleName, cancellationToken);
            if (role != null) {
                var userRole = await FindUserRoleAsync(
                    user.Id,
                    role.Id,
                    cancellationToken
                );
                return userRole != null;
            }
            return false;
        }

        public override async Task<IList<Claim>> GetClaimsAsync(
            TUser user,
            CancellationToken cancellationToken = new CancellationToken()
        ) {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null) {
                throw new ArgumentNullException(nameof(user));
            }
            var claims = await UserClaims.Where(
                    uc => uc.UserId.Equals(user.Id)
                )
                .Select(c => c.ToClaim())
                .ToListAsync(cancellationToken);
            return claims;
        }

        public override async Task AddClaimsAsync(
            TUser user,
            IEnumerable<Claim> claims,
            CancellationToken cancellationToken = new CancellationToken()
        ) {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null) {
                throw new ArgumentNullException(nameof(user));
            }
            if (claims == null) {
                throw new ArgumentNullException(nameof(claims));
            }
            foreach (var claim in claims) {
                await Session.SaveAsync(
                    CreateUserClaim(user, claim),
                    cancellationToken
                );
            }
            await FlushChanges(cancellationToken);
        }

        public override async Task ReplaceClaimAsync(
            TUser user,
            Claim claim,
            Claim newClaim,
            CancellationToken cancellationToken = new CancellationToken()
        ) {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null) {
                throw new ArgumentNullException(nameof(user));
            }
            if (claim == null) {
                throw new ArgumentNullException(nameof(claim));
            }
            if (newClaim == null) {
                throw new ArgumentNullException(nameof(newClaim));
            }
            var matchedClaims = await UserClaims.Where(
                    uc => uc.UserId.Equals(user.Id) &&
                        uc.ClaimValue == claim.Value
                        && uc.ClaimType == claim.Type
                )
                .ToListAsync(cancellationToken);
            foreach (var matchedClaim in matchedClaims) {
                matchedClaim.ClaimType = newClaim.Type;
                matchedClaim.ClaimValue = newClaim.Value;
                await Session.UpdateAsync(matchedClaim, cancellationToken);
            }
            await FlushChanges(cancellationToken);
        }

        public override async Task RemoveClaimsAsync(
            TUser user,
            IEnumerable<Claim> claims,
            CancellationToken cancellationToken = new CancellationToken()
        ) {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null) {
                throw new ArgumentNullException(nameof(user));
            }
            if (claims == null) {
                throw new ArgumentNullException(nameof(claims));
            }
            foreach (var claim in claims) {
                var matchedClaims = await UserClaims.Where(
                        uc => uc.UserId.Equals(user.Id) &&
                            uc.ClaimValue == claim.Value
                            && uc.ClaimType == claim.Type
                    )
                    .ToListAsync(cancellationToken);
                foreach (var matchedClaim in matchedClaims) {
                    await Session.DeleteAsync(matchedClaim, cancellationToken);
                }
            }
            await FlushChanges(cancellationToken);
        }

        public override async Task AddLoginAsync(
            TUser user,
            UserLoginInfo login,
            CancellationToken cancellationToken = new CancellationToken()
        ) {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null) {
                throw new ArgumentNullException(nameof(user));
            }
            if (login == null) {
                throw new ArgumentNullException(nameof(login));
            }
            await Session.SaveAsync(
                CreateUserLogin(user, login),
                cancellationToken
            );
            await FlushChanges(cancellationToken);
        }

        public override async Task RemoveLoginAsync(
            TUser user,
            string loginProvider,
            string providerKey,
            CancellationToken cancellationToken = new CancellationToken()
        ) {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null) {
                throw new ArgumentNullException(nameof(user));
            }
            var login = await FindUserLoginAsync(
                user.Id,
                loginProvider,
                providerKey,
                cancellationToken
            );
            if (login != null) {
                await Session.DeleteAsync(login, cancellationToken);
            }
        }

        public override async Task<IList<UserLoginInfo>> GetLoginsAsync(
            TUser user,
            CancellationToken cancellationToken = new CancellationToken()
        ) {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null) {
                throw new ArgumentNullException(nameof(user));
            }
            var userId = user.Id;
            var logins = await UserLogins.Where(l => l.UserId.Equals(userId))
                .Select(
                    l => new UserLoginInfo(
                        l.LoginProvider,
                        l.ProviderKey,
                        l.ProviderDisplayName
                    )
                )
                .ToListAsync(cancellationToken);
            return logins;
        }

        public override async Task<TUser> FindByLoginAsync(
            string loginProvider,
            string providerKey,
            CancellationToken cancellationToken = new CancellationToken()
        ) {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            var userLogin = await FindUserLoginAsync(
                loginProvider,
                providerKey,
                cancellationToken
            );
            if (userLogin != null) {
                return await FindUserAsync(userLogin.UserId, cancellationToken);
            }
            return null;
        }

        public override async Task<TUser> FindByEmailAsync(
            string normalizedEmail,
            CancellationToken cancellationToken = new CancellationToken()
        ) {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            return await Users.FirstOrDefaultAsync(
                u => u.NormalizedEmail == normalizedEmail,
                cancellationToken
            );
        }

        public override async Task<IList<TUser>> GetUsersForClaimAsync(
            Claim claim,
            CancellationToken cancellationToken = new CancellationToken()
        ) {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (claim == null) {
                throw new ArgumentNullException(nameof(claim));
            }
            var query = from userclaims in UserClaims
                join user in Users on userclaims.UserId equals user.Id
                where userclaims.ClaimValue == claim.Value
                    && userclaims.ClaimType == claim.Type
                select user;
            return await query.ToListAsync(cancellationToken);
        }

        public override async Task<IList<TUser>> GetUsersInRoleAsync(
            string normalizedRoleName,
            CancellationToken cancellationToken = new CancellationToken()
        ) {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (string.IsNullOrEmpty(normalizedRoleName)) {
                throw new ArgumentNullException(normalizedRoleName);
            }
            var role = await FindRoleAsync(
                normalizedRoleName,
                cancellationToken
            );
            if (role != null) {
                var query = from userrole in UserRoles
                    join user in Users on userrole.UserId equals user.Id
                    where userrole.RoleId.Equals(role.Id)
                    select user;
                return await query.ToListAsync(cancellationToken);
            }
            return new List<TUser>();
        }

        protected override async Task<IdentityUserToken> FindTokenAsync(
            TUser user,
            string loginProvider,
            string name,
            CancellationToken cancellationToken
        ) {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null) {
                throw new ArgumentNullException(nameof(user));
            }
            var token = await UserTokens.FirstOrDefaultAsync(
                ut => ut.UserId.Equals(user.Id) &&
                    ut.LoginProvider == loginProvider
                    && ut.Name == name,
                cancellationToken);
            return token;
        }

        public override async Task SetTokenAsync(
            TUser user,
            string loginProvider,
            string name,
            string value,
            CancellationToken cancellationToken
        ) {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null) {
                throw new ArgumentNullException(nameof(user));
            }
            var userToken = await FindTokenAsync(user, loginProvider, name, cancellationToken);
            if (userToken == null) {
                userToken = this.CreateUserToken(user, loginProvider, name, value);
                await this.AddUserTokenAsync(userToken);
            }
            else {
                userToken.Value = value;
            }
            await FlushChanges(cancellationToken);
        }

        protected override async Task AddUserTokenAsync(IdentityUserToken token) {
            ThrowIfDisposed();
            if (token == null) {
                throw new ArgumentNullException(nameof(token));
            }
            await Session.SaveAsync(token);
            await FlushChanges();
        }

        protected override async Task RemoveUserTokenAsync(IdentityUserToken token) {
            ThrowIfDisposed();
            if (token == null) {
                throw new ArgumentNullException(nameof(token));
            }
            await Session.DeleteAsync(token);
            await FlushChanges();
        }

        protected async Task FlushChanges(
            CancellationToken cancellationToken = default(CancellationToken)
        ) {
            if (AutoFlushChanges) {
                await Session.FlushAsync(cancellationToken);
                Session.Clear();
            }
        }

    }
}
