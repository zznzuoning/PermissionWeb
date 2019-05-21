﻿using BLL;
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
    public class MenuController : Controller
    {
        // GET: Menu
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult GetMenuList(MenuSearch model)
        {
            int totalCount = 0;
            Expression<Func<Menu, int>> orderLambda = d => d.Sort;
            var whereLambda = PredicateBuilder.True<Menu>();
            if (!string.IsNullOrEmpty(model.Name))
            {
                whereLambda = whereLambda.AndAlso(d => d.Name.Contains(model.Name));
            }
            var menus = new MenuBLL().Get(orderLambda, whereLambda, model.Order, model.Rows, model.Page, out totalCount);
            var menusList = menus.Select(d => new MenuList
            {
                Id = d.Id,
                Name = d.Name,
                ParentId=d.ParentId,
                ParentName = d.ParentId.HasValue ? new MenuBLL().Find(d.ParentId.Value).Name : "顶级菜单",
                Code = d.Code,
                LinkAddress = d.LinkAddress,
                Icon = d.Icon,
                Sort = d.Sort,
                UpdateBy = d.UpdateBy,
                UpdateTime = d.UpdateTime
            }).ToList();
            var result = new DataResult<MenuList>
            {
                total = totalCount,
                rows = menusList
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <returns></returns>
        public ActionResult GetMenuTree()
        {
            var treeData = new MenuBLL().GetMenuTreeList();
            return Json(treeData, JsonRequestBehavior.AllowGet);
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
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Creat(Menu model)
        {
            var result = new Result();
            try
            {
                if (string.IsNullOrEmpty(model.Name))
                {
                    result.Msg = "菜单名称不能为空";
                    return Json(result);
                }
                model.Id = Guid.NewGuid();
                model.CreateBy = "admin";
                model.CreateTime = DateTime.Now;
                model.UpdateBy = "admin";
                model.UpdateTime = DateTime.Now;
                var menu = new MenuBLL().Creat(model);
                if (menu != null)
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
        public ActionResult Update(Menu model)
        {
            var result = new Result();
            try
            {
                if (model.Id == Guid.Empty)
                {
                    result.Msg = "菜单Id不能为空";
                    return Json(result);
                }
                if (string.IsNullOrEmpty(model.Name))
                {
                    result.Msg = "菜单名称不能为空";
                    return Json(result);
                }
                model.UpdateBy = "admin";
                model.UpdateTime = DateTime.Now;
                var menu = new MenuBLL().Update(model);
                if (menu != null)
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
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Del(string ids)
        {
            var result = new Result();
            try
            {
                if (string.IsNullOrEmpty(ids))
                {
                    result.Msg = "菜单id不能为空";
                    return Json(result);
                }
                var isSuccess = new MenuBLL().Del(ids);
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
                result.Msg = string.Format("删除失败！", ex.Message);
                return Json(result);
            }
            return View();
        }

    }
}