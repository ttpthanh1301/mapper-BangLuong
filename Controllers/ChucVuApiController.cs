using BangLuong.Services;
using BangLuong.ViewModels;
using Microsoft.AspNetCore.Mvc;
using static BangLuong.ViewModels.ChucVuViewModels;

namespace BangLuong.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChucVuApiController : ControllerBase
    {
        private readonly IChucVuService _chucVuService;

        public ChucVuApiController(IChucVuService chucVuService)
        {
            _chucVuService = chucVuService;
        }

        // GET: api/ChucVu
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _chucVuService.GetAllFilter("", "", "", null, 1000); // Lấy tất cả
            return Ok(list);
        }

        // GET: api/ChucVu/filter
        [HttpGet("filter")]
        public async Task<IActionResult> GetAllFilter(
            [FromQuery] string? sortOrder,
            [FromQuery] string? currentFilter,
            [FromQuery] string? searchString,
            [FromQuery] int? pageNumber,
            [FromQuery] int pageSize = 10)
        {
            var filtered = await _chucVuService.GetAllFilter(
                sortOrder ?? "",
                currentFilter ?? "",
                searchString ?? "",
                pageNumber,
                pageSize);

            return Ok(filtered);
        }

        // GET: api/ChucVu/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest("Id không được trống.");

            var chucVu = await _chucVuService.GetByIdAsync(id);
            if (chucVu == null)
                return NotFound();

            return Ok(chucVu);
        }

        // POST: api/ChucVu
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ChucVuRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _chucVuService.CreateAsync(request);
            return Ok(created);
        }

        // PUT: api/ChucVu
        [HttpPut]
        public async Task<IActionResult> Update([FromForm] ChucVuViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _chucVuService.UpdateAsync(viewModel.MaCV, viewModel);
            if (!updated)
                return NotFound();

            return Ok(updated);
        }

        // DELETE: api/ChucVu/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest("Id không được trống.");

            var deleted = await _chucVuService.DeleteAsync(id);
            return Ok(deleted);
        }
    }
}
