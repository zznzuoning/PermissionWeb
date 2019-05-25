using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ViewModel.Result
{
    public class UserListByRoleId
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public bool IsAble { get; set; }
        public bool IsEidtPass { get; set; }
        public DateTime CreatTime { get; set; }
    }
}
