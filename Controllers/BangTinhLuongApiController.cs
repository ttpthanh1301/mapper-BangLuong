using BangLuong.Services;
using BangLuong.ViewModels;
using Microsoft.AspNetCore.Mvc;
using static BangLuong.ViewModels.BangTinhLuongViewModels;

namespace BangLuong.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class BangTinhLuongApiController : ControllerBase
    {
        private readonly IBangTinhLuongService _service;

        public BangTinhLuongApiController(IBangTinhLuongService service)
        {
            _service = service;
        }

        // GET: api/BangTinhLuong
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

        // GET: api/BangTinhLuong/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        // POST: api/BangTinhLuong
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BangTinhLuongRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _service.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = request.MaBL }, request);
        }

        // PUT: api/BangTinhLuong/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BangTinhLuongViewModel model)
        {
            if (id != model.MaBL) return BadRequest("Id không hợp lệ");

            await _service.UpdateAsync(id, model);
            return Ok(model);
        }

        // DELETE: api/BangTinhLuong/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }

        // POST: api/BangTinhLuong/TinhLuong?kyLuongThang=...&kyLuongNam=...
        [HttpPost("TinhLuong")]
        public async Task<IActionResult> TinhLuongTheoKy([FromQuery] int kyLuongThang, [FromQuery] int kyLuongNam)
        {
            if (kyLuongThang < 1 || kyLuongThang > 12)
                return BadRequest("Tháng phải từ 1 đến 12");

            if (kyLuongNam < 2000 || kyLuongNam > 2100)
                return BadRequest("Năm phải từ 2000 đến 2100");

            try
            {
                var success = await _service.TinhLuongTheoKyAsync(kyLuongThang, kyLuongNam);
                if (success)
                    return Ok(new { message = $"✓ Đã tính lương thành công cho kỳ {kyLuongThang}/{kyLuongNam}" });
                else
                    return StatusCode(500, new { message = "Có lỗi xảy ra khi tính lương. Vui lòng thử lại." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Lỗi: {ex.Message}" });
            }
        }
    }
}
