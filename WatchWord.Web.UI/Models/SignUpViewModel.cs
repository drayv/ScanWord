using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WatchWord.Web.UI.Models
{
    public class SignUpViewModel
    {
        [Required(), MinLength(4)]
        public string Login { get; set; }

        [Required(), EmailAddress(), Display(Name = "Email address")]
        public string Email { get; set; }

        [Required()]
        public string Password { get; set; }

        [Compare("Password"), Required()]
        public string ConfirmPassword { get; set; }
    }
}