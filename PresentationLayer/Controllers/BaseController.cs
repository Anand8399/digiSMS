using PresentationLayer.Attributes;
using PresentationLayer.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PresentationLayer.Controllers
{
    [AuthorizedAttribute]
    [HandleError]
   
    public class BaseController : Controller, IActionFilter, IExceptionFilter
    {
        /// <summary>
        /// Called before the action method is invoked.
        /// </summary>
        /// <param name="filterContext">Information about the current request and action.</param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            this.Log("OnActionExecuting", filterContext.RouteData);
            CheckUserId(filterContext);
            base.OnActionExecuting(filterContext);
        }
        /// <summary>
        /// Called after the action method is invoked.
        /// </summary>
        /// <param name="filterContext">Information about the current request and action.</param>
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            this.Log("OnActionExecuted", filterContext.RouteData);
            base.OnActionExecuted(filterContext);
        }


        /// <summary>
        /// handle exceptions
        /// </summary>
        /// <param name="filterContext">filter Context</param>
        protected override void OnException(ExceptionContext filterContext)
        {
            Exception e = filterContext.Exception;
            ViewBag.Description = filterContext.Exception.Message;
            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();
            var Result = this.View("Error", new HandleErrorInfo(e,
            filterContext.RouteData.Values["controller"].ToString(),
            filterContext.RouteData.Values["action"].ToString()));
            filterContext.Result = Result;
            this.Session.RemoveAll();
            this.Session.Clear();
            this.Session.Abandon();
            //System.Web.Security.FormsAuthentication.SignOut();
            //string[] allCookies = Request.Cookies.AllKeys;
            //foreach (string cookie in allCookies)
            //{
            //    Response.Cookies[cookie].Expires = DateTime.Now.AddDays(-1);
            //}
        }

        /// <summary>
        /// Log Messages to base Logger
        /// </summary>
        /// <param name="methodName">Controller name</param>
        /// <param name="routeData">RouteData object</param>

        private void Log(string methodName, RouteData routeData)
        {
            var controllerName = routeData.Values["controller"];
            var actionName = routeData.Values["action"];
            var message = string.Format("{0} controller:{1} action:{2}", methodName, controllerName, actionName);
            //Log.Write(message, "Action Filter Log"); Write Logger implemetation
        }

        // If true, that means the user's password expired 
        // Let's force him to change his password before using the system 
        private void CheckUserId(ActionExecutingContext filterContext)
        {
            if (System.Web.HttpContext.Current.Session["UserId"] == null)
            {
                //this.TempData["AlertMessage"] = "Password has been expired.Please change Password!";
                filterContext.Result = new RedirectResult("~/LogIn/LogOff");
            }
        }

        protected override void ExecuteCore()
        {
            //PresentationLayer.Helpers.SessionHelper.CurrentCulture = 2;
            //int culture = 0;
            //if (this.Session == null)
            //{

            //    int.TryParse(System.Configuration.ConfigurationManager.AppSettings["Culture"], out culture);
            //    PresentationLayer.Helpers.SessionHelper.CurrentCulture = culture;
            //}
            //else
            //{
            //    culture = (int)PresentationLayer.Helpers.SessionHelper.CurrentCulture;
            //}
            //// calling CultureHelper class properties for setting  
            //CultureHelper.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(System.Configuration.ConfigurationManager.AppSettings["Culture"].ToString());
            Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture;

            base.ExecuteCore();
        }

        protected override bool DisableAsyncSupport
        {
            get { return true; }
        }
	}
}