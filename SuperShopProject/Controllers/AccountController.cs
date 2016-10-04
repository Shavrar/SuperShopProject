using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using SuperShopProject.Constants;
using SuperShopProject.Managers;
using SuperShopProject.ViewModels;

namespace SuperShopProject.Controllers
{
    [RoutePrefix("account")]
    [Authorize]
    public class AccountController : BaseController
    {
        private readonly ApplicationSignInManager _signInManager;
        private readonly UsersManager _userManager;

        public AccountController(UsersManager userManager, ApplicationSignInManager signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                    case SignInStatus.LockedOut:
                        //return View("Lockout");
                    case SignInStatus.RequiresVerification:
                    case SignInStatus.Failure:
                       ModelState.AddModelError("", "InvalidLoginAttemp");                        
                       return View(model);
                    default:
                    ModelState.AddModelError("", "InvalidLoginAttemp");
                    return View(model);
            }
        }

        [HttpGet]
        [Route("name")]
        public ActionResult GetName()
        {
            return JsonNet(new {name = User.Identity.Name});
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        #region Helpers

        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }


        #endregion
    }
}