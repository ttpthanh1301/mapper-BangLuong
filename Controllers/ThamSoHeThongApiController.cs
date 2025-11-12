using BangLuong.Services;
using BangLuong.ViewModels;
using Microsoft.AspNetCore.Mvc;
using static BangLuong.ViewModels.ThamSoHeThongViewModels;

namespace BangLuong.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class ThamSoHeThongApiController : ControllerBase
    {
        private readonly IThamSoHeThongService _service;

        public ThamSoHeThongApiController(IThamSoHeThongService service)
        {
            _service = service;
        }

        // GET: api/ThamSoHeThong
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] string? sortOrder,
            [FromQuery] string? currentFilter,
            [FromQuery] string? searchString,
            [FromQuery] int? pageNumber,
            [FromQuery] int pageSize = 10)
        {
            var list = await _service.GetAllFilter(
                sortOrder ?? "",
                currentFilter ?? "",
                searchString ?? "",
                pageNumber,
                pageSize);
            return Ok(list);
        }

        // GET: api/ThamSoHeThong/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        // POST: api/ThamSoHeThong
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ThamSoHeThongRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _service.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = request.MaTS }, request);
        }

        // PUT: api/ThamSoHeThong/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] ThamSoHeThongViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _service.UpdateAsync(id, model);
            if (!result) return NotFound();

            return Ok(model);
        }

        // DELETE: api/ThamSoHeThong/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();

            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
