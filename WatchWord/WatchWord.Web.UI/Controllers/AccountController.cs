using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using WatchWord.Web.UI.Identity;
using WatchWord.Web.UI.Models.Account;
using WatchWord.Web.UI.Models.Identity;

namespace WatchWord.Web.UI.Controllers
{
    /// <summary>Account controller.</summary>
    public class AccountController : Controller
    {
        /// <summary>Gets the user's manager.</summary>
        private AppUserManager UserManager
        {
            get { return HttpContext.GetOwinContext().GetUserManager<AppUserManager>(); }
        }

        /// <summary>Gets the authentication manager.</summary>
        private IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

        /// <summary>Sign up action.</summary>
        /// <returns>The sign up view action result <see cref="ActionResult"/>.</returns>
        public ActionResult SignUp()
        {
            return View();
        }

        /// <summary>The sign up post method.</summary>
        /// <param name="model">The sign up view model.</param>
        /// <returns>Redirects to the login page, or shows validation errors <see cref="Task"/>.</returns>
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> SignUp(SignUpViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await UserManager.CreateAsync(new AppUser { UserName = model.Login, Email = model.Email }, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("Login");
            }

            AddModelErrors(result);

            return View(model);
        }

        /// <summary>Adds model errors to model state.</summary>
        /// <param name="result">Identity result.</param>
        private void AddModelErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error);
            }
        }

        /// <summary>Log in action.</summary>
        /// <param name="returnUrl">Page where a user came from.</param>
        /// <returns>Log in page <see cref="ActionResult"/>.</returns>
        public ActionResult LogIn(string returnUrl = "")
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        /// <summary>Log in post method.</summary>
        /// <param name="model">Log in view model.</param>
        /// <returns>Returns to the page where the user's came from, or shows validation errors <see cref="Task"/>.</returns>
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> LogIn(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await UserManager.FindAsync(model.Login, model.Password);
            if (user != null)
            {
                var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                AuthenticationManager.SignOut();
                AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = model.IsPersistent }, identity);

                return Redirect(GetLocalUrl(model.ReturnUrl));
            }

            ModelState.AddModelError(string.Empty, "Invalid login or password");

            return View(model);
        }

        /// <summary>Returns specified url, or url for main page if specified url is not local.</summary>
        /// <param name="url">Specified url.</param>
        /// <returns>Checked url or main page url <see cref="string"/>.</returns>
        private string GetLocalUrl(string url)
        {
            if (string.IsNullOrEmpty(url) || !Url.IsLocalUrl(url))
            {
                ////TODO Change this to "main page" later
                url = Url.Action("ParseMaterial", "Materials");
            }

            return url;
        }

        /// <summary>The log out action.</summary>
        /// <returns>Main page redirect <see cref="ActionResult"/>.</returns>
        public ActionResult LogOut()
        {
            AuthenticationManager.SignOut();
            ////TODO Change this to "main page" later
            return RedirectToAction("ParseMaterial", "Materials");
        }

        /// <summary>Checks if login is already registered.</summary>
        /// <param name="login">A username from a form.</param>
        /// <returns>JSON with result data.</returns>
        public JsonResult RemoteLoginValidation(string login)
        {
            return Json(UserManager.Users.All(n => string.Compare(login, n.UserName, System.StringComparison.InvariantCultureIgnoreCase) != 0), JsonRequestBehavior.AllowGet);
        }

        /// <summary>Checks if this email is already registered.</summary>
        /// <param name="email">An email from a form.</param>
        /// <returns>JSON with result data.</returns>
        public JsonResult RemoteEmailValidation(string email)
        {
            return Json(UserManager.Users.All(n => string.Compare(email, n.Email, System.StringComparison.InvariantCultureIgnoreCase) != 0), JsonRequestBehavior.AllowGet);
        }
    }
}