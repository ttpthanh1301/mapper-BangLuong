using BangLuong.Services;
using BangLuong.ViewModels;
using Microsoft.AspNetCore.Mvc;
using static BangLuong.ViewModels.ChiTietPhuCapViewModels;

namespace BangLuong.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChiTietPhuCapApiController : ControllerBase
    {
        private readonly IChiTietPhuCapService _service;

        public ChiTietPhuCapApiController(IChiTietPhuCapService service)
        {
            _service = service;
        }

        // GET: api/ChiTietPhuCap
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _service.GetAllFilter("", "", "", null, 1000); // Lấy tất cả
            return Ok(list);
        }

        // GET: api/ChiTietPhuCap/filter
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

        // GET: api/ChiTietPhuCap/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _service.GetById(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        // POST: api/ChiTietPhuCap
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ChiTietPhuCapRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var success = await _service.Create(request);
            if (success==0) return BadRequest("Tạo chi tiết phụ cấp thất bại.");

            return CreatedAtAction(nameof(GetById), new { id = request.MaCTPC }, request);
        }

        // PUT: api/ChiTietPhuCap/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ChiTietPhuCapViewModel model)
        {
            if (id != model.MaCTPC) return BadRequest("Id không hợp lệ.");

            var success = await _service.Update(model);
            if (success==0) return NotFound();

            return Ok(model);
        }

        // DELETE: api/ChiTietPhuCap/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.Delete(id);
            if (success==0) return NotFound();

            return NoContent();
        }
    }
}
