namespace WatchWord.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class WatchWordDataAccessv3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Setting",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Key = c.Int(nullable: false),
                    Type = c.Int(nullable: false),
                    Int = c.Int(),
                    String = c.String(),
                    Boolean = c.Boolean(),
                    Date = c.DateTime(),
                    Owner_Id = c.Int(),
                })
                .PrimaryKey(t => t.Id);

            AddColumn("KnownWord", "Type", c => c.Int(nullable: false));
            AddColumn("LearnWord", "Type", c => c.Int(nullable: false));
        }

        public override void Down()
        {
            DropForeignKey("Setting", "FK_dbo.Setting_dbo.Account_Owner_Id");
            DropIndex("Setting", new[] { "Owner_Id" });
            DropColumn("LearnWord", "Type");
            DropColumn("KnownWord", "Type");
            DropTable("Setting");
        }
    }
}
