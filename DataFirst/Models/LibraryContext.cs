using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataFirst.Models
{
    public partial class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options):base(options)
        {
        }

        public virtual DbSet<Author> Author { get; set; }
        public virtual DbSet<Book> Book { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=Library;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasIndex(e => e.AuthorId);

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Book)
                    .HasForeignKey(d => d.AuthorId);
            });
        }
    }
}
