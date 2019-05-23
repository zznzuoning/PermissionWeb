using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    public interface IRoleDAL : IBaseDAL<Role>
    {
        List<Role> GetRoleByUserId(Guid id);

        List<User> GetUserByRoleId<Tkey>(Guid id, Expression<Func<User, Tkey>> orderLambda, string order, int pageSize, int pageIndex, out int totalCount);
    }
}
