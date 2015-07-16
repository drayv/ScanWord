using System.ComponentModel.DataAnnotations;

namespace WatchWord.Web.UI.Models.Account
{
    /// <summary>The login view model.</summary>
    public class LoginViewModel
    {
        /// <summary>Gets or sets the login.</summary>
        [Required]
        public string Login { get; set; }

        /// <summary>Gets or sets the password.</summary>
        [Required]
        public string Password { get; set; }

        /// <summary>Gets or sets a value indicating whether is persistent.</summary>
        [Display(Name = "Remember me?")]
        public bool IsPersistent { get; set; }

        /// <summary>Gets or sets the return url.</summary>
        public string ReturnUrl { get; set; }
    }
}