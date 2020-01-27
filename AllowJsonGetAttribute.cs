using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Intranet2.Attributes
{
    public class AllowJsonGetAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            var jsonResult = filterContext.Result as JsonResult;
            if (jsonResult == null)
                return;
    
            jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            base.OnResultExecuting(filterContext);
        }
    }
}