using BangLuong.Services;
using BangLuong.ViewModels;
using Microsoft.AspNetCore.Mvc;
using static BangLuong.ViewModels.PhongBanViewModels;

namespace BangLuong.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class PhongBanApiController : ControllerBase
    {
        private readonly IPhongBanService _phongBanService;

        public PhongBanApiController(IPhongBanService phongBanService)
        {
            _phongBanService = phongBanService;
        }

        // GET: api/PhongBan
        [HttpGet]
        public async Task<IActionResult> GetAll(string? sortOrder, string? currentFilter, string? searchString, int? pageNumber, int pageSize = 1000)
        {
            var result = await _phongBanService.GetAllFilter(sortOrder ?? "",
     currentFilter ?? "",
     searchString ?? "",
     pageNumber,
     pageSize);
            return Ok(result);
        }

        // GET: api/PhongBan/filter
        [HttpGet("filter")]
        public async Task<IActionResult> GetAllFilter(
            string? sortOrder,
            string? currentFilter,
            string? searchString,
            int? pageNumber,
            int pageSize = 10)
        {
            var filtered = await _phongBanService.GetAllFilter(
     sortOrder ?? "",
     currentFilter ?? "",
     searchString ?? "",
     pageNumber,
     pageSize
 );
            return Ok(filtered);
        }

        // GET: api/PhongBan/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            if (string.IsNullOrEmpty(id)) return BadRequest("Id không được trống.");

            var phongBan = await _phongBanService.GetById(id);
            if (phongBan == null) return NotFound();

            return Ok(phongBan);
        }

        // POST: api/PhongBan
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] PhongBanRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _phongBanService.Create(request);
            return Ok(result);
        }

        // PUT: api/PhongBan
        [HttpPut]
        public async Task<IActionResult> Update([FromForm] PhongBanViewModel request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _phongBanService.Update(request);
            return Ok(result);
        }

        // DELETE: api/PhongBan/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id)) return BadRequest("Id không được trống.");

            var result = await _phongBanService.Delete(id);
            return Ok(result);
        }
    }
}
