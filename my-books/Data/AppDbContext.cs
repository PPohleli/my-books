using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using my_books.Data.Models;

namespace my_books.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser> //DbContext
    {
        //define AppDbContext as a file to be used by EF to map the app with the database
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){  }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Configure relationships using Fluent API for EF to map them successfully

            //Book- Book_Author -Author, many-to-many relationship
            modelBuilder.Entity<Book_Author>().HasOne(b => b.Book).WithMany(ba => ba.Book_Authors).HasForeignKey(bi => bi.BookId);
            modelBuilder.Entity<Book_Author>().HasOne(a => a.Author).WithMany(ba => ba.Book_Authors).HasForeignKey(ai => ai.AuthorId);

            //Define primary key for table
            modelBuilder.Entity<Log>().HasKey(l => l.Id);

            base.OnModelCreating(modelBuilder); //IdentityDbContext
        }


        // Define the DbSet for model
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book_Author> Books_Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}
