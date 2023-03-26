using Bookstore_WebAPI.Data.Models;
using Bookstore_WebAPI.Data.Repository;
using Bookstore_WebAPI.Persistence.DataContext;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace BookStore_Tests.Repository_Tests
{
    public class AuthorRepository_Tests
    {
        private readonly ApplicationContext _context;
        public AuthorRepository_Tests()
        {
            // настройка контекста для тестов
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: "test_database")
                .Options;
            _context = new ApplicationContext(options);

            // добавление тестовых данных
            _context.Authors.Add(new Author { Id = 1, FirstName = "John", LastName = "Doe" });
            _context.Authors.Add(new Author { Id = 2, FirstName = "Jane", LastName = "Doe" });
            _context.Books.Add(new Book { Id = 1, Name = "Book 1", PublishingHouseId = 1 });
            _context.Books.Add(new Book { Id = 2, Name = "Book 2", PublishingHouseId = 2 });
            _context.AuthorBooks.Add(new AuthorBooks { AuthorId = 1, BookId = 1 });
            _context.AuthorBooks.Add(new AuthorBooks { AuthorId = 1, BookId = 2 });
            _context.AuthorBooks.Add(new AuthorBooks { AuthorId = 2, BookId = 2 });
            _context.PublishingHouses.Add(new PublishingHouse { Id = 1, Name = "Publisher 1" });
            _context.PublishingHouses.Add(new PublishingHouse { Id = 2, Name = "Publisher 2" });
            _context.SaveChanges();
        }

        [Fact]
        public async Task AuthorRepository_GetById_ShouldReturnCorrectBooks()
        {
            // Arrange
            var repository = new AuthorRepository(_context);

            // Act
            var authorBooks = await repository.GetById(1);

            // Assert
            authorBooks.Should().HaveCount(2); // проверка количества книг у автора
            authorBooks.Should().Contain(x => x.Book.Name == "Book 1"); // проверка наличия книги
            authorBooks.Should().Contain(x => x.Book.Name == "Book 2");
        }
    }
}