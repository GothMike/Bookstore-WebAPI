using Bookstore_WebAPI.Data.Models;
using Bookstore_WebAPI.Data.Repository;
using Bookstore_WebAPI.Data.Repository.Interfaces;
using Bookstore_WebAPI.Persistence.DataContext;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BookStore_Tests.Repository_Tests
{
    public class BookRepository_Tests
    {
        private IBookRepository _bookRepository;
        private ApplicationContext _context;
        public async Task<ApplicationContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                           .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                           .Options;

            var databaseContext = new ApplicationContext(options);

            databaseContext.Database.EnsureCreated();

            AddToDatabaseAsync(databaseContext);

            await databaseContext.SaveChangesAsync();

            return databaseContext;

            async Task AddToDatabaseAsync(ApplicationContext databaseContext)
            {
                var author = new Author { Id = 1, FirstName = "John", LastName = "Doe" };
                var publishingHouse = new PublishingHouse { Id = 1, Name = "Publishing House A" };

                var book = new Book { Id = 1, Name = "Book A", PublishingHouseId = publishingHouse.Id, PublishingHouse = publishingHouse };
                var book2 = new Book { Id = 2, Name = "Book B", PublishingHouseId = publishingHouse.Id, PublishingHouse = publishingHouse };
                var book3 = new Book { Id = 3, Name = "Book С", PublishingHouseId = publishingHouse.Id, PublishingHouse = publishingHouse };

                var authorBooks = new AuthorBooks {   Author = author, Book = book };
                var authorBooks2 = new AuthorBooks {  Author = author, Book = book2 };
                var authorBooks3 = new AuthorBooks { Author = author, Book = book3 };

                ICollection<AuthorBooks> collection = new List<AuthorBooks>() { authorBooks };
                ICollection<AuthorBooks> collection2 = new List<AuthorBooks>() { authorBooks2 };
                ICollection<AuthorBooks> collection3 = new List<AuthorBooks>() { authorBooks3 };

                book.AuthorBooks = collection;
                book2.AuthorBooks = collection2;
                book3.AuthorBooks = collection3;

                var authorPublishingHouse = new AuthorPublishingHouses { Author = author, PublishingHouse = publishingHouse };

                await databaseContext.Books.AddRangeAsync(book, book2, book3);
                await databaseContext.AuthorBooks.AddRangeAsync(authorBooks, authorBooks2, authorBooks3);
                await databaseContext.PublishingHouses.AddAsync(publishingHouse);
                await databaseContext.Authors.AddAsync(author);
                await databaseContext.AuthorPublishingHouses.AddAsync(authorPublishingHouse);
            }
        }

        [Fact]
        public async Task GetAllAuthorsBooks_ReturnsListOfBooks()
        {
            // Arrange
            _context = await GetDatabaseContext();
            _bookRepository = new BookRepository(_context);
            int authorId = 1;

            // Act
            var result = await _bookRepository.GetAllAuthorsBooks(authorId);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(3);
        }
    }
}