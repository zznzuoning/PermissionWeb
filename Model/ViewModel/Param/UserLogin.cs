using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ViewModel.Param
{
    public class UserLogin
    {
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public bool IsAutoLogin { get; set; }
    }
}
