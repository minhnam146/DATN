using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using DATN.Models;

namespace DATN.App_Start
{
    public class CheckSession: AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            Account accSession = (Account)HttpContext.Current.Session["user"];
            if(accSession != null)
            {
                return;
            }
            else
            {
                var returnUrl = filterContext.RequestContext.HttpContext.Request.RawUrl;
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(
                        new
                        {
                            controller = "HomeAdmin",
                            action = "DangNhap",
                            area = "Admin",
                            returnUrl = returnUrl.ToString()
                        }));
            }
        }
    }
}