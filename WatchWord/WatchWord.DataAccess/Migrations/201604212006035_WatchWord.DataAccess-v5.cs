namespace WatchWord.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class WatchWordDataAccessv5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("Material", "MimeType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("Material", "MimeType");
        }
    }
}