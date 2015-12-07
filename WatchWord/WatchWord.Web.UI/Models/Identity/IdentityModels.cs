using Microsoft.AspNet.Identity.EntityFramework;

namespace WatchWord.Web.UI.Models.Identity
{
    public class AppUser : IdentityUser<int, AppUserLogin, AppUserRole, AppUserClaim>
    {
        public override int Id { get; set; }
    }

    public class AppRole : IdentityRole<int, AppUserRole>
    {
        public new int Id { get; set; }
    }

    public class AppUserLogin : IdentityUserLogin<int>
    {
        public int Id { get; set; }
    }

    public class AppUserRole : IdentityUserRole<int>
    {
        public int Id { get; set; }
    }

    public class AppUserClaim : IdentityUserClaim<int>
    {
        public override int Id { get; set; }
    }
}