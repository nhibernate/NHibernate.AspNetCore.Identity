using NHibernate.Mapping.Attributes;

namespace WebTest.Entities {

    [Class(Schema = "public", Table = "cities")]
    public class City {

        [Id(Name = "Id", Column = "id", Type = "long", Generator = "trigger-identity")]
        public virtual long Id { get; set; }

        [Property(Column = "name", Type = "string", Length = 64, NotNull = true, Unique = true)]
        public virtual string Name { get; set; }

        [Property(Column = "population", Type = "int", NotNull = false)]
        public virtual int Population { get; set; }

    }

}
