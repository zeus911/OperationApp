using Nbugs.IBLL.System;
using Nbugs.OperationApp.Models.System;
using Nbugs.OperationApp.UI.Admin.Core;
using Ninject;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Nbugs.OperationApp.UI.Admin.Controllers
{
    [LoginAuthorize]
    public class ModuleController : Controller
    {
        //
        // GET: /Module/
        [Inject]
        private IModuleService moduleService { get; set; }

        [Inject]
        private IModuleActionsService maSrvc { get; set; }

        [Inject]
        private IUserService userService { get; set; }

        [SupportFilter]
        public ActionResult Index()
        {
            var account = Session["Account"] as Account;
            ViewBag.Permissions = account.Permissions;
            return View();
        }

        [SupportFilter]
        public ActionResult Create(int id = 0)
        {
            var createModule = new Module()
            {
                ParentId = id,
                Enable = true,
                Sort = 0
            };
            return View(createModule);
        }

        [HttpPost]
        [SupportFilter]
        public JsonResult Create(Module module)
        {
            try
            {
                var account = Session["Account"] as Account;
                module.CreateTime = DateTime.Now;
                module.CreateUser = account.UserName;
                var createModule = moduleService.AddEntity(module);
                if (createModule != null)
                {
                    moduleService.InsertRoleModule();
                    return Json(new { type = 1, message = "新建成功！" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { type = 0, message = "新建失败！" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                return Json(new { type = 0, message = e.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [SupportFilter(ActionName = "Index")]
        public JsonResult GetOptListByModule(int id = 0)
        {
            return Json(moduleService.LoadEntities(m => m.Id == id).Select(u => u.ModuleActions).FirstOrDefault());
        }

        [SupportFilter(ActionName = "Index")]
        public JsonResult GetList(int id = 0)
        {
            var modules = moduleService.GetList(id);
            var jsonData = from r in modules.ToList().OrderBy(m => m.Sort)
                           select new
                           {
                               Id = r.Id,
                               Name = r.Name,
                               ParentId = r.ParentId,
                               Url = r.Url,
                               Icon = r.Icon,
                               Sort = r.Sort,
                               Remark = r.Remark,
                               Enable = r.Enable,
                               CreateUser = r.CreateUser,
                               CreateTime = r.CreateTime,
                               IsLast = r.IsLast,
                               state = !r.IsLast ? "closed" : "open"
                           };

            return new ToJsonResult() { Data = jsonData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [SupportFilter]
        public ActionResult Edit(int id)
        {
            return View(moduleService.LoadEntities(m => m.Id == id).FirstOrDefault());
        }

        [HttpPost]
        [SupportFilter]
        public JsonResult Edit(Module module)
        {
            if (ModelState.IsValid)
            {
                return moduleService.UpdateEntity(module)
                    ? Json(1, JsonRequestBehavior.AllowGet)
                    : Json(0, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        [SupportFilter]
        public JsonResult Delete(int id)
        {
            if (moduleService.LoadEntities(m => m.ParentId == id).Count() > 0)
                return Json(new { type = 0, message = "该节点下面还有子节点，无法删除!" }, JsonRequestBehavior.AllowGet);
            var deleteModule = moduleService.LoadEntities(m => m.Id == id).FirstOrDefault();
            return moduleService.DeleteEntity(deleteModule) ?
                Json(new { type = 0, message = "删除失败！" }, JsonRequestBehavior.AllowGet)
                : Json(new { type = 1, message = "删除成功！" }, JsonRequestBehavior.AllowGet);
        }

        [SupportFilter(ActionName = "Create")]
        public ActionResult CreateOpt(string moduleId)
        {
            ViewData["moduleId"] = moduleId;
            return View();
        }

        [SupportFilter(ActionName = "Edit")]
        public ActionResult EditOpt(int id)
        {
            return View(maSrvc.LoadEntities(u => u.Id == id).FirstOrDefault());
        }

        [HttpPost]
        [SupportFilter(ActionName = "Edit")]
        public JsonResult EditOpt(ModuleAction moduleAction)
        {
            if (ModelState.IsValid)
            {
                return maSrvc.UpdateEntity(moduleAction)
                    ? Json(0)
                    : Json(1);
            }
            return Json(0);
        }

        [HttpPost]
        [SupportFilter(ActionName = "Create")]
        public JsonResult CreateOpt(ModuleAction moduleAction)
        {
            return (maSrvc.AddEntity(moduleAction) != null)
                ? Json(new
                {
                    type = 1,
                    message = "新建成功！"
                }, JsonRequestBehavior.AllowGet)
                : Json(new
                {
                    type = 0,
                    message = "新建失败，请重新输入！"
                }, JsonRequestBehavior.AllowGet);
        }

        [SupportFilter(ActionName = "Delete")]
        public JsonResult DeleteOpt(int id)
        {
            var deleteOpt = maSrvc.LoadEntities(u => u.Id == id).FirstOrDefault();
            if (deleteOpt == null)
                return Json(0, JsonRequestBehavior.AllowGet);
            return maSrvc.DeleteEntity(deleteOpt)
                ? Json(0, JsonRequestBehavior.AllowGet)
                : Json(1, JsonRequestBehavior.AllowGet);
        }
    }
}