using BangLuong.Services;
using BangLuong.ViewModels;
using Microsoft.AspNetCore.Mvc;
using static BangLuong.ViewModels.ChamCongViewModels;

namespace BangLuong.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChamCongApiController : ControllerBase
    {
        private readonly IChamCongService _service;

        public ChamCongApiController(IChamCongService service)
        {
            _service = service;
        }

        // GET: api/ChamCong
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _service.GetAllFilter("", "", "", null, 1000); // Lấy tất cả
            return Ok(list);
        }

        // GET: api/ChamCong/filter
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

        // GET: api/ChamCong/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _service.GetById(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        // POST: api/ChamCong
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ChamCongRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var success = await _service.Create(request);
            if (success==0) return BadRequest("Tạo chấm công thất bại.");

            return CreatedAtAction(nameof(GetById), new { id = request.MaCC }, request);
        }

        // PUT: api/ChamCong/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ChamCongViewModel model)
        {
            if (id != model.MaCC) return BadRequest("Id không hợp lệ.");

            var success = await _service.Update(model);
            if (success==0) return NotFound();

            return Ok(model);
        }

        // DELETE: api/ChamCong/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.Delete(id);
            if (success==0) return NotFound();

            return NoContent();
        }
    }
}
