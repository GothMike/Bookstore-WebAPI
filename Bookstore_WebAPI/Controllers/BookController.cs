using Bookstore_WebAPI.Data.Models.Dto;
using Bookstore_WebAPI.Data.Models;
using Bookstore_WebAPI.Data.Repository;
using Bookstore_WebAPI.Data.Repository.Interfaces;
using Bookstore_WebAPI.Data.Services;
using Bookstore_WebAPI.Data.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<BookDto>))]
        public async Task<IActionResult> GetBooksAsync()
        {
            return Ok(await _bookService.GetAllAsync());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(BookDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetBookAsync(int id)
        {
            var entity = await _bookService.GetMapEntityByIdAsync(id);

            if (entity == null)
                return NotFound();

            return Ok(entity);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateBookAsync([FromBody] BookDto entityDto, int authorId, int publishingHouseId)
        {
            if (entityDto == null)
                return BadRequest(ModelState);

            if (!await _bookService.CheckDepentEntities(authorId, publishingHouseId))
                return NotFound();

           await _bookService.CreateBookAsync(entityDto, authorId, publishingHouseId);

            return Ok("Успешно создано");
        }

        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateBookAsync(int id, [FromBody] BookDto entityDto)
        {
            if (entityDto == null)
                return BadRequest();

            if (id != entityDto.Id)
                return BadRequest();

            var entity = await _bookService.GetEntityByIdAsync(id);

            if (entity == null)
                return NotFound();

            await _bookService.Update(entityDto, entity);

            return Ok($"Успешно отредактировано");
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteBookAsync(int id)
        {
            var entity = await _bookService.GetEntityByIdAsync(id);

            if (entity == null)
                return NotFound();

            await _bookService.DeleteAsync(entity);

            return Ok($"Успешно удалено");
        }
    }
}
