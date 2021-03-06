﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using WatchWord.Web.UI.Models.Identity;

namespace WatchWord.Web.UI.Identity
{
    /// <summary>The scan word user's validator.</summary>
    public class UserValidator : UserValidator<AppUser, int>
    {
        /// <summary>Initializes a new instance of the <see cref="UserValidator"/> class.</summary>
        /// <param name="manager">User's manager.</param>
        public UserValidator(UserManager<AppUser, int> manager)
            : base(manager)
        {
        }

        /// <summary>Gets or sets the minimal length of the user name.</summary>
        public int UserNameMinLength { get; set; }

        /// <summary>Gets or sets a value indicating whether user name starts with digit.</summary>
        public bool UserNameStartsWithDigit { get; set; }

        /// <summary>Validate user asynchronously.</summary>
        /// <param name="item">Application user.</param>
        /// <returns>Identity result <see cref="Task"/>.</returns>
        public override async Task<IdentityResult> ValidateAsync(AppUser item)
        {
            if (UserNameMinLength > 0 && item.UserName.Length < UserNameMinLength)
            {
                return IdentityResult.Failed(string.Format("Login length must be longer then {0}.", UserNameMinLength));
            }

            var result = await base.ValidateAsync(item);
            var errors = result.Errors.ToList();
            if (UserNameStartsWithDigit == false && item.UserName.Length > 0 && char.IsDigit(item.UserName[0]))
            {
                errors.Add("Login can't starts with digit.");
            }

            return errors.Count > 0 ? IdentityResult.Failed(errors.ToArray()) : IdentityResult.Success;
        }
    }
}