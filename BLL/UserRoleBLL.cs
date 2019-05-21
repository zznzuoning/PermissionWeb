using Entity;
using IDAL;
using Model.ViewModel.Param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class UserRoleBLL
    {
        IUserRoleDAL dal = DALFactory.DALFactory.GetUserRoleDAL();
        IUserDAL userDal = DALFactory.DALFactory.GetUserDAL();

        /// <summary>
        /// 添加用户角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Create(UserRoleModel model)
        {
            try
            {
                var userIds = model.UserIds.Split(',');
                var roleIds = model.RoleIds.Split(',');
                foreach (var id in userIds)
                {
                    var gid = Guid.Parse(id);
                    if (userDal.Find(gid) == null)
                    {
                        throw new Exception("无法识别用户Id");
                    }
                    else
                    {
                        var isScussce=dal.DelByUserId(gid);
                        if (!isScussce)
                        {
                            throw new Exception("出现程序错误,请联系管理员！");
                        }
                    }
                }
                foreach (var userId in userIds)
                {
                    var guserId = Guid.Parse(userId);
                    foreach (var roleId in roleIds)
                    {
                        var groleId = Guid.Parse(roleId);
                        var userRole = new UserRole
                        {
                            Id = Guid.NewGuid(),
                            UserId = guserId,
                            RoleId = groleId
                        };
                       var result= dal.Create(userRole);
                        if (result == null)
                        {
                            throw new Exception("循环添加出现程序错误,请联系管理员！");
                        }
                    }
                }
                return true;
            }
            catch (Exception)
            {

                throw new Exception("出现程序错误,请联系管理员！");
            }
        }

        public bool Del(Guid id)
        {
            try
            {
                return dal.Del(id); ;
            }
            catch (Exception)
            {

                throw new Exception("出现程序错误,请联系管理员！");
            }
        }

        public UserRole Find(Guid id)
        {
            try
            {
                return dal.Find(id); ;
            }
            catch (Exception)
            {

                throw new Exception("出现程序错误,请联系管理员！");
            }
        }

        public IEnumerable<UserRole> Get()
        {
            try
            {
                return dal.Get();
            }
            catch (Exception)
            {

                throw new Exception("出现程序错误,请联系管理员！");
            }
        }

        public IEnumerable<UserRole> Get<Tkey>(Expression<Func<UserRole, Tkey>> orderLambda, Expression<Func<UserRole, bool>> whereLambda, string order, int pageSize, int pageIndex, out int totalCount)
        {
            throw new NotImplementedException();
        }

        public UserRole Update(UserRole model)
        {
            try
            {
                return dal.Update(model);
            }
            catch (Exception)
            {

                throw new Exception("出现程序错误,请联系管理员！");
            }
        }
    }
}
