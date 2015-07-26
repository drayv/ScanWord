using System.Data.Entity;
using ScanWord.Core.Entity;
using WatchWord.Domain;
using WatchWord.Domain.Entity;

namespace WatchWord.Data.Sql
{
    /// <summary>Represents a Unit Of Work with WatchWord database.</summary>
    public class WatchDataContainer : DbContext
    {
        /// <summary>Initializes a new instance of the <see cref="WatchDataContainer"/> class.</summary>
        public WatchDataContainer()
            : base("name=WatchWord")
        {
        }

        #region ScanWord
        /// <summary>Gets or sets the files.</summary>
        public DbSet<File> Files { get; set; }

        /// <summary>Gets or sets the words.</summary>
        public DbSet<Word> Words { get; set; }

        /// <summary>Gets or sets the compositions.</summary>
        public DbSet<Composition> Compositions { get; set; }
        #endregion

        /// <summary>Gets or sets the accounts.</summary>
        public DbSet<Account> Accounts { get; set; }

        /// <summary>Gets or sets the materials.</summary>
        public DbSet<Material> Materials { get; set; }

        /// <summary>Configure model with fluent API.</summary>
        /// <param name="modelBuilder">The model builder.</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            #region ScanWord
            modelBuilder.Entity<File>().HasKey(f => f.Id);
            modelBuilder.Entity<Word>().HasKey(w => w.Id);
            modelBuilder.Entity<Composition>().HasKey(c => c.Id);
            #endregion

            modelBuilder.Entity<Account>().HasKey(a => a.Id);
            modelBuilder.Entity<Material>().HasKey(m => m.Id).Ignore(m => m.Words);
        }
    }
}