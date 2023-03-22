using AutoMapper;
using Bookstore_WebAPI.Data.Models;
using Bookstore_WebAPI.Data.Models.Dto;
using Bookstore_WebAPI.Data.Services.Interfaces;
using Bookstore_WebAPI.Persistence.UnitOfWork;

namespace Bookstore_WebAPI.Data.Services
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Book> GetEntityByIdAsync(int id)
        {
            return await _unitOfWork.BookGenericRepository.GetByIdAsync(id);
        }
        public async Task<IEnumerable<BookDto>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<BookDto>>(await _unitOfWork.BookGenericRepository.GetAllAsync());
        }
        public async Task<BookDto> GetMapEntityByIdAsync(int id)
        {
            return _mapper.Map<BookDto>(await _unitOfWork.BookGenericRepository.GetByIdAsync(id));
        }
        public async Task CreateBookAsync(BookDto entityDto, int mainAuthorId, int publishingHouseId)
        {
            var author = await _unitOfWork.AuthorGenericRepository.GetByIdAsync(mainAuthorId);
            var publishingHouse = await _unitOfWork.PublishingHouseGenericRepository.GetByIdAsync(publishingHouseId);
            var book = ConvertToMapEntity(entityDto);
            book.PublishingHouse = publishingHouse;

            var authorBooks = new AuthorBooks
            {
                Author = author,
                Book = book,
            };

            var authorPublishingHouses = new AuthorPublishingHouses
            {
                Author = author,
                PublishingHouse = publishingHouse,
            };

            await _unitOfWork.BookGenericRepository.AddAsync(book);
            await _unitOfWork.AuthorBooksGenericRepository.AddAsync(authorBooks);
            await _unitOfWork.AuthorPublishingHousesRepository.AddAsync(authorPublishingHouses);
            await _unitOfWork.SaveAsync();
        }
        public async Task Update(BookDto entityDto, Book entity)
        {
            entity.Name = entityDto.Name;

            _unitOfWork.BookGenericRepository.Update(entity);
            await _unitOfWork.SaveAsync();
        }
        public async Task DeleteAsync(Book entity)
        {
            _unitOfWork.BookGenericRepository.Delete(entity);
            await _unitOfWork.SaveAsync();
        }
        public Book ConvertToMapEntity(BookDto entityDto)
        {
            return _mapper.Map<Book>(entityDto);
        }

        public async Task<bool> CheckDepentEntities(int mainAuthorId, int publishingHouseId)
        {
            var author = await _unitOfWork.AuthorGenericRepository.GetByIdAsync(mainAuthorId);
            var publishingHouse = await _unitOfWork.PublishingHouseGenericRepository.GetByIdAsync(publishingHouseId);

            if (author == null)
                return false;
            if(publishingHouse == null) 
                return false;

            return true;
        }
    }
}
