using Nbugs.BLL.System;
using Nbugs.OperationApp.Models.System;
using Nbugs.OperationApp.UI.Admin.Core;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nbugs.OperationApp.UI.Admin.Controllers
{
    public class RoleController : Controller
    {
        //
        // GET: /Role/

        [Inject]
        private RoleService roleService { get; set; }

        [SupportFilter]
        public ActionResult Index()
        {
            return View();
        }

        [SupportFilter]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [SupportFilter]
        public JsonResult Create(Role role)
        {
            if (ModelState.IsValid)
            {
                if (roleService.AddEntity(role) != null)
                {
                    roleService.InsertRoleModule();
                    return Json(1, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    Json(0, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(0, JsonRequestBehavior.AllowGet);
        }

        [SupportFilter(ActionName = "Index")]
        public JsonResult GetList(int pageIndex = 1, int pageSize = 15, bool isAsc = true)
        {
            int total;
            var roles = roleService.LoadPageEntities(pageIndex, pageSize, out total, u => true, isAsc, u => u.Id);
            var jsonData = new
            {
                total = total,
                rows = roles
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [SupportFilter]
        public ActionResult Edit(int id)
        {
            return View(roleService.LoadEntities(r => r.Id == id).FirstOrDefault());
        }

        [HttpPost]
        [SupportFilter]
        public JsonResult Edit(Role role)
        {
            if (ModelState.IsValid)
            {
                return roleService.UpdateEntity(role)
                    ? Json(0, JsonRequestBehavior.AllowGet)
                    : Json(1, JsonRequestBehavior.AllowGet);
            }
            return Json(2, JsonRequestBehavior.AllowGet);
        }

    }
}
