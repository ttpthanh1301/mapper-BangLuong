using BangLuong.Services;
using BangLuong.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BangLuong.Controllers
{
    public class NguoiDungController : Controller
    {
        private readonly INguoiDungService _nguoiDungService;

        public NguoiDungController(INguoiDungService nguoiDungService)
        {
            _nguoiDungService = nguoiDungService;
        }

        // ======================= INDEX =======================
        public async Task<IActionResult> Index()
        {
            var users = await _nguoiDungService.GetAll();
            return View(users);
        }

        // ======================= REGISTER =======================
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(NguoiDungViewModels.NguoiDungRequest request)
        {
            if (!ModelState.IsValid) return View(request);

            var result = await _nguoiDungService.Register(request);
            if (!result)
            {
                ModelState.AddModelError("", "Đăng ký thất bại");
                return View(request);
            }

            return RedirectToAction(nameof(Index));
        }

        // ======================= LOGIN =======================
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(NguoiDungViewModels.LoginRequest request)
        {
            if (!ModelState.IsValid) return View(request);

            try
            {
                var token = await _nguoiDungService.Authenticate(request);
                // TODO: lưu token vào session hoặc cookie nếu cần
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(request);
            }
        }

        // ======================= EDIT =======================
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id)) return BadRequest();

            var user = await _nguoiDungService.GetById(id);
            if (user == null) return NotFound();

            var editModel = new NguoiDungViewModels.NguoiDungRequest
            {
                MaNV = user.MaNV,
                PhanQuyen = user.PhanQuyen,
                TrangThai = user.TrangThai
            };
            return View(editModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(NguoiDungViewModels.NguoiDungRequest request)
        {
            if (!ModelState.IsValid) return View(request);

            var result = await _nguoiDungService.Update(request);
            if (!result)
            {
                ModelState.AddModelError("", "Cập nhật thất bại");
                return View(request);
            }

            return RedirectToAction(nameof(Index));
        }

        // ======================= DELETE =======================
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id)) return BadRequest();

            var user = await _nguoiDungService.GetById(id);
            if (user == null) return NotFound();

            return View(user); // view confirm xóa
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var result = await _nguoiDungService.Delete(id);
            if (!result)
            {
                ModelState.AddModelError("", "Xóa thất bại");
                var user = await _nguoiDungService.GetById(id);
                return View("Delete", user);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
