using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Entity;
using WebAdmin.Models;
using Common;
using WenAdmin.Models.Param.User;
using Entity.ViewModel.Result;
using System.Linq.Expressions;
using Entity.ViewModel.Param;
using WenAdmin.Controllers;

namespace WebAdmin.Controllers
{
    public class UserController : BaseAdminController
    {
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 获取所有用户数据
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public ActionResult GetAllUserInfo(UserSearch search)
        {
            int totalCount = 0;

            Expression<Func<User, DateTime>> orderLambda = d => d.UpdateTime;
            var whereLambda = PredicateBuilder.True<User>();
            if (!string.IsNullOrEmpty(search.LogName))
            {
                whereLambda = whereLambda.AndAlso(d => d.AccountName.Contains(search.LogName));
            }
            if (!string.IsNullOrEmpty(search.UserName))
            {
                whereLambda = whereLambda.AndAlso(d => d.RealName.Contains(search.UserName));
            }
            if (!string.IsNullOrEmpty(search.IsUse) && search.IsUse != "select")
            {
                var isUses = bool.Parse(search.IsUse);
                whereLambda = whereLambda.AndAlso(d => d.IsAble == isUses);
            }
            if (!string.IsNullOrEmpty(search.IsEditPass) && search.IsEditPass != "select")
            {
                var IsEditPasss = bool.Parse(search.IsEditPass);
                whereLambda = whereLambda.AndAlso(d => d.IsChangePwd == IsEditPasss);
            }
            var user = new UserBLL().Get(orderLambda, whereLambda, search.Order, search.Rows, search.Page, out totalCount);
            var users = user.Select(d => new UserList
            {
                Id = d.Id,
                AccountName = d.AccountName,
                UserName = d.RealName,
                Role = new RoleBLL().GetRoleByUserId(d.Id).Names,
                RoleId = new RoleBLL().GetRoleByUserId(d.Id).Ids,
                DepartmentId = new DepartmentBLL().GetRoleByUserId(d.Id).Ids,
                Department = new DepartmentBLL().GetRoleByUserId(d.Id).Names,
                MobilePhone = d.MobilePhone,
                Email = d.Email,
                IsAble = d.IsAble,
                IsEdit = d.IsChangePwd,
                UpdateTime = d.UpdateTime,
                UpdateBy = d.UpdateBy
            }).ToList(); ;
            var result = new DataResult<UserList>
            {
                total = totalCount,
                rows = users
            };
            return Json(result);
        }
        /// <summary>
        /// 添加页面
        /// </summary>
        /// <returns></returns>
        public ActionResult CreatUser()
        {
            return View();
        }
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddUser(User model)
        {
            var result = new Result();
            try
            {

                if (string.IsNullOrEmpty(model.AccountName))
                {
                    result.Msg = "登录名不能为空";
                    return Json(result);
                }
                if (string.IsNullOrEmpty(model.RealName))
                {
                    result.Msg = "真实姓名不能为空";
                    return Json(result);
                }
                var isUser = new UserBLL().GetUserByName(model.AccountName);
                if (isUser != null)
                {
                    result.Msg = "登录名已存在";
                    return Json(result);
                }
                //6d3bd386-fe4d-4bc2-9a5d-1ee816caf65e
                var user = new User
                {
                    Id = Guid.NewGuid(),
                    AccountName = model.AccountName,
                    RealName = model.RealName,
                    PassWord = Md5.GetMD5String("q123456"),
                    MobilePhone = model.MobilePhone,
                    Email = model.Email,
                    IsAble = model.IsAble,
                    IsChangePwd = model.IsChangePwd,
                    Description = model.Description,
                    CreateTime = DateTime.Now,
                    CreateBy = Users.AccountName,
                    UpdateBy = Users.AccountName,
                    UpdateTime = DateTime.Now
                };
                var userInfo = new UserBLL().Create(user);
                if (userInfo != null)
                {
                    result.Msg = "操作成功！默认密码是【q123456】！";
                    result.Success = true;
                }
                else
                {
                    result.Msg = "添加失败！";
                }
                return Json(result);
            }
            catch (Exception ex)
            {
                result.Msg = string.Format("添加失败！{0}", ex.Message);
                return Json(result);
            }
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        public ActionResult UpdateUser()
        {
            return View();
        }
        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateUser(User model)
        {
            var result = new Result();
            try
            {
                if (model.Id == Guid.Empty)
                {
                    result.Msg = "标识不存在";
                    return Json(result);
                }
                if (string.IsNullOrEmpty(model.RealName))
                {
                    result.Msg = "真实姓名不能为空";
                    return Json(result);
                }
                if (string.IsNullOrEmpty(model.AccountName))
                {
                    result.Msg = "登录名不能为空";
                    return Json(result);
                }
                var user = new UserBLL().GetUserById(model.Id);
                if (user.AccountName != model.AccountName)
                {
                    result.Msg = "登录名不可修改";
                    return Json(result);
                }
                model.UpdateBy = Users.AccountName;
                model.UpdateTime = DateTime.Now;
                var userInfo = new UserBLL().UpdateUser(model);
                if (userInfo != null)
                {
                    result.Msg = "修改成功！";
                    result.Success = true;

                }
                else
                {
                    result.Msg = "修改失败！";
                }
                return Json(result);
            }
            catch (Exception ex)
            {
                result.Msg = string.Format("修改失败！{0}", ex.Message);
                return Json(result);
            }

        }

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetUserById(Guid id)
        {
            var result = new DataResults<User>();
            try
            {
                var user = new UserBLL().GetUserById(id);
                if (user == null)
                {
                    result.Msg = "用户不存在";
                    return Json(result);
                }
                else
                {
                    result.Success = true;
                    result.Data = user;
                    return Json(result, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                result.Msg = string.Format("加载失败！{0}", ex.Message);
                return Json(result);
            }
        }
        /// <summary>
        /// 获取所有角色
        /// </summary>
        /// <returns></returns>
        public ActionResult GetRoleInfo()
        {
            var role = new RoleBLL().Get();
            var result = role.
                Select(d => new ComboResult
                {
                    Id = d.Id,
                    Name = d.RoleName
                });
            return Json(result);
        }
        /// <summary>
        /// 获取所有部门
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDepartmentInfo()
        {
            var role = new DepartmentBLL().GetDepartments();
           
            return Json(role,JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 角色设置
        /// </summary>
        /// <returns></returns>
        public ActionResult UserRole()
        {
            return View();
        }


        /// <summary>
        /// 角色设置
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UserRole(UserRoleModel model)
        {
            var result = new Result();
            try
            {
              
                if (string.IsNullOrEmpty(model.UserIds))
                {
                    result.Msg = "UserId不能为空";
                    return Json(result);
                }
                if (string.IsNullOrEmpty(model.RoleIds))
                {
                    result.Msg = "RoleId不能为空";
                    return Json(result);
                }
                var isScussce = new UserRoleBLL().Create(model);
                if (isScussce)
                {
                    result.Msg = "设置成功！";
                    result.Success = true;
                }
                else
                {
                    result.Msg = "设置失败！";
                }
                return Json(result);
            }
            catch (Exception ex)
            {

                result.Msg = string.Format("设置失败！{0}", ex.Message);
                return Json(result);
            }
        }

        /// <summary>
        /// 部门设置
        /// </summary>
        /// <returns></returns>
        public ActionResult UserDepartment()
        {
            return View();
        }
        /// <summary>
        /// 部门设置
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UserDepartment(UserDepartmentModel model)
        {
            var result = new Result();
            try
            {
                if (string.IsNullOrEmpty(model.UserIds))
                {
                    result.Msg = "UserId不能为空";
                    return Json(result);
                }
                if (string.IsNullOrEmpty(model.DepartmentIds))
                {
                    result.Msg = "DepartmentId不能为空";
                    return Json(result);
                }
                var isScussce = new UserDepartmentBLL().Create(model);
                if (isScussce)
                {
                    result.Msg = "设置成功！";
                    result.Success = true;
                }
                else
                {
                    result.Msg = "设置失败！";
                }
                return Json(result);
            }
            catch (Exception ex)
            {
                result.Msg = string.Format("设置失败！{0}",ex.Message);
                return Json(result);
            }
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        public ActionResult ChangePwd()
        {
            return View();
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="newPwd"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdatePwd(string newPwd)
        {
            var result = new Result();
            try
            {
                var user = new User {
                    Id=Users.Id,
                    PassWord=Md5.GetMD5String(newPwd),
                    UpdateBy= Users.RealName
                };
                if (new UserBLL().UpdatePwd(user))
                {
                    result.Success = true;
                    result.Msg = "修改成功,请重新登录！";
                }
                else
                {
                    result.Msg = "修改失败！";
                }
                return Json(result);
            }
            catch (Exception ex)
            {

                result.Msg = string.Format("修改失败！{0}", ex.Message);
                return Json(result);
            }
           
        }
    }
}