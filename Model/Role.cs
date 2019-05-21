using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    /// <summary>
    /// 角色表
    /// </summary>
    public class Role :Base
    {
        /// <summary>
        /// 角色名
        /// </summary>
        public string RoleName { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        public string Description { get; set; }

    }
}
