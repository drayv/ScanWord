namespace WatchWord.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class WatchWordDataAccessv2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "KnownWord",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Word = c.String(),
                    Translation = c.String(),
                    Owner_Id = c.Int(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "LearnWord",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Word = c.String(),
                    Translation = c.String(),
                    Owner_Id = c.Int(),
                })
                .PrimaryKey(t => t.Id);

            AlterColumn("Word", "TheWord", c => c.String());
            AlterColumn("File", "Extension", c => c.String());
            AlterColumn("File", "Filename", c => c.String());
            AlterColumn("File", "Path", c => c.String());
            AlterColumn("File", "FullName", c => c.String());
            AlterColumn("Material", "Name", c => c.String());
            AlterColumn("Material", "Description", c => c.String());
        }

        public override void Down()
        {
            DropForeignKey("LearnWord", "FK_dbo.LearnWord_dbo.Account_Owner_Id");
            DropForeignKey("KnownWord", "FK_dbo.KnownWord_dbo.Account_Owner_Id");
            DropIndex("LearnWord", new[] { "Owner_Id" });
            DropIndex("KnownWord", new[] { "Owner_Id" });
            AlterColumn("Material", "Description", c => c.String(unicode: false));
            AlterColumn("Material", "Name", c => c.String(unicode: false));
            AlterColumn("File", "FullName", c => c.String(unicode: false));
            AlterColumn("File", "Path", c => c.String(unicode: false));
            AlterColumn("File", "Filename", c => c.String(unicode: false));
            AlterColumn("File", "Extension", c => c.String(unicode: false));
            AlterColumn("Word", "TheWord", c => c.String(unicode: false));
            DropTable("LearnWord");
            DropTable("KnownWord");
        }
    }
}