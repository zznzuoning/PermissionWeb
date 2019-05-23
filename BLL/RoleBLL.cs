using Entity;
using IDAL;
using Model.ViewModel.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class RoleBLL
    {
        IRoleDAL dal = DALFactory.DALFactory.GetRoleDAL();

        /// <summary>
        /// 根据用户Id获取角色信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public StringData GetRoleByUserId(Guid id)
        {
            try
            {
                string roles = string.Empty;
                string ids = string.Empty;
                List<Role> rolesInfo = dal.GetRoleByUserId(id);
                foreach (var role in rolesInfo)
                {
                    ids += role.Id + ",";
                    roles += role.RoleName + ",";
                }
                var stringData = new StringData
                {
                    Ids = !string.IsNullOrEmpty(ids) ? ids.Substring(0, ids.Length - 1) : ids,
                    Names = !string.IsNullOrEmpty(roles) ? roles.Substring(0, roles.Length - 1) : roles
                };
                return stringData;
            }
            catch (Exception)
            {

                throw new Exception("出现程序错误,请联系管理员！");
            }
        }
        /// <summary>
        /// 根据角色id获取所属用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<User> GetUserByRoleId<Tkey>(Guid id, Expression<Func<User, Tkey>> orderLambda, string order, int pageSize, int pageIndex, out int totalCount)
        {
            return dal.GetUserByRoleId(id, orderLambda, order, pageSize, pageIndex, out totalCount);
        }

        public Role Create(Role model)
        {
            try
            {
                var user = dal.Create(model);
                return user;
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

        public Role Find(Guid id)
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

        public IEnumerable<Role> Get()
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

        public IEnumerable<Role> Get<Tkey>(Expression<Func<Role, Tkey>> orderLambda, Expression<Func<Role, bool>> whereLambda, string order, int pageSize, int pageIndex, out int totalCount)
        {
            return dal.Get(orderLambda, whereLambda, order, pageSize, pageIndex, out totalCount);
        }

        public Role Update(Role model)
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
