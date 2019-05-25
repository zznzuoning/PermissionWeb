using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ViewModel.Result
{
    public class MenuButtonList
    {
        public Guid MenuId { get; set; }
        public string MenuName { get; set; }
        public Guid? ParentId { get; set; }
        public string MenuIcon { get; set; }
        public Guid? ButtonId { get; set; }
        public string ButtonName { get; set; }
        public string ButtonIcon { get; set; }
        public Guid? RoleId { get; set; }
        public bool Checked { get; set; }
    }
}
