using Nbugs.OperationApp.Models.System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Nbugs.OperationApp.UI.Admin.Core
{
    public static class ExtendMvcHtml
    {
        public static MvcHtmlString ToolButton(this HtmlHelper helper, string id, string icon, string text,
            List<Permission> permissions, string keycode, string controller, bool hr)
        {
            if (permissions.Where(p => p.KeyCode == keycode && p.Controller.ToLower() == controller.ToLower()).Count() > 0)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.AppendFormat("<a id=\"{0}\" style=\"float: left;\" class=\"l-btn l-btn-plain\">", id);
                sb.AppendFormat("<span class=\"l-btn-left\"><span class=\"l-btn-text {0}\" style=\"padding-left: 20px;\">", icon);
                sb.AppendFormat("{0}</span></span></a>", text);
                if (hr)
                {
                    sb.Append("<div class=\"datagrid-btn-separator\"></div>");
                }
                return new MvcHtmlString(sb.ToString());
            }
            else
            {
                return new MvcHtmlString("");
            }
        }

        public static MvcHtmlString ToolButton(this HtmlHelper helper, string id, string icon, string text, bool hr)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendFormat("<a id=\"{0}\" style=\"float: left;\" class=\"l-btn l-btn-plain\">", id);
            sb.AppendFormat("<span class=\"l-btn-left\"><span class=\"l-btn-text {0}\" style=\"padding-left: 20px;\">", icon);
            sb.AppendFormat("{0}</span></span></a>", text);
            if (hr)
            {
                sb.Append("<div class=\"datagrid-btn-separator\"></div>");
            }
            return new MvcHtmlString(sb.ToString());
        }
    }
}