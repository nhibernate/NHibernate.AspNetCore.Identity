using System;

namespace WebTest.Models {

    /// <summary></summary>
    public class TodoItemModel {
        /// <summary> </summary>
        public string Id { get; set; }
        /// <summary>  </summary>
        public string Title { get; set; }
        /// <summary>  </summary>
        public string Description { get; set; }
        /// <summary>  </summary>
        public bool Completed { get; set; }
        /// <summary>  </summary>
        public string UserId { get; set; }
        /// <summary>  </summary>
        public string UserName { get; set; }
    }

    public class TodoItemSearchModel : PagedRequestModel {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool? Completed { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
    }

}
