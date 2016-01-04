namespace WatchWord.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class WatchWordDataAccessv1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Account",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    ExternalId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "Composition",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Line = c.Int(nullable: false),
                    Сolumn = c.Int(nullable: false),
                    Word_Id = c.Int(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "Word",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    TheWord = c.String(unicode: false),
                    Count = c.Int(nullable: false),
                    File_Id = c.Int(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "File",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Extension = c.String(unicode: false),
                    Filename = c.String(unicode: false),
                    Path = c.String(unicode: false),
                    FullName = c.String(unicode: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "Material",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Type = c.Int(nullable: false),
                    Name = c.String(unicode: false),
                    Description = c.String(unicode: false),
                    Image = c.Binary(),
                    File_Id = c.Int(),
                    Owner_Id = c.Int(),
                })
                .PrimaryKey(t => t.Id);

        }

        public override void Down()
        {
            DropForeignKey("Material", "FK_dbo.Material_dbo.Account_Owner_Id");
            DropForeignKey("Material", "FK_dbo.Material_dbo.File_File_Id");
            DropForeignKey("Composition", "FK_dbo.Composition_dbo.Word_Word_Id");
            DropForeignKey("Word", "FK_dbo.Word_dbo.File_File_Id");
            DropIndex("Material", new[] { "Owner_Id" });
            DropIndex("Material", new[] { "File_Id" });
            DropIndex("Word", new[] { "File_Id" });
            DropIndex("Composition", new[] { "Word_Id" });
            DropTable("Material");
            DropTable("File");
            DropTable("Word");
            DropTable("Composition");
            DropTable("Account");
        }
    }
}