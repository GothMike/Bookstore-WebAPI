using Bookstore_WebAPI.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Bookstore_WebAPI.Persistence.DataContext
{
    public class AuthorPublishingHousesConfiguration : IEntityTypeConfiguration<AuthorPublishingHouses>
    {
        public void Configure(EntityTypeBuilder<AuthorPublishingHouses> builder)
        {
            builder
                .HasOne(aph => aph.Author)
                .WithMany(a => a.AuthorPublishingHouses)
                .HasForeignKey(aph => aph.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);
            builder
                .HasOne(p => p.PublishingHouse)
                .WithMany(pc => pc.AuthorsPublishingHouses)
                .HasForeignKey(c => c.PublishingHouseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

    public class AuthorBooksConfiguration : IEntityTypeConfiguration<AuthorBooks>
    {
        public void Configure(EntityTypeBuilder<AuthorBooks> builder)
        {
            builder
                .HasOne(ab => ab.Author)
                .WithMany(a => a.AuthorBooks)
                .HasForeignKey(ab => ab.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);
            builder
                .HasOne(ab => ab.Book)
                .WithMany(b => b.AuthorBooks)
                .HasForeignKey(ab => ab.BookId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder
                .Property(e => e.FirstName)
                .HasMaxLength(30)
                .IsRequired();
            builder
                .Property(e => e.LastName)
                .HasMaxLength(30)
                .IsRequired();
        }
    }

    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder
                .Property(e => e.Name)
                .HasMaxLength(50)
                .IsRequired();
            builder
                .HasOne(b => b.PublishingHouse)
                .WithMany(ph => ph.Books)
                .HasForeignKey(b => b.PublishingHouseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

    public class PublishingHouseConfiguration : IEntityTypeConfiguration<PublishingHouse>
    {
        public void Configure(EntityTypeBuilder<PublishingHouse> builder)
        {
            builder
                .Property(e => e.Name)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
