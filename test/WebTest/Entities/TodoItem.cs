using NHibernate.Mapping.Attributes;

namespace WebTest.Entities {

    [Class(Schema = "public", Table = "todo_items")]
    public class TodoItem {

        [Id(Name = "Id", Column = "id", Type = "long", Generator = "trigger-identity")]
        public virtual long Id { get; set; }

        [Property(Column = "title", Type = "string", Length = 32, NotNull = true, Unique = true)]
        public virtual string Title { get; set; }

        [Property(Column = "description", Type = "string", Length = 128, NotNull = false)]
        public virtual string Description { get; set; }

        [Property(Column = "completed", Type = "bool", NotNull = true)]
        public virtual bool Completed { get; set; }

        [ManyToOne(Column = "user_id", NotFound = NotFoundMode.Ignore)]
        public virtual AppUser User { get; set; }

    }

}
