using System;
using System.Collections.Generic;

namespace WebTest.Models {

    public class PagedRequestModel {
        public int Take { get; set; } = 10;
        public int Skip { get; set; } = 0;
    }

    public class PagedResultModel<T> : PagedRequestModel {
        public long Total { get; set; }
        public IList<T> Data { get; set; }
    }

}
