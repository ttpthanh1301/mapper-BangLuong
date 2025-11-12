using BangLuong.Services;
using BangLuong.ViewModels;
using Microsoft.AspNetCore.Mvc;
using static BangLuong.ViewModels.DanhMucKhenThuongViewModels;

namespace BangLuong.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class DanhMucKhenThuongApiController : ControllerBase
    {
        private readonly IDanhMucKhenThuongService _service;

        public DanhMucKhenThuongApiController(IDanhMucKhenThuongService service)
        {
            _service = service;
        }

        // GET: api/DanhMucKhenThuong
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _service.GetAllFilter("", "", "", null, 1000); // Lấy tất cả
            return Ok(list);
        }

        // GET: api/DanhMucKhenThuong/filter
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

        // GET: api/DanhMucKhenThuong/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            if (string.IsNullOrEmpty(id)) return BadRequest("Id không được trống.");

            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();

            return Ok(item);
        }

        // POST: api/DanhMucKhenThuong
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DanhMucKhenThuongRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _service.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = request.MaKT }, request);
        }

        // PUT: api/DanhMucKhenThuong/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] DanhMucKhenThuongViewModel model)
        {
            if (string.IsNullOrEmpty(id) || id != model.MaKT) return BadRequest("Id không hợp lệ.");

            await _service.UpdateAsync(model); // Service nên trả bool

            return Ok(model);
        }

        // DELETE: api/DanhMucKhenThuong/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id)) return BadRequest("Id không được trống.");
                await _service.DeleteAsync(id); 

            return NoContent();
        }
    }
}
