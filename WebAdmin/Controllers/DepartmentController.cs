using BLL;
using Entity;
using Entity.ViewModel.Param;
using Entity.ViewModel.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAdmin.Models;

namespace WenAdmin.Controllers
{
    public class DepartmentController : BaseAdminController
    {
        // GET: Department
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 根据部门id获取用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult GetUserByDepartmentId(GetUserByDepartmentId model)
        {
            int total = 0;
            var users = new DepartmentBLL().GetUserByDepartmenId(model.Ids, model.Rows, model.Page, out total);
            var userList = users.Select(d => new UserListByRoleId
            {
                Id = d.Id,
                Name = d.AccountName,
                UserName = d.RealName,
                CreatTime = d.CreateTime,
                IsAble = d.IsAble,
                IsEidtPass = d.IsChangePwd
            }).ToList();
            return Json(new DataResult<UserListByRoleId> { total = total, rows = userList }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 添加部门
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Department model)
        {
            var result = new Result();
            try
            {
                if (string.IsNullOrEmpty(model.Name))
                {
                    result.Msg = "部门名称不能为空";
                    return Json(result);
                }
                if (new DepartmentBLL().GetDepartmentByName(model.Name))
                {
                    result.Msg = "部门已存在，请勿重复创建";
                    return Json(result);
                }
                model.Id = Guid.NewGuid();
                model.CreateBy = Users.AccountName;
                model.CreateTime = DateTime.Now;
                model.UpdateBy = Users.AccountName;
                model.UpdateTime = DateTime.Now;
                var department = new DepartmentBLL().Create(model);
                if (department != null)
                {
                    result.Success = true;
                    result.Msg = "添加成功!";
                }
                else
                {
                    result.Msg = "添加失败！";
                }
                return Json(result);
            }
            catch (Exception ex)
            {
                result.Msg = string.Format("添加失败！{0}",ex.Message);
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
        public ActionResult Update(Department model)
        {
            var result = new Result();
            try
            {
                if (model.Id == Guid.Empty)
                {
                    result.Msg = "部门Id不能为空";
                    return Json(result);
                }
                if (string.IsNullOrEmpty(model.Name))
                {
                    result.Msg = "部门名称不能为空";
                    return Json(result);
                }
                if (new DepartmentBLL().GetDepartmentByName(model.Name))
                {
                    result.Msg = "部门已存在，请勿重复创建";
                    return Json(result);
                }
               
                model.UpdateBy = Users.AccountName;
                model.UpdateTime = DateTime.Now;
                var department = new DepartmentBLL().Update(model);
                if (department != null)
                {
                    result.Success = true;
                    result.Msg = "修改成功!";
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