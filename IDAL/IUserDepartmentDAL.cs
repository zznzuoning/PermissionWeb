using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    public interface IUserDepartmentDAL : IBaseDAL<UserDepartment>
    {
        bool DelByUserId(Guid id);
    }
}
