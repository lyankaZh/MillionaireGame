using System;
using System.Web.Mvc;
using MillionaireGame.DAL;
using MillionaireGame.DAL.Entities;

namespace MillionaireGame.Infrastructure.Filters
{
    //public class ExceptionLoggerAttribute : FilterAttribute, IExceptionFilter
    //{
    //    public void OnException(ExceptionContext filterContext)
    //    {
    //        ExceptionDetail exceptionDetail = new ExceptionDetail()
    //        {
    //            ExceptionMessage = filterContext.Exception.Message,
    //            StackTrace = filterContext.Exception.StackTrace,
    //            ControllerName = filterContext.RouteData.Values["controller"].ToString(),
    //            ActionName = filterContext.RouteData.Values["action"].ToString(),
    //            Date = DateTime.Now
    //        };

    //        using (MillionaireContext db = new MillionaireContext())
    //        {
    //            db.ExceptionDetails.Add(exceptionDetail);
    //            db.SaveChanges();
    //        }

    //        filterContext.ExceptionHandled = true;
    //    }
    //}

}