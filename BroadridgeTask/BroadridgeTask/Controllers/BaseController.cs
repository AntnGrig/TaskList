using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BroadridgeTask.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnException(ExceptionContext filterContext)
        {
            string msgErr;

            if (filterContext == null)
            {
                return;
            }

            if (!filterContext.HttpContext.IsCustomErrorEnabled)
            {
                return;
            }

            Exception ex = filterContext.Exception;
            msgErr = ex.Message;

            bool ajaxRequest = filterContext.HttpContext.Request.Headers["Ajax"] == "true" ? true : false;
            filterContext.ExceptionHandled = true;

            if (ajaxRequest)
            {
                filterContext.HttpContext.Response.StatusCode = 500;
            }
            else
            {                
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Error" }, { "action", "ErrorCommon" }, { "errorMessage", msgErr } });
            }

            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
        }
    }
}