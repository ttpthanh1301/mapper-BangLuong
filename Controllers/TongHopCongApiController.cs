using BangLuong.Services;
using BangLuong.ViewModels;
using Microsoft.AspNetCore.Mvc;
using static BangLuong.ViewModels.TongHopCongViewModels;

namespace BangLuong.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class TongHopCongApiController : ControllerBase
    {
        private readonly ITongHopCongService _service;

        public TongHopCongApiController(ITongHopCongService service)
        {
            _service = service;
        }

        // GET: api/TongHopCong
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

        // GET: api/TongHopCong/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        // POST: api/TongHopCong
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TongHopCongRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _service.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = request.MaTHC }, request);
        }

        // PUT: api/TongHopCong/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TongHopCongViewModel model)
        {
            if (id != model.MaTHC) return BadRequest("Id không hợp lệ");

            await _service.UpdateAsync(id, model);
            return Ok(model);
        }

        // DELETE: api/TongHopCong/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }

        // POST: api/TongHopCong/Run?kyLuongThang=...&kyLuongNam=...
        [HttpPost("Run")]
        public async Task<IActionResult> RunTongHopCong([FromQuery] int kyLuongThang, [FromQuery] int kyLuongNam)
        {
            try
            {
                await _service.RunTongHopCongThangAsync(kyLuongThang, kyLuongNam);
                return Ok(new { message = $"✅ Tổng hợp công tháng {kyLuongThang}/{kyLuongNam} hoàn tất!" });
            }
            catch
            {
                return StatusCode(500, new { message = "❌ Lỗi khi chạy tổng hợp công." });
            }
        }
    }
}
