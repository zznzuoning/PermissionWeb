using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DALFactory;
using Entity;
using IDAL;
using System.Linq.Expressions;
using Entity.ViewModel.Result;

namespace BLL
{
    /// <summary>
    /// 用户（BLL）
    /// </summary>
    public class UserBLL
    {
        IUserDAL userDal = DALFactory.DALFactory.GetUserDAL();
        /// <summary>
        /// 获取用户
        /// </summary>
        public List<User> GetUserInfo(int pageSize, int pageIndex, out int totalCount)

        {
            //初始获取用户列表
            var users = userDal.Get();
            //用户总条数
            totalCount = users.Count();
            var user = users
                .OrderBy(d => d.CreateTime)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize);

            return user.ToList();
        }
        public IEnumerable<User> Get<Tkey>(Expression<Func<User, Tkey>> orderLambda, Expression<Func<User, bool>> whereLambda, string order,int pageSize, int pageIndex, out int totalCount)
        {
            var user = userDal.Get(orderLambda,whereLambda,order,pageSize,pageIndex,out totalCount);
            return user;
        }
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public User Create(User model)
        {
            try
            {
                return userDal.Create(model);
            }
            catch
            {
                throw new Exception("出现程序错误,请联系管理员！");
            }

        }

        /// <summary>
        /// 根据登录名获取用户
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public User GetUserByName(string Name)
        {
            try
            {
                var user = userDal.GetUserByName(Name);
                return user;
            }
            catch
            {
                throw new Exception("出现程序错误,请联系管理员！");
            }

        }

        /// <summary>
        /// 根据Id获取用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User GetUserById(Guid id)
        {
            try
            {
                var user = userDal.Find(id);
                return user;
            }
            catch
            {

                throw new Exception("出现程序错误,请联系管理员！");
            }
        }
        /// <summary>
        /// 验证账号密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool ValidateUser(User model)
        {
            try
            {
                return userDal.ValidateUser(model);
               
            }
            catch (Exception)
            {

                throw new Exception("出现程序错误,请联系管理员！");
            }
        }
       
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public User UpdateUser(User model)
        {
            try
            {
                return userDal.Update(model);
            }
            catch
            {

                throw new Exception("出现程序错误,请联系管理员！");
            }

        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdatePwd(User model)
        {
            try
            {
                return userDal.UpdatePwd(model);
            }
            catch
            {

                throw new Exception("出现程序错误,请联系管理员！");
            }

        }
        
    }
}
