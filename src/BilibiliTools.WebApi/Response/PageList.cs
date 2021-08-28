using System.Collections.Generic;

namespace BilibiliTools.Web.Response
{
    public class PageList<T>
    {
        public int TotalCount { get; set; }

        public List<T> Item { get; set; }
    }
}