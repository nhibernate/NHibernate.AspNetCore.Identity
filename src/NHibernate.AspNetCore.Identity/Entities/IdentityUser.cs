using System;
using Microsoft.AspNetCore.Identity;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

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

    public class IdentityUserMappingPostgreSql : ClassMapping<IdentityUser> {

        public IdentityUserMappingPostgreSql() {
            Schema("public");
            Table("aspnet_users");
            Id(e => e.Id, id => {
                id.Column("id");
                id.Type(NHibernateUtil.String);
                id.Length(32);
                id.Generator(Generators.TriggerIdentity);
            });
            Property(e => e.UserName, prop => {
                prop.Column("user_name");
                prop.Type(NHibernateUtil.String);
                prop.Length(64);
                prop.NotNullable(true);
                prop.Unique(true);
            });
            Property(e => e.NormalizedUserName, prop => {
                prop.Column("normalized_user_name");
                prop.Type(NHibernateUtil.String);
                prop.Length(64);
                prop.NotNullable(true);
                prop.Unique(true);
            });
            Property(e => e.Email, prop => {
                prop.Column("email");
                prop.Type(NHibernateUtil.String);
                prop.Length(256);
                prop.NotNullable(true);
            });
            Property(e => e.NormalizedEmail, prop => {
                prop.Column("normalized_email");
                prop.Type(NHibernateUtil.String);
                prop.Length(256);
                prop.NotNullable(true);
            });
            Property(e => e.EmailConfirmed, prop => {
                prop.Column("email_confirmed");
                prop.Type(NHibernateUtil.Boolean);
                prop.NotNullable(true);
            });
            Property(e => e.PhoneNumber, prop => {
                prop.Column("phone_number");
                prop.Type(NHibernateUtil.String);
                prop.Length(128);
                prop.NotNullable(false);
            });
            Property(e => e.PhoneNumberConfirmed, prop => {
                prop.Column("phone_number_confirmed");
                prop.Type(NHibernateUtil.Boolean);
                prop.NotNullable(true);
            });
            Property(e => e.LockoutEnabled, prop => {
                prop.Column("lockout_enabled");
                prop.Type(NHibernateUtil.Boolean);
                prop.NotNullable(true);
            });
            Property(e => e.LockoutEndUnixTimeMilliseconds, prop => {
                prop.Column("lockout_end_unix_time_milliseconds");
                prop.Type(NHibernateUtil.Int64);
                prop.NotNullable(false);
            });
            Property(e => e.AccessFailedCount, prop => {
                prop.Column("access_failed_count");
                prop.Type(NHibernateUtil.Int32);
                prop.NotNullable(true);
            });
            Property(e => e.ConcurrencyStamp, prop => {
                prop.Column("concurrency_stamp");
                prop.Type(NHibernateUtil.String);
                prop.Length(36);
                prop.NotNullable(false);
            });
            Property(e => e.PasswordHash, prop => {
                prop.Column("password_hash");
                prop.Type(NHibernateUtil.String);
                prop.Length(256);
                prop.NotNullable(false);
            });
            Property(e => e.TwoFactorEnabled, prop => {
                prop.Column("two_factor_enabled");
                prop.Type(NHibernateUtil.Boolean);
                prop.NotNullable(true);
            });
            Property(e => e.SecurityStamp, prop => {
                prop.Column("security_stamp");
                prop.Type(NHibernateUtil.String);
                prop.Length(64);
                prop.NotNullable(false);
            });
        }

    }

}
