using AMS.DataTransferObjects.CookieHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AMS.ActionFilters
{
    public class AMSACtionFilters : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            CookieVM cookieData = CookieHelper.GetAllValues();
            if (cookieData.UserId == 0)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary {{ "Controller", "Account" },
                                      { "Action", "Login" } });
            }
            else if (cookieData.UserName == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary {{ "Controller", "Account" },
                                      { "Action", "Login" } });
            }
            else
            {
                if (cookieData.UserIsAdmin != true && filterContext.RouteData.Values["action"].ToString() == "AdminPanel")
                {
                    filterContext.Result = new RedirectToRouteResult(
                   new RouteValueDictionary {{ "Controller", "Home" },
                                      { "Action", "Index" } });
                }
            }
            base.OnActionExecuting(filterContext);
        }
    }
}