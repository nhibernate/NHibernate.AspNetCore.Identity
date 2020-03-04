using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace NHibernate.AspNetCore.Identity.Mappings {

    public class IdentityRoleMappingPostgreSql : ClassMapping<IdentityRole> {

        public IdentityRoleMappingPostgreSql() {
            Schema("public");
            Table("aspnet_roles");
            Id(e => e.Id, id => {
                id.Column("id");
                id.Type(NHibernateUtil.String);
                id.Length(32);
                id.Generator(Generators.TriggerIdentity);
            });
            Property(e => e.Name, prop => {
                prop.Column("name");
                prop.Type(NHibernateUtil.String);
                prop.Length(64);
                prop.NotNullable(true);
                prop.Unique(true);
            });
            Property(e => e.NormalizedName, prop => {
                prop.Column("normalized_name");
                prop.Type(NHibernateUtil.String);
                prop.Length(64);
                prop.NotNullable(true);
                prop.Unique(true);
            });
            Property(e => e.ConcurrencyStamp, prop => {
                prop.Column("concurrency_stamp");
                prop.Type(NHibernateUtil.String);
                prop.Length(36);
                prop.NotNullable(false);
            });
        }

    }

    public class IdentityRoleMappingMsSql : ClassMapping<IdentityRole> {

        public IdentityRoleMappingMsSql() {
            Schema("dbo");
            Table("AspNetRoles");
            Id(e => e.Id, id => {
                id.Column("Id");
                id.Type(NHibernateUtil.String);
                id.Length(32);
                id.Generator(Generators.UUIDHex("N"));
            });
            Property(e => e.Name, prop => {
                prop.Column("Name");
                prop.Type(NHibernateUtil.String);
                prop.Length(64);
                prop.NotNullable(true);
                prop.Unique(true);
            });
            Property(e => e.NormalizedName, prop => {
                prop.Column("NormalizedName");
                prop.Type(NHibernateUtil.String);
                prop.Length(64);
                prop.NotNullable(true);
                prop.Unique(true);
            });
            Property(e => e.ConcurrencyStamp, prop => {
                prop.Column("ConcurrencyStamp");
                prop.Type(NHibernateUtil.String);
                prop.Length(36);
                prop.NotNullable(false);
            });
        }

    }

    public class IdentityRoleMappingMySql : ClassMapping<IdentityRole> {

        public IdentityRoleMappingMySql() {
            Table("aspnet_roles");
            Id(e => e.Id, id => {
                id.Column("id");
                id.Type(NHibernateUtil.String);
                id.Length(32);
                id.Generator(Generators.UUIDHex("N"));
            });
            Property(e => e.Name, prop => {
                prop.Column("name");
                prop.Type(NHibernateUtil.String);
                prop.Length(64);
                prop.NotNullable(true);
                prop.Unique(true);
            });
            Property(e => e.NormalizedName, prop => {
                prop.Column("normalized_name");
                prop.Type(NHibernateUtil.String);
                prop.Length(64);
                prop.NotNullable(true);
                prop.Unique(true);
            });
            Property(e => e.ConcurrencyStamp, prop => {
                prop.Column("concurrency_stamp");
                prop.Type(NHibernateUtil.String);
                prop.Length(36);
                prop.NotNullable(false);
            });
        }

    }

    public class IdentityRoleMappingSqlite : ClassMapping<IdentityRole> {

        public IdentityRoleMappingSqlite() {
            Table("aspnet_roles");
            Id(e => e.Id, id => {
                id.Column("id");
                id.Type(NHibernateUtil.String);
                id.Length(32);
                id.Generator(Generators.UUIDHex("N"));
            });
            Property(e => e.Name, prop => {
                prop.Column("name");
                prop.Type(NHibernateUtil.String);
                prop.Length(64);
                prop.NotNullable(true);
                prop.Unique(true);
            });
            Property(e => e.NormalizedName, prop => {
                prop.Column("normalized_name");
                prop.Type(NHibernateUtil.String);
                prop.Length(64);
                prop.NotNullable(true);
                prop.Unique(true);
            });
            Property(e => e.ConcurrencyStamp, prop => {
                prop.Column("concurrency_stamp");
                prop.Type(NHibernateUtil.String);
                prop.Length(36);
                prop.NotNullable(false);
            });
        }

    }

}
