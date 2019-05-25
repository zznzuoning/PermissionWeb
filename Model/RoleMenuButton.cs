using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    /// <summary>
    /// 角色菜单按钮表
    /// </summary>
    public class RoleMenuButton
    {
        public Guid Id { get; set; }
        public Guid RoleId { get; set; }
        public Guid MenuId { get; set; }
        public Guid? ButtonId { get; set; }
    }
}
