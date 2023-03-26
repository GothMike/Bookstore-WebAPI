
using Bookstore_WebAPI.Data.Models;
using Bookstore_WebAPI.Data.Repository;
using Bookstore_WebAPI.Data.Repository.Interfaces;
using Bookstore_WebAPI.Persistence.DataContext;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace BookStore_Tests.Repository_Tests
{
    public class PublishingHouseRepository_Tests
    {
        [Fact]
        public async Task PublishingHouseRepository_GetAPHById_ShouldReturnAuthorPublishingHouse()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: "GetAPHById_ShouldReturnAuthorPublishingHouse_WhenCalled")
                .Options;
            var context = new ApplicationContext(options);
            var publishingHouseRepository = new PublishingHouseRepository(context);

            var author = new Author { Id = 1, FirstName = "John", LastName = "Doe" };
            var publishingHouse = new PublishingHouse { Id = 1, Name = "Test Publishing House" };
            var authorPublishingHouse = new AuthorPublishingHouses { Author = author, PublishingHouse = publishingHouse };
            context.AuthorPublishingHouses.Add(authorPublishingHouse);
            await context.SaveChangesAsync();

            // Act
            var result = await publishingHouseRepository.GetAPHById(1);

            // Assert
            result.Should().Be(authorPublishingHouse);
        }
    }
}