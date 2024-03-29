﻿using Bookstore_WebAPI.Data.Models;
using Bookstore_WebAPI.Data.Repository.Interfaces;
using Bookstore_WebAPI.Persistence.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Bookstore_WebAPI.Data.Repository
{
    public class PublishingHouseRepository : IPublishingHouseRepository
    {
        private readonly ApplicationContext _context;

        public PublishingHouseRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<AuthorPublishingHouses> GetAPHById(int id)
        {
            return await _context.AuthorPublishingHouses.FirstOrDefaultAsync(a => a.PublishingHouseId == id);
        }
    }
}
