using System;
using Microsoft.AspNetCore.Identity;

namespace NHibernate.AspNetCore.Identity {

    public class IdentityUser : IdentityUser<string> {

        public virtual long? LockoutEndUnixTimeMilliseconds { get; set; }

        public override DateTimeOffset? LockoutEnd {
            get {
                if (LockoutEndUnixTimeMilliseconds.HasValue) {
                    return DateTimeOffset.FromUnixTimeMilliseconds(
                        LockoutEndUnixTimeMilliseconds.Value
                    );
                }
                return null;
            }
            set {
                if (value.HasValue) {
                    LockoutEndUnixTimeMilliseconds = value.Value.ToUnixTimeMilliseconds();
                }
                else {
                    LockoutEndUnixTimeMilliseconds = null;
                }
            }
        }
    }

    public class IdentityRole : IdentityRole<string> { }

    public class IdentityUserClaim : IdentityUserClaim<string> { }

    public class IdentityUserRole : IdentityUserRole<string> {

        protected bool Equals(IdentityUserRole other) {
            return RoleId == other.RoleId
                && UserId == other.UserId;
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) {
                return false;
            }
            if (ReferenceEquals(this, obj)) {
                return true;
            }
            if (obj.GetType() != GetType()) {
                return false;
            }
            return Equals((IdentityUserRole)obj);
        }

        public override int GetHashCode() {
            unchecked {
                var hashCode = 0;
                hashCode = RoleId.GetHashCode();
                hashCode = (hashCode * 397) ^ UserId.GetHashCode();
                return hashCode;
            }
        }

    }

    public class IdentityUserLogin : IdentityUserLogin<string> {

        protected bool Equals(IdentityUserLogin other) {
            return LoginProvider == other.LoginProvider
                && ProviderKey == other.ProviderKey;
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) {
                return false;
            }
            if (ReferenceEquals(this, obj)) {
                return true;
            }
            if (obj.GetType() != GetType()) {
                return false;
            }
            return Equals((IdentityUserLogin)obj);
        }

        public override int GetHashCode() {
            unchecked {
                var hashCode = 0;
                hashCode = LoginProvider.GetHashCode();
                hashCode = (hashCode * 397) ^ ProviderKey.GetHashCode();
                return hashCode;
            }
        }

    }

    public class IdentityUserToken : IdentityUserToken<string> {

        protected bool Equals(IdentityUserToken other) {
            return UserId == other.UserId
                && LoginProvider == other.LoginProvider
                && Name == other.Name;
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) {
                return false;
            }
            if (ReferenceEquals(this, obj)) {
                return true;
            }
            if (obj.GetType() != GetType()) {
                return false;
            }
            return Equals((IdentityUserToken)obj);
        }

        public override int GetHashCode() {
            unchecked {
                var hashCode = 0;
                hashCode = UserId.GetHashCode();
                hashCode = (hashCode * 397) ^ LoginProvider.GetHashCode();
                hashCode = (hashCode * 397) ^ Name.GetHashCode();
                return hashCode;
            }
        }
    }

    public class IdentityRoleClaim : IdentityRoleClaim<string> { }

}
