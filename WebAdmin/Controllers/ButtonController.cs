using BLL;
using Common;
using Entity;
using Entity.ViewModel.Param;
using Entity.ViewModel.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using WebAdmin.Models;

namespace WenAdmin.Controllers
{
    public class ButtonController : BaseAdminController
    {
        // GET: Button
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 获取所有按钮
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult GetButtonList(ButtonSearch model)
        {
            int totalCount = 0;
            Expression<Func<Button,int>> orderLambda = d => d.Sort;
            var whereLambda = PredicateBuilder.True<Button>();
            if (!string.IsNullOrEmpty(model.Name))
            {
                whereLambda = whereLambda.AndAlso(d => d.Name.Contains(model.Name));
            }
            var buttons= new ButtonBLL().Get(orderLambda,whereLambda,model.Order,model.Rows,model.Page,out totalCount);
            var buttonList = buttons.Select(d=>new ButtonList {
                Id=d.Id,
                Name=d.Name,
                Code=d.Code,
                Icon=d.Icon,
                Sort=d.Sort,
                UpdateDateTime=d.UpdateTime,
                UpdateBy=d.UpdateBy,
                Description=d.Description
            }).ToList();
            var result = new DataResult<ButtonList>
            {
                total=totalCount,
                rows=buttonList
            };
            return Json(result,JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetIconsList()
        {
            var iconsList = new ButtonBLL().GetIcons();
            var result = iconsList.Select(d=>new IconList {
                id=d.Id,
                text=d.IconCssInfo,
                iconCls=d.IconCssInfo
            }).ToList();
            return Json(result,JsonRequestBehavior.AllowGet);
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
        public ActionResult Creat(Button model)
        {
            var result = new Result();
            try
            {
                if (string.IsNullOrEmpty(model.Name))
                {
                    result.Msg = "按钮名称不能为空";
                    return Json(result);
                }
                model.Id = Guid.NewGuid();
                model.CreateBy = Users.AccountName;
                model.CreateTime = DateTime.Now;
                model.UpdateBy = Users.AccountName;
                model.UpdateTime = DateTime.Now;
                var button = new ButtonBLL().Create(model);
                if (button != null)
                {
                    result.Success = true;
                    result.Msg = "添加成功！";
                }
                else
                {
                    result.Msg = "添加失败";
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
        public ActionResult Update(Button model)
        {
            var result = new Result();
            try
            {
                if (string.IsNullOrEmpty(model.Name))
                {
                    result.Msg = "按钮名称不能为空";
                    return Json(result);
                }
                model.UpdateBy = Users.AccountName;
                model.UpdateTime = DateTime.Now;
                var button = new ButtonBLL().Update(model);
                if (button != null)
                {
                    result.Success = true;
                    result.Msg = "修改成功！";
                }
                else
                {
                    result.Msg = "修改失败！";
                }
                return Json(result);
            }
            catch (Exception ex)
            {
                result.Msg = string.Format("修改失败！{0}",ex.Message);
                return Json(result);
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DelButtonByIDs(string ids)
        {
            var result = new Result();
            try
            {
                if (string.IsNullOrEmpty(ids))
                {
                    result.Msg = "Id不能为空";
                    return Json(result);
                }
                var isSuccess = new ButtonBLL().Del(ids);
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
                result.Msg = string.Format("删除失败！{0}",ex.Message);
                return Json(result);
            }
        }
        /// <summary>
        ///  获取页面操作按钮权限
        /// </summary>
        /// <param name="KeyName">页面名称关键字</param>
        /// <param name="keyCode">菜单标识码</param>
        /// <returns></returns>
        public ActionResult GetUserAuthorizeButton(string keyName,string keyCode)
        {
            var baseButtons = new ButtonBLL().GetButtonByMenuCodeAndUserId(keyCode, Users.Id);
            var buttons = GetAuthorizeButtons(baseButtons, keyName);
            return Json(buttons,JsonRequestBehavior.AllowGet);
        }
    }
}