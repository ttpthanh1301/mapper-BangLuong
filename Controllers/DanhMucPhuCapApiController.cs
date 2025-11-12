using BangLuong.Services;
using BangLuong.ViewModels;
using Microsoft.AspNetCore.Mvc;
using static BangLuong.ViewModels.DanhMucPhuCapViewModels;

namespace BangLuong.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class DanhMucPhuCapApiController : ControllerBase
    {
        private readonly IDanhMucPhuCapService _service;

        public DanhMucPhuCapApiController(IDanhMucPhuCapService service)
        {
            _service = service;
        }

        // GET: api/DanhMucPhuCap
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _service.GetAllFilter("", "", "", null, 1000); // Lấy tất cả
            return Ok(list);
        }

        // GET: api/DanhMucPhuCap/filter
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

        // GET: api/DanhMucPhuCap/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            if (string.IsNullOrEmpty(id)) return BadRequest("Id không được trống.");

            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();

            return Ok(item);
        }

        // POST: api/DanhMucPhuCap
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] DanhMucPhuCapRequest request)
        {
          if (!ModelState.IsValid) return BadRequest(ModelState);

            await _service.CreateAsync(request);
            return Ok(new { message = "update thành công" });
        }

        // PUT: api/DanhMucPhuCap/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromForm] DanhMucPhuCapViewModel model)
        {
            if (string.IsNullOrEmpty(id) || id != model.MaPC) return BadRequest("Id không hợp lệ.");

            await _service.UpdateAsync(model);
               return Ok(new { message = "update thành công" });
           
        }

        // DELETE: api/DanhMucPhuCap/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id)) return BadRequest("Id không được trống.");

            await _service.DeleteAsync(id);
            return Ok();
        }
    }
}
