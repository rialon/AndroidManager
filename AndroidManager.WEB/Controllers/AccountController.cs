using AndroidManager.BLL.Dto;
using AndroidManager.BLL.Interfaces;
using AndroidManager.WEB.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AndroidManager.WEB.Controllers {

    public class AccountController : Controller {
        private IOperatorService _OperatorService {
            get {
                return HttpContext.GetOwinContext().GetUserManager<IOperatorService>();
            }
        }

        private IAuthenticationManager _Authentication {
            get {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public ActionResult Login() {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel loginModel) {
            await this._SetInitialData();
            if (ModelState.IsValid) {
                var _operatorDto = new OperatorDto { Email = loginModel.Email, Password = loginModel.Password };
                var _claim = await this._OperatorService.Authenticate(_operatorDto);
                if (_claim != null) {
                    this._Authentication.SignOut();
                    this._Authentication.SignIn(new AuthenticationProperties { IsPersistent = true }, _claim);
                    return RedirectToAction("Index", "Job");
                } else {
                    ModelState.AddModelError("", "Invalid login or password");
                }
            }
            return View(loginModel);
        }

        public ActionResult Logout() {
            this._Authentication.SignOut();
            return RedirectToAction("Index", "Job");
        }

        public ActionResult Register() {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> Register(RegisterViewModel registerModel) {
            await this._SetInitialData();
            if (ModelState.IsValid) {
                var _userDto = new OperatorDto {
                    Email = registerModel.Email,
                    Password = registerModel.Password,
                    Role = "User"
                };
                var _result = await this._OperatorService.Create(_userDto);
                if (_result.Succeeded) {
                    return View("SuccessRegister");
                } else {
                    ModelState.AddModelError(_result.Property, _result.Message);
                }
            } else {
                var _duplicatedModelState = ModelState.Values.SingleOrDefault(ms => ms.Errors.Count > 1 && ms.Errors[0].ErrorMessage == ms.Errors[1].ErrorMessage);
                if (_duplicatedModelState != null) {
                    _duplicatedModelState.Errors.Remove(_duplicatedModelState.Errors[1]);
                }
            }
            return View(registerModel);
        }

        private async Task _SetInitialData() {
            await this._OperatorService.SetInitialData(new List<string> { "User", "Admin" });
        }
    }
}