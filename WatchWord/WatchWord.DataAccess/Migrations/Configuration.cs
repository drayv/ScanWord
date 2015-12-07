using MySql.Data.Entity;
using System.Data.Entity.Migrations;

namespace WatchWord.DataAccess.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<WatchDataContainer>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            SetSqlGenerator("MySql.Data.MySqlClient", new MySqlMigrationSqlGenerator());
            CodeGenerator = new MySqlMigrationCodeGenerator();
        }

        protected override void Seed(WatchDataContainer context)
        {
        }
    }
}