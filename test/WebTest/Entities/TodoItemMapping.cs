using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace WebTest.Entities {

    public class TodoItemMapping : ClassMapping<TodoItem> {

        public TodoItemMapping() {
            Schema("public");
            Table("todo_items");
            Id(
                e => e.Id,
                mapping => {
                    mapping.Column("id");
                    mapping.Type(NHibernateUtil.Int64);
                    mapping.Generator(Generators.TriggerIdentity);
                }
            );
            Property(
                e => e.Title,
                mapping => {
                    mapping.Column("title");
                    mapping.Type(NHibernateUtil.String);
                    mapping.Length(32);
                    mapping.NotNullable(true);
                    mapping.Unique(true);
                }
            );
            Property(
                e => e.Description,
                mapping => {
                    mapping.Column("description");
                    mapping.Type(NHibernateUtil.String);
                    mapping.Length(128);
                    mapping.NotNullable(false);
                }
            );
            Property(
                e => e.Completed,
                mapping => {
                    mapping.Column("completed");
                    mapping.Type(NHibernateUtil.Boolean);
                    mapping.NotNullable(false);
                }
            );
            ManyToOne(
                e => e.User,
                mapping => {
                    mapping.Column("user_id");
                    mapping.NotFound(NotFoundMode.Ignore);
                }
            );
        }

    }
}
