using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    /// <summary>
    /// 菜单按钮表
    /// </summary>
    public class MenuButton
    {
        public Guid Id { get; set; }
        public Guid MenuId { get; set; }
        public Guid ButtonId { get; set; }
        public virtual Menu Menus { get; set; }
        public virtual Button Buttons { get; set; }
    }
}
