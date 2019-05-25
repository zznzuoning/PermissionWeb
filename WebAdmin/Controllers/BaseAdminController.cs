using Entity;
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
    }
}