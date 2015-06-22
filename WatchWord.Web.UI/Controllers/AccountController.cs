using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using WatchWord.Web.UI.Identity;
using WatchWord.Web.UI.Models.Account;
using WatchWord.Web.UI.Models.Identity;
using System.Linq;

namespace WatchWord.Web.UI.Controllers
{
    /// <summary>
    /// The account controller.
    /// </summary>
    public class AccountController : Controller
    {
        /// <summary>
        /// Gets the user manager.
        /// </summary>
        private AppUserManager UserManager
        {
            get { return HttpContext.GetOwinContext().GetUserManager<AppUserManager>(); }
        }

        /// <summary>
        /// Gets the authentication manager.
        /// </summary>
        private IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

        /// <summary>
        /// Sign up action.
        /// </summary>
        /// <returns>
        /// The SignUp view action result <see cref="ActionResult"/>.
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
        /// The sing up view with errors or redirect to login page <see cref="Task"/>.
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

        /// <summary>
        /// Add model errors to model state.
        /// </summary>
        /// <param name="result">
        /// Identity result.
        /// </param>
        private void AddModelErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                this.ModelState.AddModelError(string.Empty, error);
            }
        }

        /// <summary>
        /// Login action.
        /// </summary>
        /// <param name="returnUrl">
        /// The return url in login view model.
        /// </param>
        /// <returns>
        /// Action result <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult LogIn(string returnUrl = "")
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        /// <summary>
        /// Login post method.
        /// </summary>
        /// <param name="model">
        /// Login view model
        /// </param>
        /// <returns>
        /// Action result task <see cref="Task"/>.
        /// </returns>
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = await this.UserManager.FindAsync(model.Login, model.Password);
            if (user != null)
            {
                var identity = await this.UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                this.AuthenticationManager.SignOut();
                this.AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = model.IsPersistent }, identity);

                return this.Redirect(this.GetLocalUrl(model.ReturnUrl));
            }

            this.ModelState.AddModelError(string.Empty, "Invalid login or password");

            return View(model);
        }

        /// <summary>
        /// Check url and return it or default local url.
        /// </summary>
        /// <param name="url">
        /// The url to check and return.
        /// </param>
        /// <returns>
        /// Checked url <see cref="string"/>.
        /// </returns>
        private string GetLocalUrl(string url)
        {
            if (string.IsNullOrEmpty(url) || !Url.IsLocalUrl(url))
            {
                ////TODO Change this later
                url = Url.Action("Add", "Materials");
            }

            return url;
        }

        /// <summary>
        /// The logout action.
        /// </summary>
        /// <returns>
        /// Redirect to main page <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            ////TODO Change this
            return RedirectToAction("Add", "Materials");
        }

        /// <summary>
        /// Cheks if login is already registered.
        /// </summary>
        /// <param name="login">A username from a form</param>
        /// <returns>Json with result data</returns>
        public JsonResult RemoteLoginValidation(string login)
        {
            return Json(UserManager.Users.All(n => string.Compare(login, n.UserName, System.StringComparison.InvariantCultureIgnoreCase) != 0), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Cheks if this email is already registered.
        /// </summary>
        /// <param name="email">A email from a form</param>
        /// <returns>Json with result data</returns>
        public JsonResult RemoteEmailValidation(string email)
        {
            return Json(UserManager.Users.All(n => string.Compare(email, n.Email, System.StringComparison.InvariantCultureIgnoreCase) != 0), JsonRequestBehavior.AllowGet);
        }
    }
}