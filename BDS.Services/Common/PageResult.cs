using System.Collections.Generic;

namespace BDS.Services.Common
{
    public class PageResult<T>
    {
        public List<T> Items { get; set; }
        public int totalRecord { get; set; }
    }
}