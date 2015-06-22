using System.ComponentModel.DataAnnotations;

namespace WatchWord.Web.UI.Models.Account
{
    /// <summary>
    /// The sign up view model.
    /// </summary>
    public class SignUpViewModel
    {
        /// <summary>
        /// Gets or sets the login.
        /// </summary>
        [Required] 
        [MinLength(4)]
        [System.Web.Mvc.Remote("RemoteLoginValidation", "Account", ErrorMessage = "A user with such name is already registered")]
        public string Login { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        [Required]
        [EmailAddress] 
        [Display(Name = "Email address")]
        [System.Web.Mvc.Remote("RemoteEmailValidation", "Account",  ErrorMessage = "A user with this email is already registered")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the confirm password.
        /// </summary>
        [Compare("Password")]
        [Required]
        public string ConfirmPassword { get; set; }
    }
}