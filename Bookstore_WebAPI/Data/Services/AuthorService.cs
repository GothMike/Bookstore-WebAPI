using AutoMapper;
using Bookstore_WebAPI.Data.Models;
using Bookstore_WebAPI.Data.Models.Dto;
using Bookstore_WebAPI.Data.Services.Interfaces;
using Bookstore_WebAPI.Persistence.UnitOfWork;

namespace Bookstore_WebAPI.Data.Services
{
    public class AuthorService : BaseAbstractService<Author, AuthorDto>, IAuthorService
    {
        public AuthorService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public async Task<IEnumerable<BookDto>> GetAllMappingAuthorBooks(int id)
        {
            return _mapper.Map<ICollection<BookDto>>(await _unitOfWork.BookRepository.GetAllAuthorsBooks(id));
        }

        public async Task CreateAuthorAsync(AuthorDto entityDto)
        {
            await _unitOfWork.CreateRepository<Author>().AddAsync(ConvertToMapEntity(entityDto));
            await _unitOfWork.SaveAsync();
        }

        public async Task Update(AuthorDto entityDto, Author entity)
        {
            entity.FirstName = entityDto.FirstName;
            entity.LastName = entityDto.LastName;

            _unitOfWork.CreateRepository<Author>().Update(entity);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(Author entity)
        {
            var authorBooks = await _unitOfWork.BookRepository.GetAllAuthorsBooks(entity.Id);

            if (authorBooks.Count() != 0)
                _unitOfWork.CreateRepository<Book>().DeleteAllEntites(authorBooks);

            _unitOfWork.CreateRepository<Author>().Delete(entity);

            await _unitOfWork.SaveAsync();
        }
    }
}
