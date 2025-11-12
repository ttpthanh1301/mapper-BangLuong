using BangLuong.Services;
using BangLuong.ViewModels;
using Microsoft.AspNetCore.Mvc;
using static BangLuong.ViewModels.ChiTietKhenThuongViewModels;

namespace BangLuong.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChiTietKhenThuongApiController : ControllerBase
    {
        private readonly IChiTietKhenThuongService _service;

        public ChiTietKhenThuongApiController(IChiTietKhenThuongService service)
        {
            _service = service;
        }

        // GET: api/ChiTietKhenThuong
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _service.GetAllFilter("", "", "", null, 1000); // Lấy tất cả
            return Ok(list);
        }

        // GET: api/ChiTietKhenThuong/filter
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

        // GET: api/ChiTietKhenThuong/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        // POST: api/ChiTietKhenThuong
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ChiTietKhenThuongRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var success = await _service.CreateAsync(request);
            if (!success) return BadRequest("Tạo chi tiết khen thưởng thất bại.");

            return CreatedAtAction(nameof(GetById), new { id = request.MaCTKT }, request);
        }

        // PUT: api/ChiTietKhenThuong/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ChiTietKhenThuongViewModel model)
        {
            if (id != model.MaCTKT) return BadRequest("Id không hợp lệ.");

            var success = await _service.UpdateAsync(id, model);
            if (!success) return NotFound();

            return Ok(model);
        }

        // DELETE: api/ChiTietKhenThuong/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success) return NotFound();

            return NoContent();
        }
    }
}
