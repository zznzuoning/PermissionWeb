using BLL;
using Common;
using Entity;
using Model.ViewModel.Param;
using Model.ViewModel.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using WebAdmin.Models;

namespace WenAdmin.Controllers
{
    public class RoleController : Controller
    {
        // GET: Role
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult GetRoleList(RoleSearch model)
        {
            int totalCount = 0;
            var whereLambda = PredicateBuilder.True<Role>();
            Expression<Func<Role, DateTime>> orderLambda = d => d.UpdateTime;
            if (!string.IsNullOrEmpty(model.Name))
            {
                whereLambda = whereLambda.AndAlso(d => d.RoleName.Contains(model.Name));
            }
            var roles = new RoleBLL().Get(orderLambda, whereLambda, model.Order, model.Rows, model.Page, out totalCount);
            var roleList = roles.Select(d => new RoleList
            {
                Id = d.Id,
                Name = d.RoleName,
                UpdateTime = d.UpdateTime,
                UpdateBy = d.UpdateBy,
                Description = d.Description
            }).ToList();
            var result = new DataResult<RoleList>
            {
                total = totalCount,
                rows = roleList
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 根据角色id获取所属的用户列表
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult GetUserListByRoleId(Guid Id, BaseSearch model)
        {
            int totalCount = 0;
            Expression<Func<User, DateTime>> orderLambda = d => d.UpdateTime;
            var users = new RoleBLL().GetUserByRoleId(Id, orderLambda, model.Order, model.Rows, model.Page, out totalCount);
            var userList = users.Select(d => new UserListByRoleId
            {
                Id = d.Id,
                Name = d.AccountName,
                UserName = d.RealName,
                IsAble = d.IsAble,
                IsEidtPass = d.IsChangePwd,
                CreatTime = d.CreateTime
            }).ToList();
            var result = new DataResult<UserListByRoleId>
            {
                total = totalCount,
                rows = userList
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        public ActionResult Creat()
        {
            return View();
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Creat(Role model)
        {
            var result = new Result();
            try
            {
                if (string.IsNullOrEmpty(model.RoleName))
                {
                    result.Msg = "角色名称不能为空";
                    return Json(result);
                }
                model.Id = Guid.NewGuid();
                model.CreateBy = "admin";
                model.CreateTime = DateTime.Now;
                model.UpdateBy = "admin";
                model.UpdateTime = DateTime.Now;
                var role = new RoleBLL().Create(model);
                if (role != null)
                {
                    result.Msg = "添加成功！";
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
        /// 修改
        /// </summary>
        /// <returns></returns>
        public ActionResult Update()
        {
            return View();
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Update(Role model)
        {
            var result = new Result();
            try
            {
                if (model.Id == Guid.Empty)
                {
                    result.Msg = "角色Id不能为空";
                    return Json(result);
                }
                if (string.IsNullOrEmpty(model.RoleName))
                {
                    result.Msg = "角色名称不能为空";
                    return Json(result);
                }
                model.UpdateBy = "admin";
                model.UpdateTime = DateTime.Now;
                var role = new RoleBLL().Update(model);
                if (role != null)
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
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Del(Guid id)
        {
            var result = new Result();
            try
            {
                if (id == Guid.Empty)
                {
                    result.Msg = "角色Id不能为空";
                    return Json(result);
                }
                var isSuccess = new RoleBLL().Del(id);
                if (isSuccess)
                {
                    result.Msg = "删除成功！";
                    result.Success = true;
                }
                else
                {
                    result.Msg = "删除失败！";
                }
                return Json(result);
            }
            catch (Exception ex)
            {

                result.Msg = string.Format("删除失败！{0}", ex.Message);
                return Json(result);
            }
        }
        /// <summary>
        /// 角色授权
        /// </summary>
        /// <returns></returns>
        public ActionResult RoleMenu()
        {
            return View();
        }


        /// <summary>
        /// 角色授权
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RoleMenu(RoleMenuModel model)
        {
            var result = new Result();
            try
            {
                if (model.RoleId == Guid.Empty)
                {
                    result.Msg = "角色Id不能为空";
                    return Json(result);
                }
                var isSuceess = new RoleBLL().Authorize(model);
                if (isSuceess)
                {
                    result.Msg = "授权成功！";
                    result.Success = true;
                }
                else
                {
                    result.Msg = "授权失败！";
                }
                return Json(result);
            }
            catch (Exception ex)
            {

                result.Msg = string.Format("授权失败！{0}", ex.Message);
                return Json(result);
            }
        }
    }
}