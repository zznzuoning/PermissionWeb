using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ViewModel.Result
{
    public class UserMenuList
    {
        public Guid MenuId { get; set; }
        public string MenuName { get; set; }
        public string LinkAddress { get; set; }
        public string Icon { get; set; }
        public int Sort { get; set; }
    }
}
