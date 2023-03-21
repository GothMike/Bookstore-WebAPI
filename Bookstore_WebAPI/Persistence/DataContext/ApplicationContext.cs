using Bookstore_WebAPI.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Bookstore_WebAPI.Persistence.DataContext
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> optins) : base(optins) { }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<PublishingHouse> PublishingHouses { get; set; }
        public DbSet<AuthorBooks> AuthorBooks { get; set; }
        public DbSet<AuthorPublishingHouses> AuthorPublishingHouses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AuthorConfiguration());
            modelBuilder.ApplyConfiguration(new BookConfiguration());
            modelBuilder.ApplyConfiguration(new PublishingHouseConfiguration());
            modelBuilder.ApplyConfiguration(new AuthorBooksConfiguration());
            modelBuilder.ApplyConfiguration(new AuthorPublishingHousesConfiguration());
        }
    }
}
