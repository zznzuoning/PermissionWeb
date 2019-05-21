using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    /// <summary>
    /// 菜单表
    /// </summary>
    public class Menu : Base
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 父类Id
        /// </summary>
        public Guid? ParentId { get; set; }
        /// <summary>
        /// 标识码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 链接地址
        /// </summary>
        public string LinkAddress { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
    }
}
