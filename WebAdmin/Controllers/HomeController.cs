using BLL;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAdmin.Models;

namespace WenAdmin.Controllers
{
    public class HomeController : BaseAdminController
    {
        // GET: Home
        public ActionResult Index()
        {
            if (Users==null)
            {
                return RedirectToAction("Index", "Login");
            }
            ViewBag.RealName = Users.RealName;
            ViewBag.TimeView = DateTime.Now.ToLongDateString();
            ViewBag.DayDate = CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(DateTime.Now.DayOfWeek);
            return View();
        }
        /// <summary>
        /// 获取导航菜单
        /// </summary>
        /// <param name="Id">父id</param>
        /// <returns></returns>
        public ActionResult GetTreeByEasyui(Guid? Id)
        {
            var result = new MenuBLL().GetUserMenuByUserId(Users.Id, Id);
            return Json(result,JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 检查是否第一次要修改密码
        /// </summary>
        /// <returns></returns>
        public ActionResult CheckIsChangePwd()
        {
            var result = new DataResults<bool>();
            result.Success = true;
            result.Data = Users.IsChangePwd;
            return Json(result);
        }
    }
}