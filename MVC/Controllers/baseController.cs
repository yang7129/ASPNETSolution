using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NLog;
namespace MVC.Controllers
{
    public class baseController : Controller
    {
        // GET: base
        //public ActionResult Index()
        //{
        //    return View();
        //}
        string Url = "";
        string controller = "";
        string action = "";
        string area = "";

        protected override void OnException(ExceptionContext filterContext)
        {
            String Errormsg = String.Empty;
            Exception unhandledException = new Exception("BASE ERROR");
            if (Server.GetLastError() != null)
                unhandledException = Server.GetLastError();
            else
                unhandledException = filterContext.Exception;
            Exception httpException = unhandledException as Exception;
            Errormsg = "發生例外網頁:{0}錯誤訊息:{1}";
            if (httpException != null /*&& !httpException.GetType().IsAssignableFrom(typeof(HttpException))*/)
            {
                Errormsg = String.Format(Errormsg, Request.Path + Environment.NewLine,unhandledException.GetBaseException().ToString() + Environment.NewLine);

                Url = System.Web.HttpContext.Current.Request.Url.PathAndQuery;
                controller = Convert.ToString(ControllerContext.RouteData.Values["controller"]);
                action = Convert.ToString(ControllerContext.RouteData.Values["action"]);
                area = Convert.ToString(ControllerContext.RouteData.DataTokens["area"]);
                NLog.Logger logger = LogManager.GetCurrentClassLogger();
                logger.Error(Url + "|controller|" + controller + "|action|" + action + "|area|" + area + "|" + Errormsg);

            }
            base.OnException(filterContext); 
            filterContext.ExceptionHandled = true;
             

        }
    }
}