using BangLuong.Services;
using BangLuong.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BangLuong.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NguoiDungApiController : ControllerBase
    {
        private readonly INguoiDungService _nguoiDungService;

        public NguoiDungApiController(INguoiDungService nguoiDungService)
        {
            _nguoiDungService = nguoiDungService;
        }

        // POST: api/NguoiDungApi/authenticate
        [HttpPost("authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] NguoiDungViewModels.LoginRequest request)
        {
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);

            try
            {
                var token = await _nguoiDungService.Authenticate(request);
                return Ok(new { success = true, token });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, error = ex.Message });
            }
        }

        // POST: api/NguoiDungApi/register
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] NguoiDungViewModels.NguoiDungRequest request)
        {
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);

            var result = await _nguoiDungService.Register(request);
            return result 
                ? Ok(new { success = true, message = "Đăng ký thành công" })
                : BadRequest(new { success = false, message = "Đăng ký thất bại" });
        }

        // GET: api/NguoiDungApi
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _nguoiDungService.GetAll();
            return Ok(new { success = true, data = users });
        }

        // GET: api/NguoiDungApi/{maNV}
        [HttpGet("{maNV}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById(string maNV)
        {
            var user = await _nguoiDungService.GetById(maNV);
            if (user == null)
                return NotFound(new { success = false, message = "Người dùng không tồn tại" });

            return Ok(new { success = true, data = user });
        }

        // PUT: api/NguoiDungApi
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromBody] NguoiDungViewModels.NguoiDungRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _nguoiDungService.Update(request);
            return result 
                ? Ok(new { success = true, message = "Cập nhật thành công" })
                : BadRequest(new { success = false, message = "Cập nhật thất bại" });
        }

        // DELETE: api/NguoiDungApi/{maNV}
        [HttpDelete("{maNV}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string maNV)
        {
            var result = await _nguoiDungService.Delete(maNV);
            return result
                ? Ok(new { success = true, message = "Xóa thành công" })
                : NotFound(new { success = false, message = "Người dùng không tồn tại" });
        }
    }
}
