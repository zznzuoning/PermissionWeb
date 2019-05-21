using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel.Param
{
    public class BaseSearch
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// 页大小
        /// </summary>
        public int Rows { get; set; }
        /// <summary>
        /// 排序字段
        /// </summary>
        public string Sort { get; set; }
        /// <summary>
        /// 排序方式（asc,desc）
        /// </summary>
        public string Order { get; set; }
    }
}
