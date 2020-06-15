using System;
using Microsoft.AspNetCore.Identity;

namespace NHibernate.AspNetCore.Identity {

    public class IdentityUser : IdentityUser<string> {

        public virtual long? LockoutEndUnixTimeSeconds { get; set; }

        public override DateTimeOffset? LockoutEnd {
            get {
                if (!LockoutEndUnixTimeSeconds.HasValue) {
                    return null;
                }
                var offset = DateTimeOffset.FromUnixTimeSeconds(
                    LockoutEndUnixTimeSeconds.Value
                );
                return TimeZoneInfo.ConvertTime(offset, TimeZoneInfo.Local);
            }
            set {
                if (value.HasValue) {
                    LockoutEndUnixTimeSeconds = value.Value.ToUnixTimeSeconds();
                }
                else {
                    LockoutEndUnixTimeSeconds = null;
                }
            }
        }
    }

}
