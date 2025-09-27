using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daitan.Data.Dao.PagedData
{
    public class PagedRecords<T> where T : class
    {
        public int TotalCount { get; set; }
        public List<T> Items { get; set; }
    }

    public class PagedRecordsParams
    {
        public int PageNo { get; set; }
        public int PageSize { get; set; }
    }

}
