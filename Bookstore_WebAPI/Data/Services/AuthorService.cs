using AutoMapper;
using Bookstore_WebAPI.Data.Models;
using Bookstore_WebAPI.Data.Models.Dto;
using Bookstore_WebAPI.Data.Services.Interfaces;
using Bookstore_WebAPI.Persistence.UnitOfWork;

namespace Bookstore_WebAPI.Data.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AuthorService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Author> GetEntityByIdAsync(int id)
        {
            return  await _unitOfWork.AuthorGenericRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<AuthorDto>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<AuthorDto>>(await _unitOfWork.AuthorGenericRepository.GetAllAsync());
        }

        public async Task<AuthorDto> GetMapEntityByIdAsync(int id)
        {
            return _mapper.Map<AuthorDto>(await _unitOfWork.AuthorGenericRepository.GetByIdAsync(id));
        }

        public async Task<IEnumerable<BookDto>> GetAllMappingAuthorBooks(int id)
        {
            return _mapper.Map<ICollection<BookDto>>(await _unitOfWork.BookRepository.GetAllAuthorsBooks(id));
        }

        public async Task CreateAuthorAsync(AuthorDto entityDto)
        {
            await _unitOfWork.AuthorGenericRepository.AddAsync(ConvertToMapEntity(entityDto));
            await _unitOfWork.SaveAsync();
        }

        public async Task Update(AuthorDto entityDto, Author entity)
        {
            entity.FirstName = entityDto.FirstName;
            entity.LastName = entityDto.LastName;

            _unitOfWork.AuthorGenericRepository.Update(entity);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(Author entity)
        {
            var authorBooks = await _unitOfWork.BookRepository.GetAllAuthorsBooks(entity.Id);

            if (authorBooks.Count() != 0)
                _unitOfWork.BookGenericRepository.DeleteAllEntites(authorBooks);

            _unitOfWork.AuthorGenericRepository.Delete(entity);

            await _unitOfWork.SaveAsync();
        }

        public Author ConvertToMapEntity(AuthorDto entityDto)
        {
            return _mapper.Map<Author>(entityDto);
        }
    }
}
