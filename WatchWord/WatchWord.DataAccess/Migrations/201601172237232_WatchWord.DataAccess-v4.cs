namespace WatchWord.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class WatchWordDataAccessv4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Translation",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Word = c.String(),
                    Translate = c.String(),
                    AddDate = c.DateTime(nullable: false),
                    Source = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id);

        }

        public override void Down()
        {
            DropTable("Translation");
        }
    }
}
