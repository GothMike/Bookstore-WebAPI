using AutoMapper;
using Bookstore_WebAPI.Data.Models;
using Bookstore_WebAPI.Data.Models.Dto;
using Bookstore_WebAPI.Data.Services.Interfaces;
using Bookstore_WebAPI.Persistence.UnitOfWork;

namespace Bookstore_WebAPI.Data.Services
{
    public class BookService : BaseAbstractService<Book,BookDto> ,IBookService
        {

        public BookService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        
        public async Task CreateBookAsync(BookDto entityDto, int mainAuthorId, int publishingHouseId)
        {
            var author = await _unitOfWork.CreateRepository<Author>().GetByIdAsync(mainAuthorId);
            var publishingHouse = await _unitOfWork.CreateRepository<PublishingHouse>().GetByIdAsync(publishingHouseId);
            var authorPublishingHouses = await _unitOfWork.PublishingHouseRepository.GetAPHById(publishingHouseId);

                var book = ConvertToMapEntity(entityDto);
                book.PublishingHouse = publishingHouse;

                var newAuthorBooks = new AuthorBooks
                {
                    Author = author,
                    Book = book,
                };

            if (authorPublishingHouses == null)
            {
                var newAuthorPublishingHouses = new AuthorPublishingHouses
                {
                    Author = author,
                    PublishingHouse = publishingHouse,
                };
                await _unitOfWork.CreateRepository<AuthorPublishingHouses>().AddAsync(newAuthorPublishingHouses);
            }

                await _unitOfWork.CreateRepository<Book>().AddAsync(book);
                await _unitOfWork.CreateRepository<AuthorBooks>().AddAsync(newAuthorBooks);

            await _unitOfWork.SaveAsync();
        }

        public async Task Update(BookDto entityDto, Book entity)
        {
            entity.Name = entityDto.Name;

            _unitOfWork.CreateRepository<Book>().Update(entity);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(Book entity)
        {
            _unitOfWork.CreateRepository<Book>().Delete(entity);
            await _unitOfWork.SaveAsync();
        }

        public async Task<bool> CheckDepentEntities(int mainAuthorId, int publishingHouseId)
        {
            var author = await _unitOfWork.CreateRepository<Author>().GetByIdAsync(mainAuthorId);
            var publishingHouse = await _unitOfWork.CreateRepository<PublishingHouse>().GetByIdAsync(publishingHouseId);

            if (author == null)
                return false;
            if(publishingHouse == null) 
                return false;

            return true;
        }
    }
}
