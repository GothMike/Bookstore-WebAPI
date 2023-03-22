﻿using Bookstore_WebAPI.Data.Models;
using Bookstore_WebAPI.Data.Models.Dto;

namespace Bookstore_WebAPI.Data.Services.Interfaces
{
    public interface IPublishingHouseService : IBaseService<PublishingHouse, PublishingHouseDto>
    {
        Task CreatePublishingHouseAsync(PublishingHouseDto entityDto);
    }
}
