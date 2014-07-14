using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nbugs.Common;
using Nbugs.OperationApp.UI.Admin.Core;
using Nbugs.OperationApp.Models.System;
using Nbugs.IBLL.System;
using Ninject;

namespace Nbugs.OperationApp.UI.Admin.Controllers
{
    [LoginAuthorize]
    public class AccountController : Controller
    {
        //
        // GET: /Account/
        [Inject]
        private IUserService userService { get; set; }
        [Inject]
        private IModuleService moduleService { get; set; }
        [Inject]
        private IRoleService roleService { get; set; }
        private Account account= new Account();
        public ActionResult Index()
        {
            account = Session["Account"] as Account;
            ViewBag.Permissions = account.Permissions;
            return View();
        }
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult CheckCode()
        {
            ValidataCode validataCode = new ValidataCode();
            string code = validataCode.CreateValidateCode(4);
            TempData["ValidataCode"] = code;
            byte[] bytes = validataCode.CreateValidateGraphic(code);
            return File(bytes, @"image/jpeg");
        }
        [HttpPost]
        [AllowAnonymous]
        public JsonResult CheckUser(User user, string code)
        {
            string sessionCode = string.IsNullOrEmpty(this.TempData["ValidataCode"].ToString())
                ? new Guid().ToString()
                : this.TempData["ValidataCode"].ToString();
            this.TempData["ValidataCode"] = new Guid();
            if (sessionCode != code)
            {
                return Json("输入验证码有误！");
            }
            string msg = "undefined";
            switch (userService.CheckUser(user))
            {
                case 1:
                    msg = "用户不存在";
                    break;

                case 2:
                    msg = "密码错误";
                    break;

                case 3:
                    msg = "ok";

                    var loginUser = userService.LoadEntities(u => u.LoginName == user.LoginName).FirstOrDefault();
                    account.UserId = loginUser.Id;
                    account.UserName = loginUser.Name;
                    account.Permissions = userService.GetPermissions(loginUser.Id);
                    Session["Account"] = account;
                    break;

                default:
                    msg = "未知错误";
                    break;
            }
            return Json(msg);
        }
        
        [SupportFilter(ActionName="index")]
        public JsonResult GetTree(int? id)
        {
            if(id==null)
            {
                return null;
            }

            account = (Account)Session["Account"];
            var menus = moduleService.GetMenuByUserIdAndModuleId(account.UserId,id);
            var jsonData = menus.OrderBy(m => m.Sort).Select(n => new
            {
                id = n.Id.ToString(),
                text = n.Name,
                attributes = n.Url,
                iconCls = n.Icon,
                state = !n.IsLast ? "closed" : "open"
            });
          
            return Json(jsonData,JsonRequestBehavior.AllowGet);
        }

        [SupportFilter(ActionName = "Index")]
        public JsonResult GetList(Query query)
        {
            int total=0;
            bool isAsc = false;
            var users = userService.GetUsers(query.page, query.rows, out total, isAsc = query.order == "desc" ? false : true);
            var jsonData = new
            {
                total = total,
                rows = users
            };
            return new ToJsonResult() { Data = jsonData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [SupportFilter]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [SupportFilter]
        public JsonResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                user.CreateTime = DateTime.Now;
                account = Session["Account"] as Account;
                user.CreateUser = account.UserName;
                return userService.AddEntity(user) == null
                    ? Json(0, JsonRequestBehavior.AllowGet)
                    : Json(1, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [SupportFilter]
        public ActionResult Edit(int id)
        {
            return View(userService.LoadEntities(u => u.Id == id).FirstOrDefault());
        }

        [HttpPost]
        [SupportFilter]
        public JsonResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                return userService.UpdateEntity(user)
                    ? Json(0, JsonRequestBehavior.AllowGet)
                    : Json(1, JsonRequestBehavior.AllowGet);
            }
            return Json(0, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [SupportFilter]
        public JsonResult Delete(int id)
        {
            var deleteUser = userService.LoadEntities(u => u.Id == id).FirstOrDefault();

            return userService.DeleteEntity(deleteUser)
                ? Json(new { type = 0, message = "删除失败" }, JsonRequestBehavior.AllowGet)
                : Json(new { type = 1, message = "删除成功" }, JsonRequestBehavior.AllowGet);
        }

        [SupportFilter]
        public ActionResult Details(int id)
        {
            return View(userService.LoadEntities(u => u.Id == id).FirstOrDefault());
        }

        [SupportFilter]
        public ActionResult Allot(int id)
        {
            ViewBag.UserId = id;
            account = Session["Account"] as Account;
            ViewBag.Permissions = userService.GetPermissions(account.UserId);
            return View();
        }

        [HttpPost]
        [SupportFilter(ActionName = "Allot")]
        public JsonResult UpdateUserRoleByUserId(int UserId, int RoleId, bool Flag)
        {
            if (Flag)
            {
                try
                {
                    userService.InsertUserRole(UserId, RoleId);
                    return Json(1, JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                try
                {
                    userService.DeleteUserRole(UserId, RoleId);
                    return Json(1, JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
                }
            }
        }

        [SupportFilter(ActionName = "Allot")]
        public JsonResult GetRoles(int userId)
        {
            var userRoleIds = userService.GetRolesByUserId(userId);
            var roles = roleService.LoadEntities(r => true);
            var jsonData = roles.Select(r => new
            {
                Id = r.Id,
                Name = r.Name,
                Flag = userRoleIds.Contains(r.Id)
            });
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
    }
}
