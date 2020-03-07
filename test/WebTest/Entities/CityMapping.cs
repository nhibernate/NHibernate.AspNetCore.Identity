using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace WebTest.Entities {

    public class CityMapping : ClassMapping<City> {

        public CityMapping() {
            Schema("public");
            Table("cities");
            Id(
                e => e.Id,
                mapping => {
                    mapping.Column("id");
                    mapping.Type(NHibernateUtil.Int64);
                    mapping.Generator(Generators.TriggerIdentity);
                }
            );
            Property(
                e => e.Name,
                mapping => {
                    mapping.Column("name");
                    mapping.Type(NHibernateUtil.String);
                    mapping.Length(64);
                    mapping.NotNullable(true);
                    mapping.Unique(true);
                }
            );
            Property(
                e => e.Population,
                mapping => {
                    mapping.Column("population");
                    mapping.Type(NHibernateUtil.Int32);
                    mapping.NotNullable(true);
                    mapping.Unique(true);
                }
            );
        }

    }

}
