using Bookstore_WebAPI.Data.Models.Dto;
using Bookstore_WebAPI.Data.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AuthorDto>))]
        public async Task<IActionResult> GetAuthorsAsync()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(await _authorService.GetAllAsync());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(AuthorDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAuthorAsync(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = await _authorService.GetMapEntityByIdAsync(id);

            if (entity == null)
                return NotFound();

            return Ok(entity);
        }

        [HttpGet("{id}/books")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AuthorDto>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAllAuthorBooksAsync(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _authorService.GetMapEntityByIdAsync(id) == null)
                return NotFound();

            return Ok(await _authorService.GetAllMappingAuthorBooks(id));
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateAuthorAsync([FromBody] AuthorDto entityDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (entityDto == null)
                return BadRequest(ModelState);

            await _authorService.CreateAuthorAsync(entityDto);

            return Ok("Успешно создано");
        }

        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateAuthorAsync(int id, [FromBody] AuthorDto entityDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (entityDto == null)
                return BadRequest();

            if (id != entityDto.Id)
                return BadRequest();

            var entity = await _authorService.GetEntityByIdAsync(id);

            if (entity == null)
                return NotFound();

            await _authorService.Update(entityDto, entity);

            return Ok($"Успешно отредактировано");
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteAuthorAsync(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = await _authorService.GetEntityByIdAsync(id);

            if (entity == null)
                return NotFound();

            await _authorService.DeleteAsync(entity);

            return Ok($"Успещно удалено");
        }
    }
}
