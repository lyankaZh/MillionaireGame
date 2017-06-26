using System;
using System.Web.Mvc;
using log4net;
using MillionaireGame.DAL;
using MillionaireGame.DAL.Entities;

namespace MillionaireGame.Infrastructure.Filters
{
    public class ExceptionLoggerAttribute : FilterAttribute, IExceptionFilter
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)
            {
                return;
            }
            
            log4net.LogicalThreadContext.Properties["ExceptionMessage"] = filterContext.Exception.Message;
            log4net.LogicalThreadContext.Properties["ControllerName"] = filterContext.RouteData.Values["controller"].ToString();
            log4net.LogicalThreadContext.Properties["ActionName"] = filterContext.RouteData.Values["action"].ToString();
            log4net.LogicalThreadContext.Properties["StackTrace"] = filterContext.Exception.StackTrace;
            log.Error("Error");

            //ExceptionDetail exceptionDetail = new ExceptionDetail()
            //{
            //    ExceptionMessage = filterContext.Exception.Message,
            //    StackTrace = filterContext.Exception.StackTrace,
            //    ControllerName = filterContext.RouteData.Values["controller"].ToString(),
            //    ActionName = filterContext.RouteData.Values["action"].ToString(),
            //    Date = DateTime.Now
            //};

            //using (MillionaireContext db = new MillionaireContext())
            //{
            //    db.ExceptionDetails.Add(exceptionDetail);
            //    db.SaveChanges();
            //}

            filterContext.ExceptionHandled = true;
            
            filterContext.Result = new ViewResult
            {
                ViewName = "ErrorView"
            };
        }
    }

}