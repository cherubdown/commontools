using Intranet2.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Intranet2.Common.Extensions
{
    public static class HtmlExtensions
    {
        public static HtmlString BuildBreadcrumbNavigation(this HtmlHelper helper)
        {
            var routeData = helper.ViewContext.RouteData;
            // optional condition: I didn't wanted it to show on home and account controller
            if (routeData.Values["controller"].ToString() == "Home" ||
                routeData.Values["controller"].ToString() == "Account")
            {
                return new HtmlString(string.Empty);
            }

            StringBuilder breadcrumb = new StringBuilder("<ol class='breadcrumb'><li>")
                .Append(helper.ActionLink("Home", "Index", "Home").ToHtmlString()).Append("</li>");


            breadcrumb.Append("<li>");
            breadcrumb.Append(helper.ActionLink(routeData.Values["controller"].ToString().Titleize(),
                                               "Index",
                                               routeData.Values["controller"].ToString()));
            breadcrumb.Append("</li>");

            if (helper.ViewContext.RouteData.Values["action"].ToString() != "Index")
            {
                breadcrumb.Append("<li>");
                breadcrumb.Append(helper.ActionLink(routeData.Values["action"].ToString().Titleize(),
                                                    routeData.Values["action"].ToString(),
                                                    routeData.Values["controller"].ToString()));
                breadcrumb.Append("</li>");
            }

            return new HtmlString(breadcrumb.Append("</ol>").ToString());
        }

    }
}
