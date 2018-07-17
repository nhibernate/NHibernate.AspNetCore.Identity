using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using NHibernate.Linq;

namespace NHibernate.AspNetCore.Identity {
    
    public class RoleStore
        : IQueryableRoleStore<IdentityRole>, IRoleClaimStore<IdentityRole> {

        private bool disposed;
        
        public ISession Session { get; }
        
        public IdentityErrorDescriber ErrorDescriber { get; set; }
        
        public bool AutoFlushChanges { get; set; } = true;

        public RoleStore(
            ISession session,
            IdentityErrorDescriber describer = null
        ) {
            if (session == null) {
                throw new ArgumentNullException(nameof(session));
            }
            Session = session;
            ErrorDescriber = describer ?? new IdentityErrorDescriber();
        }

        public void Dispose() {
            Session.Dispose();
            disposed = true;
        }

        public virtual async Task<IdentityResult> CreateAsync(
            IdentityRole role,
            CancellationToken cancellationToken
        ) {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (role == null) {
                throw new ArgumentNullException(nameof(role));
            }
            await Session.SaveAsync(role, cancellationToken);
            await FlushChanges(cancellationToken);
            return IdentityResult.Success;
        }

        public virtual async Task<IdentityResult> UpdateAsync(
            IdentityRole role,
            CancellationToken cancellationToken
        ) {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (role == null) {
                throw new ArgumentNullException(nameof(role));
            }
            var candidate = await Session.GetAsync<IdentityRole>(
                role.Id,
                cancellationToken
            );
            if (candidate == null) {
                return IdentityResult.Failed();
            }
            candidate.Name = role.Name;
            candidate.NormalizedName = role.NormalizedName;
            candidate.ConcurrencyStamp = Guid.NewGuid().ToString("N");
            await Session.UpdateAsync(candidate, cancellationToken);
            await FlushChanges(cancellationToken);
            return IdentityResult.Success;
        }

        public virtual async Task<IdentityResult> DeleteAsync(
            IdentityRole role,
            CancellationToken cancellationToken
        ) {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (role == null) {
                throw new ArgumentNullException(nameof(role));
            }
            await Session.DeleteAsync(role, cancellationToken);
            await FlushChanges(cancellationToken);
            return IdentityResult.Success;
        }

        public virtual Task<string> GetRoleIdAsync(
            IdentityRole role,
            CancellationToken cancellationToken
        ) {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (role == null) {
                throw new ArgumentNullException(nameof(role));
            }
            return Task.FromResult(role.Id);
        }

        public virtual Task<string> GetRoleNameAsync(
            IdentityRole role,
            CancellationToken cancellationToken
        ) {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (role == null) {
                throw new ArgumentNullException(nameof(role));
            }
            return Task.FromResult(role.Name);
        }

        public Task SetRoleNameAsync(
            IdentityRole role,
            string roleName,
            CancellationToken cancellationToken
        ) {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (role == null) {
                throw new ArgumentNullException(nameof(role));
            }
            role.Name = roleName;
            return Task.CompletedTask;
        }

        public Task<string> GetNormalizedRoleNameAsync(
            IdentityRole role,
            CancellationToken cancellationToken
        ) {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (role == null) {
                throw new ArgumentNullException(nameof(role));
            }
            return Task.FromResult(role.NormalizedName);
        }

        public Task SetNormalizedRoleNameAsync(
            IdentityRole role,
            string normalizedName,
            CancellationToken cancellationToken
        ) {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (role == null) {
                throw new ArgumentNullException(nameof(role));
            }
            role.NormalizedName = normalizedName;
            return Task.CompletedTask;
        }

        public async Task<IdentityRole> FindByIdAsync(
            string roleId,
            CancellationToken cancellationToken
        ) {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            var id = roleId;
            var role = await Session.GetAsync<IdentityRole>(id, cancellationToken);
            return role;
        }

        public async Task<IdentityRole> FindByNameAsync(
            string normalizedRoleName,
            CancellationToken cancellationToken
        ) {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            var role = await Roles
                .FirstOrDefaultAsync(
                    r => r.NormalizedName == normalizedRoleName,
                    cancellationToken
                );
            return role;
        }

        public virtual IQueryable<IdentityRole> Roles => Session.Query<IdentityRole>();

        private IQueryable<IdentityRoleClaim> RoleClaims => Session.Query<IdentityRoleClaim>();

        public virtual async Task<IList<Claim>> GetClaimsAsync(
            IdentityRole role,
            CancellationToken cancellationToken
        ) {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (role == null) {
                throw new ArgumentNullException(nameof(role));
            }
            
            var claims = await RoleClaims
                .Where(rc => rc.RoleId == role.Id)
                .Select(c => new Claim(c.ClaimType, c.ClaimValue))
                .ToListAsync(cancellationToken);
            return claims;
        }

        public virtual async Task AddClaimAsync(
            IdentityRole role,
            Claim claim,
            CancellationToken cancellationToken
        ) {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (role == null) {
                throw new ArgumentNullException(nameof(role));
            }
            if (claim == null) {
                throw new ArgumentNullException(nameof(claim));
            }
            var roleClaim = CreateRoleClaim(role, claim);
            await Session.SaveAsync(roleClaim, cancellationToken);
            await FlushChanges(cancellationToken);
        }

        public virtual async Task RemoveClaimAsync(
            IdentityRole role,
            Claim claim,
            CancellationToken cancellationToken = default(CancellationToken)
        ) {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (role == null) {
                throw new ArgumentNullException(nameof(role));
            }
            if (claim == null) {
                throw new ArgumentNullException(nameof(claim));
            }
            var claims = await RoleClaims.Where(
                    rc => rc.RoleId == role.Id
                        && rc.ClaimValue == claim.Value &&
                        rc.ClaimType == claim.Type
                )
                .ToListAsync(cancellationToken);
            foreach (var c in claims) {
                await Session.DeleteAsync(c, cancellationToken);
            }
            await FlushChanges(cancellationToken);
        }

        protected void ThrowIfDisposed() {
            if (disposed) {
                throw new ObjectDisposedException(GetType().Name);
            }
        }
        
        protected virtual IdentityRoleClaim CreateRoleClaim(
            IdentityRole role,
            Claim claim
        ) => new IdentityRoleClaim {
            RoleId = role.Id,
            ClaimType = claim.Type,
            ClaimValue = claim.Value
        };
        
        private async Task FlushChanges(CancellationToken cancellationToken) {
            if (AutoFlushChanges) {
                await Session.FlushAsync(cancellationToken);
                Session.Clear();
            }
        }

    }
}
