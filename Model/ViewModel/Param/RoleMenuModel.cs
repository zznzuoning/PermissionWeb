using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ViewModel.Param
{
    public class RoleMenuModel
    {
        public Guid RoleId { get; set; }
       public List<Bm> MenuButtonIds { get; set; }
    }
    public class Bm {
        public Guid menuid { get; set; }
        public Guid? buttonid { get; set; }
    }
    
}
