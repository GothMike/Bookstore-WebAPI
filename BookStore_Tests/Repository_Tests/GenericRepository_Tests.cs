using Xunit;
using FakeItEasy;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Bookstore_WebAPI.Data.Models;
using Bookstore_WebAPI.Data.Repository;
using Bookstore_WebAPI.Persistence.DataContext;
using Bookstore_WebAPI.Data.Repository.Interfaces;
using System.Runtime.CompilerServices;

namespace BookStore_Tests.Repository_Tests
{
    public class GenericRepositoryTests
    {
        private readonly ApplicationContext _context;
        private readonly IGenericRepository<Author> _authorRepository;

        public GenericRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new ApplicationContext(options);
            _authorRepository = new GenericRepository<Author>(_context);
        }


        [Fact]
        public async Task AuthorRepository_GetAllAsync_ShouldReturnAllEntities()
        {
            // Arrange
            var author1 = new Author { FirstName = "John", LastName = "Doe" };
            var author2 = new Author { FirstName = "Jane", LastName = "Smith" };
            await _authorRepository.AddAsync(author1);
            await _authorRepository.AddAsync(author2);
            await _context.SaveChangesAsync();

            // Act
            var authors = await _authorRepository.GetAllAsync();

            // Assert
            authors.Should().Contain(author1);
            authors.Should().Contain(author2);
        }

        [Fact]
        public async Task AuthorRepository_GetByIdAsync_ShouldReturnEntity()
        {
            // Arrange
            var author1 = new Author {  FirstName = "John", LastName = "Doe" };
            var author2 = new Author { FirstName = "Jane", LastName = "Smith" };
            await _authorRepository.AddAsync(author1);
            await _authorRepository.AddAsync(author2);
            await _context.SaveChangesAsync();

            // Act
            var author = await _authorRepository.GetByIdAsync(author2.Id);

            // Assert
            author.Should().NotBeNull();
            author.Should().Be(author2);
        }

        [Fact]
        public async Task AuthorRepository_AddAsync_ShouldAddEntityToDatabase()
        {
            // Arrange
            var author = new Author { FirstName = "John", LastName = "Doe" };

            // Act
            await _authorRepository.AddAsync(author);
            await _context.SaveChangesAsync();

            // Assert
            var authorsInDb = await _context.Authors.ToListAsync();
            authorsInDb.Should().Contain(author);
        }

        [Fact]
        public async Task AuthorRepository_Delete_ShouldRemoveEntityFromDatabaseAsync()
        {
            // Arrange
            var author = new Author { FirstName = "John", LastName = "Doe" };
            await _authorRepository.AddAsync(author);
            await _context.SaveChangesAsync();

            // Act
            _authorRepository.Delete(author);
            await _context.SaveChangesAsync();

            // Assert
            var authorInDb = await _context.Authors.FindAsync(author.Id);
            authorInDb.Should().BeNull();
        }

        [Fact]
        public async Task AuthorRepository_DeleteAllEntities_ShouldRemoveEntitiesFromDatabaseAsync()
        {
            // Arrange
            var author1 = new Author {  FirstName = "John", LastName = "Doe" };
            var author2 = new Author {  FirstName = "Jane", LastName = "Smith" };
            await _authorRepository.AddAsync(author1);
            await _authorRepository.AddAsync(author2);
            await _context.SaveChangesAsync();
            var authors = await _authorRepository.GetAllAsync();

            // Act
            _authorRepository.DeleteAllEntites(authors);
            await _context.SaveChangesAsync();

            // Assert
            var authorsInDb = await _context.Authors.ToListAsync();
            authorsInDb.Should().BeEmpty();
        }

        [Fact]
        public async Task AuthorRepository_Update_ShouldUpdateEntityInDatabase()
        {
            // Arrange
            var author = new Author
            {
                FirstName = "John",
                LastName = "Doe"
            };
            await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();

            // Act
            author.LastName = "Smith";
            _authorRepository.Update(author);
            await _context.SaveChangesAsync();

            // Assert
            var result = await _context.Authors.FindAsync(author.Id);
            result.LastName.Should().Be("Smith");
        }
    }
}