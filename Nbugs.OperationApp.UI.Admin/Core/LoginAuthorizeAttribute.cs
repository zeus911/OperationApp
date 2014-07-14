using System;
using System.Web;
using System.Web.Mvc;

namespace Nbugs.OperationApp.UI.Admin.Core
{
    public class LoginAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Session["Account"] == null)
            {
                httpContext.Response.StatusCode = 401;
                return false;
            }
            else
            {
                return true;
            }
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = new JsonResult
                {
                    Data = new { message = "登录超时，请重新登录后操作！" },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
                return;
            }
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext is null");
            }
            else
            {
                //string path = filterContext.HttpContext.Request.Path;
                filterContext.HttpContext.Response.Redirect("/Account/Login", true);
                //filterContext.HttpContext.Response.Write("登录超时，请重新<a target='_blank' href='/Account/Login'>登录</a>后操作！");
                //filterContext.HttpContext.Response.End();
            }
        }
    }
}