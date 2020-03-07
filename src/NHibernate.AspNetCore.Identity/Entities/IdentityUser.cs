using System;
using Microsoft.AspNetCore.Identity;

namespace NHibernate.AspNetCore.Identity {

    public class IdentityUser : IdentityUser<string> {

        public virtual long? LockoutEndUnixTimeMilliseconds { get; set; }

        public override DateTimeOffset? LockoutEnd {
            get {
                if (!LockoutEndUnixTimeMilliseconds.HasValue) {
                    return null;
                }
                var offset = DateTimeOffset.FromUnixTimeMilliseconds(
                    LockoutEndUnixTimeMilliseconds.Value
                );
                return TimeZoneInfo.ConvertTime(offset, TimeZoneInfo.Local);
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

}
