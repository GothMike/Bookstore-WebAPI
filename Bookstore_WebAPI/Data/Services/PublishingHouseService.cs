using AutoMapper;
using Bookstore_WebAPI.Data.Models;
using Bookstore_WebAPI.Data.Models.Dto;
using Bookstore_WebAPI.Data.Services.Interfaces;
using Bookstore_WebAPI.Persistence.UnitOfWork;

namespace Bookstore_WebAPI.Data.Services
{
    public class PublishingHouseService : BaseAbstractService<PublishingHouse, PublishingHouseDto>, IPublishingHouseService
    {
        public PublishingHouseService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public async Task CreatePublishingHouseAsync(PublishingHouseDto entityDto)
        {
            await _unitOfWork.CreateRepository<PublishingHouse>().AddAsync(ConvertToMapEntity(entityDto));
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(PublishingHouse entity)
        {
            _unitOfWork.CreateRepository<PublishingHouse>().Delete(entity);
            await _unitOfWork.SaveAsync();
        }

        public async Task Update(PublishingHouseDto entityDto, PublishingHouse entity)
        {
            entity.Name = entityDto.Name;

            _unitOfWork.CreateRepository<PublishingHouse>().Update(entity);
            await _unitOfWork.SaveAsync();
        }

    }
}
