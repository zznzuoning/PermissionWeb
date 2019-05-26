using Entity;
using Entity.ViewModel.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WenAdmin.Filter;

namespace WenAdmin.Controllers
{
    [AuthorizeFilter]
    public class BaseAdminController : Controller
    {
        public User Users
        {
            get
            {
                var user = ViewData["UserData"] as User;
                return user;
            }
        }
        /// <summary>
        /// 获取页面操作按钮权限
        /// </summary>
        /// <param name="keyName">页面名称关键字</param>
        /// <returns></returns>
        public List<AuthorizeButton> GetAuthorizeButtons(List<Button> buttons, string keyName)
        {
            var list = new List<AuthorizeButton>();
            foreach (var button in buttons)
            {
                var authorizeButton = new AuthorizeButton();
                authorizeButton.text = button.Name;
                authorizeButton.iconCls = button.Icon;
                switch (button.Code)
                {
                    case "add":
                        authorizeButton.handler = string.Format("Add{0}()", keyName);
                        break;
                    case "save":
                        authorizeButton.handler = string.Format("Update{0}()", keyName);
                        break;
                    case "cut":
                        authorizeButton.handler = string.Format("Del{0}()", keyName);
                        break;
                    default:
                        authorizeButton.handler = button.Code +"()";
                        break;
                }
                list.Add(authorizeButton);
            }
            return list;
        }
    }
   
}