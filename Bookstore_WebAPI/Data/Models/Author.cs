using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookstore_WebAPI.Data.Models
{
    public class Author
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public ICollection<AuthorPublishingHouses> AuthorPublishingHouses { get; set; }
        public ICollection<AuthorBooks> AuthorBooks { get; set; }
    }
}
