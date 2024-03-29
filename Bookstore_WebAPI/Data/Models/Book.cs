﻿using System.ComponentModel.DataAnnotations;

namespace Bookstore_WebAPI.Data.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PublishingHouseId { get; set; }
        public PublishingHouse PublishingHouse { get; set; }
        public ICollection<AuthorBooks> AuthorBooks { get; set; }
    }
}
