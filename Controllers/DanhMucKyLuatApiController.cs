using BangLuong.Services;
using BangLuong.ViewModels;
using Microsoft.AspNetCore.Mvc;
using static BangLuong.ViewModels.DanhMucKyLuatViewModels;

namespace BangLuong.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class DanhMucKyLuatApiController : ControllerBase
    {
        private readonly IDanhMucKyLuatService _service;

        public DanhMucKyLuatApiController(IDanhMucKyLuatService service)
        {
            _service = service;
        }

        // GET: api/DanhMucKyLuat
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _service.GetAllFilter("", "", "", null, 1000); // Lấy tất cả
            return Ok(list);
        }

        // GET: api/DanhMucKyLuat/filter
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

        // GET: api/DanhMucKyLuat/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            if (string.IsNullOrEmpty(id)) return BadRequest("Id không được trống.");

            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();

            return Ok(item);
        }

        // POST: api/DanhMucKyLuat
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DanhMucKyLuatRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var success = await _service.CreateAsync(request);
            if (!success) return BadRequest("Tạo danh mục kỷ luật thất bại.");

            return CreatedAtAction(nameof(GetById), new { id = request.MaKL }, request);
        }

        // PUT: api/DanhMucKyLuat/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] DanhMucKyLuatViewModel model)
        {
            if (string.IsNullOrEmpty(id) || id != model.MaKL) return BadRequest("Id không hợp lệ.");

            var success = await _service.UpdateAsync(model);
            if (!success) return NotFound();

            return Ok(model);
        }

        // DELETE: api/DanhMucKyLuat/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id)) return BadRequest("Id không được trống.");

            var success = await _service.DeleteAsync(id);
            if (!success) return NotFound();

            return NoContent();
        }
    }
}
