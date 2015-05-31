using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WatchWord.Web.UI.Models;

namespace WatchWord.Web.UI.Identity
{
    public class AppIdentityDbContext : IdentityDbContext<AppUser>
    {
        public AppIdentityDbContext()
            : base("ScanWordDb")
        {

        }
        /// <summary>
        /// Creates an instance of the IidentityDbContext
        /// </summary>
        /// <returns></returns>
        public static AppIdentityDbContext Create()
        {
            return new AppIdentityDbContext();
        }
    }
}