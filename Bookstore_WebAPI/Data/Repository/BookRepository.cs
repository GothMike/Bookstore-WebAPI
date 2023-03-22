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

        public async Task<bool> EntityExistsAsync(int Id)
        {
            return await _context.Books.AnyAsync(b => b.Id == Id);
        }

        public async Task<List<Book>> GetAllPublishingHouseBooks(int id)
        {
            var publishingHouseBooks = await _context.Books.Where(i => i.PublishingHouseId == id).ToListAsync();
            var books = new List<Book>();

            foreach (var book in publishingHouseBooks)
            {
                books.Add(await _context.Books.FirstOrDefaultAsync(b => b.Id == book.PublishingHouseId));
            }

            return books;
        }

        public async Task<IEnumerable<Book>> GetAllAuthorsBooks(int id)
        {
            var authorBooks = await _context.AuthorBooks.Where(i => i.AuthorId == id).ToListAsync();
            var books = new List<Book>();

            foreach (var book in authorBooks)
            {
                books.Add(await _context.Books.FirstOrDefaultAsync(b => b.Id == book.Id));
            }

            return books;
        }
    }
}
