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
    //[LoginAuthorize]
    public class AccountController : Controller
    {
        //
        // GET: /Account/
        [Inject]
        private IUserService userService { get; set; }
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
    }
}
