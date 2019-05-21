using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
   public class Base
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 最后更新人
        /// </summary>
        public string UpdateBy { get; set; }

        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }
}
