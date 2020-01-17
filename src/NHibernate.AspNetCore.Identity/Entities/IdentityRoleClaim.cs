using Microsoft.AspNetCore.Identity;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace NHibernate.AspNetCore.Identity {

    public class IdentityRoleClaim : IdentityRoleClaim<string> { }

    public class IdentityRoleClaimMappingPostgreSql : ClassMapping<IdentityRoleClaim> {

        public IdentityRoleClaimMappingPostgreSql() {
            Schema("public");
            Table("aspnet_role_claims");
            Id(e => e.Id, id => {
                id.Column("id");
                id.Type(NHibernateUtil.Int32);
                id.Length(32);
                id.Generator(Generators.Sequence, g => {
                    g.Params(new { sequence = "snow_flake_id_seq" });
                });
            });
            Property(e => e.ClaimType, prop => {
                prop.Column("claim_type");
                prop.Type(NHibernateUtil.String);
                prop.Length(1024);
                prop.NotNullable(true);
            });
            Property(e => e.ClaimValue, prop => {
                prop.Column("claim_value");
                prop.Type(NHibernateUtil.String);
                prop.Length(1024);
                prop.NotNullable(true);
            });
            Property(e => e.RoleId, prop => {
                prop.Column("role_id");
                prop.Type(NHibernateUtil.String);
                prop.Length(32);
                prop.NotNullable(true);
            });
        }

    }

    public class IdentityRoleClaimMappingMsSql : ClassMapping<IdentityRoleClaim> {

        public IdentityRoleClaimMappingMsSql() {
            Schema("dbo");
            Table("IdentityRoleClaim");
            Id(e => e.Id, id => {
                id.Column("Id");
                id.Type(NHibernateUtil.Int32);
                id.Generator(Generators.Identity);
            });
            Property(e => e.ClaimType, prop => {
                prop.Column("ClaimType");
                prop.Type(NHibernateUtil.String);
                prop.Length(1024);
                prop.NotNullable(true);
            });
            Property(e => e.ClaimValue, prop => {
                prop.Column("ClaimValue");
                prop.Type(NHibernateUtil.String);
                prop.Length(1024);
                prop.NotNullable(true);
            });
            Property(e => e.RoleId, prop => {
                prop.Column("RoleId");
                prop.Type(NHibernateUtil.String);
                prop.Length(32);
                prop.NotNullable(true);
            });
        }

    }
}
