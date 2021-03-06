﻿using System.Data.Entity;
using ScanWord.Core.Entity;
using System.Data.Entity.SqlServer;

namespace ScanWord.DataAccess
{
    /// <summary>Represents a Unit Of Work with ScanWord database.</summary>
    public class ScanWordContainer : DbContext
    {
        /// <summary>Initializes a new instance of the <see cref="ScanWordContainer"/> class.</summary>
        public ScanWordContainer()
            : base("name=ScanWord")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<ScanWordContainer>());
        }

        /// <summary>Gets or sets the files.</summary>
        public DbSet<File> Files { get; set; }

        /// <summary>Gets or sets the words.</summary>
        public DbSet<Word> Words { get; set; }

        /// <summary>Gets or sets the compositions.</summary>
        public DbSet<Composition> Compositions { get; set; }

        /// <summary>Configures model with fluent API.</summary>
        /// <param name="modelBuilder">The model builder.</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<File>().HasKey(f => f.Id);
            modelBuilder.Entity<Word>().HasKey(w => w.Id);
            modelBuilder.Entity<Composition>().HasKey(c => c.Id);
            ////TODO: File full name index
        }
    }
}