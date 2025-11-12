using BangLuong.Services;
using BangLuong.ViewModels;
using Microsoft.AspNetCore.Mvc;
using static BangLuong.ViewModels.ChiTietKyLuatViewModels;

namespace BangLuong.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChiTietKyLuatApiController : ControllerBase
    {
        private readonly IChiTietKyLuatService _service;

        public ChiTietKyLuatApiController(IChiTietKyLuatService service)
        {
            _service = service;
        }

        // GET: api/ChiTietKyLuat
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _service.GetAllFilter("", "", "", null, 1000); // Lấy tất cả
            return Ok(list);
        }

        // GET: api/ChiTietKyLuat/filter
        [HttpGet("filter")]
        public async Task<IActionResult> GetAllFilter(
            [FromQuery] string? sortOrder,
            [FromQuery] string? currentFilter,
            [FromQuery] string? searchString,
            [FromQuery] int? pageNumber,
            [FromQuery] int pageSize = 10)
        {
            var filtered = await _service.GetAllFilter(
                sortOrder ?? "",
                currentFilter ?? "",
                searchString ?? "",
                pageNumber,
                pageSize);

            return Ok(filtered);
        }

        // GET: api/ChiTietKyLuat/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        // POST: api/ChiTietKyLuat
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ChiTietKyLuatRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var success = await _service.CreateAsync(request);
            if (!success) return BadRequest("Tạo chi tiết kỷ luật thất bại.");

            return CreatedAtAction(nameof(GetById), new { id = request.MaCTKL }, request);
        }

        // PUT: api/ChiTietKyLuat/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ChiTietKyLuatViewModel model)
        {
            if (id != model.MaCTKL) return BadRequest("Id không hợp lệ.");

            var success = await _service.UpdateAsync(id, model);
            if (!success) return NotFound();

            return Ok(model);
        }

        // DELETE: api/ChiTietKyLuat/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success) return NotFound();

            return NoContent();
        }
    }
}
