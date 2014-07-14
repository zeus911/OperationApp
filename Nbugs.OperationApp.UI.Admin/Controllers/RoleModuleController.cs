using Nbugs.IBLL.System;
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
    [LoginAuthorize]
    public class RoleModuleController : Controller
    {
        //
        // GET: /RoleModule/
        [Inject]
        private IRoleService roleService { get; set; }

        [Inject]
        private IRoleModuleService rmService { get; set; }

        [Inject]
        private IModuleActionsService maService { get; set; }

        [Inject]
        private IModuleService moduleService { get; set; }

        [Inject]
        private IRoleModuleActionService rmaService { get; set; }

        [SupportFilter]
        public ActionResult Index()
        {
            return View();
        }

        [SupportFilter(ActionName = "Index")]
        public JsonResult GetModuleList(int id = 0)
        {
            var menus = moduleService.LoadEntities(m => m.ParentId == id && m.Id > 0);
            var jsonData = (
                        from m in menus
                        orderby m.Sort
                        select new
                        {
                            Id = m.Id.ToString(),
                            Name = m.Name,
                            IsLast = m.IsLast,
                            state = !m.IsLast ? "closed" : "open"
                        }
                    );
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [SupportFilter(ActionName = "Index")]
        public JsonResult GetRoleList()
        {
            return Json(roleService.LoadEntities(r => true)
                    .Select(r => new { Id = r.Id, Name = r.Name }), JsonRequestBehavior.AllowGet);
        }

        [SupportFilter(ActionName = "Index")]
        public JsonResult GetRoleModuleActions(int roleId = 0, int moduleId = 0)
        {
            if (roleId == 0 && moduleId == 0)
            {
                return Json(new object[0], JsonRequestBehavior.AllowGet);
            }
            var roleModuleActions = rmService.LoadEntities(r => r.RoleId == roleId && r.ModuleId == moduleId)
                                             .Select(m => m.RoleModuleActions).FirstOrDefault()
                                             .Where(x => x.IsValid == true).Select(r => r.KeyCode);
            var jsonData = maService.LoadEntities(m => m.ModuleId == moduleId).Select(x =>
                           new
                           {
                               ModuleId = moduleId,
                               RoleId = roleId,
                               Name = x.Name,
                               KeyCode = x.KeyCode,
                               IsValid = roleModuleActions.Contains(x.KeyCode) ? true : false
                           });
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [SupportFilter(ActionName = "Save")]
        public void UpdateRight(UpdateModule updateModule)
        {
            var updateRoleModule = rmService.LoadEntities(m => m.ModuleId == updateModule.ModuleId && m.RoleId == updateModule.RoleId).FirstOrDefault();
            if (updateRoleModule.RoleModuleActions != null)
            {
                var updateRoleModelAction = updateRoleModule.RoleModuleActions.Where(r => r.KeyCode == updateModule.KeyCode).FirstOrDefault();
                if (updateRoleModelAction != null)
                {
                    updateRoleModelAction.IsValid = updateModule.IsValid;
                    rmaService.UpdateEntity(updateRoleModelAction);
                    rmService.UpdateRoleModule(updateModule.ModuleId, updateModule.RoleId);
                }
                else
                {
                    updateRoleModelAction = new RoleModuleAction
                    {
                        IsValid = updateModule.IsValid,
                        KeyCode = updateModule.KeyCode,
                        RoleModuleId = updateRoleModule.Id
                    };
                    rmaService.AddEntity(updateRoleModelAction);
                    rmService.UpdateRoleModule(updateModule.ModuleId, updateModule.RoleId);
                }
            }
            else
            {
                var updateRoleModelAction = new RoleModuleAction
                {
                    IsValid = updateModule.IsValid,
                    KeyCode = updateModule.KeyCode,
                    RoleModuleId = updateRoleModule.Id
                };
                rmaService.AddEntity(updateRoleModelAction);
                rmService.UpdateRoleModule(updateModule.ModuleId, updateModule.RoleId);
            }
        }
    }
}
