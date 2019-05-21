using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
namespace IDAL
{
    /// <summary>
    /// 用户接口（不同的数据库访问类实现接口达到多数据库的支持）
    /// </summary>
    public interface IUserDAL : IBaseDAL<User>
    {
      
    }
}
