using MySql.Data.Entity;
using System.Data.Entity.Migrations;

namespace WatchWord.Web.UI.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<Identity.AppIdentityDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            SetSqlGenerator("MySql.Data.MySqlClient", new MySqlMigrationSqlGenerator());
            CodeGenerator = new MySqlMigrationCodeGenerator();
        }

        protected override void Seed(Identity.AppIdentityDbContext context)
        {
        }
    }
}