using BLL;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WenAdmin.Filter
{
    public class AuthorizeFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string user_Id = AES.DecryptStr(CookiesHelper.GetCookieValue("UserID"));
            Guid guser_Id = Guid.Empty;
            if (Guid.TryParse(user_Id, out guser_Id))
            {
                var user = new UserBLL().GetUserById(guser_Id);
                if (user != null)
                {
                    filterContext.Controller.ViewData["UserData"] = user;
                    base.OnActionExecuting(filterContext);
                }
                else
                {
                    CookiesHelper.AddCookie("UserID", DateTime.Now.AddDays(-1));
                    filterContext.Result = new RedirectResult("/Login/Index");
                }
            }
            else
            {
                filterContext.Result = new RedirectResult("/Login/Index");
            }

        }
    }
}