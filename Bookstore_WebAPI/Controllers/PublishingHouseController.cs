using Bookstore_WebAPI.Data.Models.Dto;
using Bookstore_WebAPI.Data.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishingHouseController : ControllerBase
    {
        private readonly IPublishingHouseService _publishingHouseService;

        public PublishingHouseController(IPublishingHouseService publishingHouseService)
        {
            _publishingHouseService = publishingHouseService;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PublishingHouseDto>))]
        public async Task<IActionResult> GetPublishingHousesAsync()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(await _publishingHouseService.GetAllAsync());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(PublishingHouseDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetPublishingHouseAsync(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = await _publishingHouseService.GetMapEntityByIdAsync(id);

            if (entity == null)
                return NotFound();

            return Ok(entity);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreatePublishingHouseAsync([FromBody] PublishingHouseDto entityDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (entityDto == null)
                return BadRequest(ModelState);

            await _publishingHouseService.CreatePublishingHouseAsync(entityDto);

            return Ok("Успешно создано");
        }

        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdatePublishingHouseAsync(int id, [FromBody] PublishingHouseDto entityDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (entityDto == null)
                return BadRequest();

            if (id != entityDto.Id)
                return BadRequest();

            var entity = await _publishingHouseService.GetEntityByIdAsync(id);

            if (entity == null)
                return NotFound();

            await _publishingHouseService.Update(entityDto, entity);

            return Ok($"Успешно отредактировано");
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeletePublishingHouseAsync(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = await _publishingHouseService.GetEntityByIdAsync(id);

            if (entity == null)
                return NotFound();

            await _publishingHouseService.DeleteAsync(entity);

            return Ok($"Успешно удалено");
        }
    }
}
