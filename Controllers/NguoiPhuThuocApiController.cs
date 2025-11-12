using BangLuong.Services;
using BangLuong.ViewModels;
using Microsoft.AspNetCore.Mvc;
using static BangLuong.ViewModels.NguoiPhuThuocViewModels;

namespace BangLuong.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class NguoiPhuThuocApiController : ControllerBase
    {
        private readonly INguoiPhuThuocService _service;

        public NguoiPhuThuocApiController(INguoiPhuThuocService service)
        {
            _service = service;
        }

        // GET: api/NguoiPhuThuoc
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _service.GetAllFilter("", "", "", null, 1000); // Lấy tất cả
            return Ok(list);
        }

        // GET: api/NguoiPhuThuoc/filter
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

        // GET: api/NguoiPhuThuoc/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        // POST: api/NguoiPhuThuoc
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] NguoiPhuThuocRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var created = await _service.CreateAsync(request);
            return Ok(created);
        }

        // PUT: api/NguoiPhuThuoc/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] NguoiPhuThuocViewModel viewModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updated = await _service.UpdateAsync(id, viewModel);
            if (!updated) return NotFound();

            return Ok(updated);
        }

        // DELETE: api/NguoiPhuThuoc/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            return Ok(deleted);
        }

        // GET: api/NguoiPhuThuoc/nhanvienids
        [HttpGet("nhanvienids")]
        public async Task<IActionResult> GetAllNhanVienIds()
        {
            var maNVList = await _service.GetAllNhanVienIdsAsync();
            return Ok(maNVList);
        }
    }
}
