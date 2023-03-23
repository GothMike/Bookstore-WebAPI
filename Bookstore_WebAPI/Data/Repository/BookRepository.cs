using Bookstore_WebAPI.Data.Models;
using Bookstore_WebAPI.Data.Repository.Interfaces;
using Bookstore_WebAPI.Persistence.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Bookstore_WebAPI.Data.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationContext _context;

        public BookRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAllPublishingHouseBooks(int id)
        {
            var books = await _context.Books.Where(i => i.PublishingHouseId == id).ToListAsync();

            return books;
        }

        public async Task<IEnumerable<Book>> GetAllAuthorsBooks(int id)
        {
            var authorBooks = await _context.AuthorBooks.Where(i => i.AuthorId == id).ToListAsync();
            var books = new List<Book>();

            foreach (var book in authorBooks)
            {
               books.Add(await _context.Books.FindAsync(book.BookId));
            }

            return books;
        }
    }
}
