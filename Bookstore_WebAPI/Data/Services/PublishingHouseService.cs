using AutoMapper;
using Bookstore_WebAPI.Data.Models;
using Bookstore_WebAPI.Data.Models.Dto;
using Bookstore_WebAPI.Data.Services.Interfaces;
using Bookstore_WebAPI.Persistence.UnitOfWork;

namespace Bookstore_WebAPI.Data.Services
{
    public class PublishingHouseService : IPublishingHouseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PublishingHouseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<PublishingHouse> GetEntityByIdAsync(int id)
        {
            return await _unitOfWork.PublishingHouseGenericRepository.GetByIdAsync(id);
        }
        public async Task<IEnumerable<PublishingHouseDto>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<PublishingHouseDto>>(await _unitOfWork.PublishingHouseGenericRepository.GetAllAsync());
        }
        public async Task<PublishingHouseDto> GetMapEntityByIdAsync(int id)
        {
            return _mapper.Map<PublishingHouseDto>(await _unitOfWork.PublishingHouseGenericRepository.GetByIdAsync(id));
        }
        public async Task CreatePublishingHouseAsync(PublishingHouseDto entityDto)
        {
            await _unitOfWork.PublishingHouseGenericRepository.AddAsync(ConvertToMapEntity(entityDto));
            await _unitOfWork.SaveAsync();
        }
        public async Task Update(PublishingHouseDto entityDto, PublishingHouse entity)
        {
            entity.Name = entityDto.Name;

            _unitOfWork.PublishingHouseGenericRepository.Update(entity);
            await _unitOfWork.SaveAsync();
        }
        public async Task DeleteAsync(PublishingHouse entity)
        {
            var publishingHouses = await _unitOfWork.BookRepository.GetAllPublishingHouseBooks(entity.Id);

            if (publishingHouses.Count() != 0)
                _unitOfWork.BookGenericRepository.DeleteAllEntites(publishingHouses);

            _unitOfWork.PublishingHouseGenericRepository.Delete(entity);

            await _unitOfWork.SaveAsync();
        }
        public PublishingHouse ConvertToMapEntity(PublishingHouseDto entityDto)
        {
            return _mapper.Map<PublishingHouse>(entityDto);
        }
    }
}
