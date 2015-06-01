namespace WatchWord.Web.UI.Models.Account
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The sign up view model.
    /// </summary>
    public class SignUpViewModel
    {
        /// <summary>
        /// Gets or sets the login.
        /// </summary>
        [Required, MinLength(4)]
        public string Login { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        [Required, EmailAddress, Display(Name = "Email address")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the confirm password.
        /// </summary>
        [Compare("Password"), Required]
        public string ConfirmPassword { get; set; }
    }
}