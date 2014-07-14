using System.Web.Mvc;

namespace Nbugs.OperationApp.UI.Admin.Areas.yxt
{
    public class yxtAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "yxt";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "yxt_default",
                "yxt/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
