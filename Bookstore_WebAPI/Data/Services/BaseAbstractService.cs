using AutoMapper;
using Bookstore_WebAPI.Data.Services.Interfaces;
using Bookstore_WebAPI.Persistence.UnitOfWork;

namespace Bookstore_WebAPI.Data.Services
{
    public abstract class BaseAbstractService<TEntity, TEntityDto> : IBaseAbstractService<TEntity, TEntityDto>
        where TEntity : class
        where TEntityDto : class
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;

        public BaseAbstractService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<TEntity> GetEntityByIdAsync(int id)
        {
            return await _unitOfWork.CreateRepository<TEntity>().GetByIdAsync(id);
        }

        public async Task<IEnumerable<TEntityDto>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<TEntityDto>>(await _unitOfWork.CreateRepository<TEntity>().GetAllAsync());
        }
        public async Task<TEntityDto> GetMapEntityByIdAsync(int id)
        {
            return _mapper.Map<TEntityDto>(await _unitOfWork.CreateRepository<TEntity>().GetByIdAsync(id));
        }

        public TEntity ConvertToMapEntity(TEntityDto entityDto)
        {
            return _mapper.Map<TEntity>(entityDto);
        }


    }
}
