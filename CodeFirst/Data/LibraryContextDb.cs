using CodeFirst.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeFirst.Data
{
    public class LibraryContextDb:DbContext
    {
        public LibraryContextDb(DbContextOptions<LibraryContextDb> options)
            : base(options)
        {

        }
        public LibraryContextDb()
        {
                
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                //_ = this.ChangeTracker;
                optionsBuilder.UseSqlServer("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=Library;Integrated Security =True");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().ToTable("Publishers");
            modelBuilder.Entity<Book>().HasKey(b => new { b.Id, b.AuthorId });
            modelBuilder.Entity<Book>().Property(b => b.Id).ValueGeneratedNever();
            modelBuilder.Entity<Book>().Property(b => b.Title).IsRequired();
            modelBuilder.Entity<Book>().Property(b => b.Title).HasMaxLength(40);
            modelBuilder.Entity<Book>().Property(b => b.AuthorId).HasColumnName("publisherId");
            modelBuilder.Entity<Book>().Property(b => b.PublishedDate).HasColumnType("date");

        }
        public DbSet<Author> Author { get; set; }
        public DbSet<Book> Book{ get; set; }
    }
}
