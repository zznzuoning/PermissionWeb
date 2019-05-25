using BLL;
using Common;
using Entity;
using Entity.ViewModel.Param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebAdmin.Models;

namespace WenAdmin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            
            return View();
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(UserLogin model)
        {
            var result = new Result();
            try
            {
                if (string.IsNullOrEmpty(model.UserName))
                {
                    result.Msg = "用户名不能为空！";
                    return Json(result);
                }
                if (string.IsNullOrEmpty(model.PassWord))
                {
                    result.Msg = "密码不能为空！";
                    return Json(result);
                }
                var userLogin = new User { AccountName = model.UserName, PassWord = Md5.GetMD5String(model.PassWord) };

                if (!new UserBLL().ValidateUser(userLogin))
                {
                    result.Msg = "用户名或者密码错误！";
                    return Json(result);
                }
                var user = new UserBLL().GetUserByName(model.UserName);
                if (!user.IsAble)
                {
                    result.Msg = "此用户已禁用！";
                    return Json(result);
                }
                CookiesHelper.SetCookie("UserID", AES.EncryptStr(user.Id.ToString()));
                result.Success = true;
                return Json(result);
            }
            catch 
            {

                result.Msg = "登录超时！";
                return Json(result);
            }
        }
        /// <summary>
        /// 退出系统
        /// </summary>
        /// <returns></returns>
        public ActionResult LogOut()
        {
            CookiesHelper.AddCookie("UserID", DateTime.Now.AddDays(-1));
            return Json(new Result {
                Success=true,
                Msg="成功！"
            });
        }
    }
}