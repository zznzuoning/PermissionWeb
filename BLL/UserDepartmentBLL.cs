using Entity;
using IDAL;
using Model.ViewModel.Param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class UserDepartmentBLL
    {
        IUserDepartmentDAL dal = DALFactory.DALFactory.GetUserDepartmentDAL();
        IUserDAL userDal = DALFactory.DALFactory.GetUserDAL();

        /// <summary>
        /// 添加用户部门
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Create(UserDepartmentModel model)
        {
            try
            {
                var userIds = model.UserIds.Split(',');
                var departmentIds = model.DepartmentIds.Split(',');
                foreach (var item in userIds)
                {
                    var gid = Guid.Parse(item);
                    var user = userDal.Find(gid);
                    if (user == null)
                    {
                        throw new Exception("无法识别用户ID");
                    }
                    else
                    {
                        var isScussce = dal.DelByUserId(gid);
                        if (!isScussce)
                        {
                            throw new Exception("出现程序错误,请联系管理员！");
                        }
                    }
                }
                foreach (var uid in userIds)
                {
                    var userId = Guid.Parse(uid);
                    foreach (var did in departmentIds)
                    {
                        var departmentId = Guid.Parse(did);
                        var userDepartment = new UserDepartment
                        {
                            Id = Guid.NewGuid(),
                            UserId = userId,
                            DepartmentId = departmentId
                        };
                        var result = dal.Create(userDepartment);
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
    }
}
