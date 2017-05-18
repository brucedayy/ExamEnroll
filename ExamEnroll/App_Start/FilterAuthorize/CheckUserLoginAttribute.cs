using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExamEnroll.App_Start.FilterAuthorize
{
    public class CheckUserLoginAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Session["username"] == null)
            {
                httpContext.Response.StatusCode = 401;//无权限状态码  
                return false;
            }
            return true;
        }



        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);
            if (filterContext.HttpContext.Response.StatusCode == 401)
            {
                filterContext.Result = new RedirectResult("/Home/Login");
            }
        }
    }
}