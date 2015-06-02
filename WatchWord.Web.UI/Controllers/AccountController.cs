using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using WatchWord.Web.UI.Identity;
using WatchWord.Web.UI.Models.Account;
using WatchWord.Web.UI.Models.Identity;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;

namespace WatchWord.Web.UI.Controllers
{
    /// <summary>
    /// The account controller.
    /// </summary>
    public class AccountController : Controller
    {
        /// <summary>
        /// The sign up.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult SignUp()
        {
            return View();
        }

        /// <summary>
        /// The sign up post method.
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> SignUp(SignUpViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            var result = await this.UserManager.CreateAsync(new AppUser { UserName = model.Login, Email = model.Email }, model.Password);
            if (result.Succeeded)
            {
                return this.RedirectToAction("Login");
            }

            AddModelErrors(result);

            return View(model);
        }

        private void AddModelErrors(Microsoft.AspNet.Identity.IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                this.ModelState.AddModelError(string.Empty, error);
            }
        }

        public ActionResult Login(string returnUrl = "")
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        private string GetLocalUrl(string url)
        {
            if (string.IsNullOrEmpty(url) || !Url.IsLocalUrl(url))
            {
                //Change this later
                url = Url.Action("Add", "Materials");
            }
            return url;
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindAsync(model.Login, model.Password);
                if (user != null)
                {
                    var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = model.IsPersistant }, identity);
                    return Redirect(GetLocalUrl(model.ReturnUrl));
                }
                ModelState.AddModelError("", "Invalid login or password");
            }
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            //Change this
            return RedirectToAction("Add", "Materials");
        }

        /// <summary>
        /// Gets the user manager.
        /// </summary>
        private AppUserManager UserManager
        {
            get { return HttpContext.GetOwinContext().GetUserManager<AppUserManager>(); }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
    }
}