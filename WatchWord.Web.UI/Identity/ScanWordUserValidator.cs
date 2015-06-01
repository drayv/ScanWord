using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WatchWord.Web.UI.Models;

namespace WatchWord.Web.UI.Identity
{
    public class ScanWordUserValidator : UserValidator<AppUser>
    {
        public int UserNameMinLength { get; set; }

        public bool UserNameStartsWithDiggit { get; set; }

        public ScanWordUserValidator(UserManager<AppUser, string> manager)
            : base(manager)
        {

        }

        /// <summary>
        /// Validate incoming user async
        /// </summary>
        /// <param name="item">An instance of IUser</param>
        /// <returns></returns>
        public override async Task<IdentityResult> ValidateAsync(AppUser item)
        {
            if (UserNameMinLength > 0 && item.UserName.Length < UserNameMinLength)
                return IdentityResult.Failed(new[] { string.Format("Login length must be longer then {0}.", UserNameMinLength) });
            var result = await base.ValidateAsync(item);
            var errors = result.Errors.ToList();
            if (UserNameStartsWithDiggit == false && item.UserName.Length > 0 && char.IsDigit(item.UserName[0]))
                    errors.Add("Login can't starts with digit.");
            return errors.Count > 0 ? IdentityResult.Failed(errors.ToArray()) : IdentityResult.Success;
        }
    }
}