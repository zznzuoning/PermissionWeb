using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    /// <summary>
    /// 用户部门表
    /// </summary>
    public class UserDepartment
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid DepartmentId { get; set; }
        public virtual User Users { get; set; }
        public virtual Department Departments { get; set; }
    }
}
