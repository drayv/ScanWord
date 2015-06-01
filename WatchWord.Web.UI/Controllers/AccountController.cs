namespace WatchWord.Web.UI.Controllers
{
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity.Owin;
    using WatchWord.Web.UI.Identity;
    using WatchWord.Web.UI.Models.Account;
    using WatchWord.Web.UI.Models.Identity;

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
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var result = await this.UserManager.CreateAsync(new AppUser { UserName = model.Login, Email = model.Email }, model.Password);
            if (result.Succeeded)
            {
                return this.RedirectToAction("LogIn");
            }

            foreach (var error in result.Errors)
            {
                this.ModelState.AddModelError(string.Empty, error);
            }

            return View(model);
        }
    }
}