using Nbugs.OperationApp.UI.Admin.Core;
using System.Web.Mvc;

namespace Nbugs.OperationApp.UI.Admin.Controllers
{
    [LoginAuthorize]
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }
    }
}