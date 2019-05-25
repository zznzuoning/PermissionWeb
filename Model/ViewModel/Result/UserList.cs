using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ViewModel.Result
{
    public class UserList
    {
        public Guid Id { get; set; }
        public string AccountName { get; set; }
        public string UserName { get; set; }
        public string RoleId { get; set; }
        public string Role { get; set; }
        public string DepartmentId { get; set; }
        public string Department { get; set; }
        public string MobilePhone { get; set; }
        public string Email { get; set; }
        public bool IsAble { get; set; }
        public bool IsEdit { get; set; }
        public DateTime UpdateTime { get; set; }
        public string UpdateBy { get; set; }
    }
}
