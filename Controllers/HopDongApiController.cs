using BangLuong.Services;
using BangLuong.ViewModels;
using Microsoft.AspNetCore.Mvc;
using static BangLuong.ViewModels.HopDongViewModels;

namespace BangLuong.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class HopDongApiController : ControllerBase
    {
        private readonly IHopDongService _service;

        public HopDongApiController(IHopDongService service)
        {
            _service = service;
        }

        // GET: api/HopDong
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _service.GetAllFilter("", "", "", null, 1000); // Lấy tất cả
            return Ok(list);
        }

        // GET: api/HopDong/filter
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

        // GET: api/HopDong/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _service.GetById(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        // POST: api/HopDong
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] HopDongRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var success = await _service.Create(request);
            if (success==0) return BadRequest("Tạo hợp đồng thất bại.");

            return CreatedAtAction(nameof(GetById), new { id = request.MaHD }, request);
        }

        // PUT: api/HopDong/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] HopDongViewModel model)
        {
            if (id != model.MaHD) return BadRequest("Id không hợp lệ.");

            var success = await _service.Update(model);
            if (success==0) return NotFound();

            return Ok(model);
        }

        // DELETE: api/HopDong/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.Delete(id);
            if (success==0) return NotFound();

            return NoContent();
        }
    }
}
