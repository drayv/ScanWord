namespace WatchWord.Web.UI.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class WatchWordWebUIv1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "AppRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "AppUserRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                        AppRole_Id = c.Int(),
                        AppUser_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "AppUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(unicode: false),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(unicode: false),
                        SecurityStamp = c.String(unicode: false),
                        PhoneNumber = c.String(unicode: false),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(precision: 0),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "AppUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ClaimType = c.String(unicode: false),
                        ClaimValue = c.String(unicode: false),
                        AppUser_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "AppUserLogins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LoginProvider = c.String(unicode: false),
                        ProviderKey = c.String(unicode: false),
                        UserId = c.Int(nullable: false),
                        AppUser_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("AppUserRoles", "FK_dbo.AppUserRoles_dbo.AppUsers_AppUser_Id");
            DropForeignKey("AppUserLogins", "FK_dbo.AppUserLogins_dbo.AppUsers_AppUser_Id");
            DropForeignKey("AppUserClaims", "FK_dbo.AppUserClaims_dbo.AppUsers_AppUser_Id");
            DropForeignKey("AppUserRoles", "FK_dbo.AppUserRoles_dbo.AppRoles_AppRole_Id");
            DropIndex("AppUserLogins", new[] { "AppUser_Id" });
            DropIndex("AppUserClaims", new[] { "AppUser_Id" });
            DropIndex("AppUserRoles", new[] { "AppUser_Id" });
            DropIndex("AppUserRoles", new[] { "AppRole_Id" });
            DropTable("AppUserLogins");
            DropTable("AppUserClaims");
            DropTable("AppUsers");
            DropTable("AppUserRoles");
            DropTable("AppRoles");
        }
    }
}
