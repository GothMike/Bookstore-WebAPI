using Xunit;
using FakeItEasy;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Bookstore_WebAPI.Data.Models;
using Bookstore_WebAPI.Data.Repository;
using Bookstore_WebAPI.Persistence.DataContext;

namespace BookStore_Tests.Repository_Tests
{
    public class GenericRepositoryTests
    {
        private readonly ApplicationContext _dbContext;
        private readonly GenericRepository<Author> _repository;
        private readonly List<Author> _authors;

        public GenericRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _dbContext = new ApplicationContext(options);

            _dbContext.Database.EnsureCreated();

            _repository = new GenericRepository<Author>(_dbContext);
            _authors = new List<Author>
        {
            new Author { Id = 1, FirstName = "FirstName1", LastName = "LastName1" },
            new Author { Id = 2, FirstName = "FirstName2", LastName = "LastName2" },
            new Author { Id = 3, FirstName = "FirstName3", LastName = "LastName3" }
        };


            _dbContext.AddRange(_authors);
            _dbContext.SaveChanges();
        }

        [Fact]
        public async Task AuthorRepository_GetAllAsync_ShouldReturnAllEntities()
        {
            // Arrange

            // Act
            var result = await _repository.GetAllAsync();

            // Assert
            result.Should().HaveCount(3);
        }

        [Fact]
        public async Task AuthorRepository_GetByIdAsync_ShouldReturnEntity()
        {
            // Arrange
            var id = _authors[0].Id;

            // Act
            var result = await _repository.GetByIdAsync(id);

            // Assert
            result.Should().BeEquivalentTo(_authors[0]);
        }

        [Fact]
        public async Task AuthorRepository_GetByIdAsyncShouldReturnNull()
        {
            // Arrange
            var notExistingId = 4;

            // Act
            var result = await _repository.GetByIdAsync(notExistingId);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task AuthorRepository_AddAsync_ShouldAddEntityToDatabase()
        {
            // Arrange
            var newAuthor = new Author { Id = 4, FirstName = "FirstName4", LastName = "LastName4" };

            // Act
            await _repository.AddAsync(newAuthor);
            await _dbContext.SaveChangesAsync();

            // Assert
            var result = await _dbContext.Authors.ToListAsync();
            result.Should().HaveCount(_authors.Count + 1);
            result.Should().ContainEquivalentOf(newAuthor);
        }

        [Fact]
        public void AuthorRepository_Delete_ShouldRemoveEntityFromDatabase()
        {
            // Arrange
            var authorToRemove = _authors[0];

            // Act
            _repository.Delete(authorToRemove);
            _dbContext.SaveChanges();

            // Assert
            var result = _dbContext.Authors.Find(authorToRemove.Id);
            result.Should().BeNull();
        }

        [Fact]
        public void AuthorRepository_DeleteAllEntities_ShouldRemoveEntitiesFromDatabase()
        {
            // Arrange
            var authorsToRemove = new List<Author>
        {
            _authors[0],
            _authors[1]
        };

            // Act
            _repository.DeleteAllEntites(authorsToRemove);
            _dbContext.SaveChanges();

            // Assert
            var result = _dbContext.Authors.ToList();
            result.Should().HaveCount(_authors.Count - authorsToRemove.Count);
            result.Should().NotContain(authorsToRemove);
        }

        [Fact]
        public async Task AuthorRepository_Update_ShouldUpdateEntityInDatabase()
        {
            // Arrange
            var authorToUpdate = _authors[0];
            var updatedAuthor = new Author { Id = authorToUpdate.Id, FirstName = "UpdatedFirstName", LastName = "UpdatedLastName" };

            // Act
            _repository.Update(authorToUpdate);
            _dbContext.Entry(authorToUpdate).State = EntityState.Detached;
            _repository.Update(updatedAuthor);
            await _dbContext.SaveChangesAsync();

            // Assert
            var result = await _dbContext.Authors.FindAsync(authorToUpdate.Id);
            result.Should().BeEquivalentTo(updatedAuthor);
        }
    }
}