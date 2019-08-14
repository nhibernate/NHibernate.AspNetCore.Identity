using System;

namespace WebTest.Entities {

    public class TodoItem {

        public virtual long Id { get; set; }

        public virtual string Title { get; set; }

        public virtual string Description { get; set; }

        public virtual bool Completed { get; set; }

        public virtual AppUser User { get; set; }

    }

}
