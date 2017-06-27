using System.Reflection;
using System.Web.Mvc;
using log4net;

namespace MillionaireGame.Infrastructure.Filters
{
    public class ExceptionLoggerAttribute : FilterAttribute, IExceptionFilter
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)
            {
                return;
            }
            
            LogicalThreadContext.Properties["ExceptionMessage"] = filterContext.Exception.Message;
            LogicalThreadContext.Properties["ControllerName"] = filterContext.RouteData.Values["controller"].ToString();
            LogicalThreadContext.Properties["ActionName"] = filterContext.RouteData.Values["action"].ToString();
            LogicalThreadContext.Properties["StackTrace"] = filterContext.Exception.StackTrace;
            Log.Error("Error");
            filterContext.ExceptionHandled = true;
            filterContext.Result = new ViewResult
            {
                ViewName = "ErrorView"
            };
        }
    }

}