using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BangLuong.Services;
using static BangLuong.ViewModels.PhongBanViewModels;

namespace BangLuong.Controllers
{
    [Authorize] // Bắt buộc đăng nhập cho tất cả action
    public class PhongBanController : Controller
    {
        private readonly IPhongBanService _phongBanService;
        private readonly UserManager<NguoiDung> _userManager;

        public PhongBanController(
            IPhongBanService phongBanService,
            UserManager<NguoiDung> userManager)
        {
            _phongBanService = phongBanService;
            _userManager = userManager;
        }

        // ======================= INDEX =======================
        // Chỉ Admin, Manager mới xem danh sách
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? pageNumber)
        {
            try
            {
                int pageSize = 10;
                var list = await _phongBanService.GetAllFilter(sortOrder, currentFilter, searchString, pageNumber, pageSize);

                ViewData["CurrentSort"] = sortOrder;
                ViewData["CurrentFilter"] = searchString;

                return View(list);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Lỗi khi tải danh sách: {ex.Message}";
                return View(new List<PhongBanViewModel>());
            }
        }

        // ======================= DETAILS =======================
        // Tất cả user đăng nhập đều xem được chi tiết
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
                return BadRequest("Mã phòng ban không được để trống");

            try
            {
                var phongBan = await _phongBanService.GetById(id);
                if (phongBan == null)
                    return NotFound("Không tìm thấy phòng ban");

                return View(phongBan);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // ======================= CREATE GET =======================
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult Create()
        {
            return View();
        }

        // ======================= CREATE POST =======================
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Create(PhongBanRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            try
            {
                await _phongBanService.Create(request);
                TempData["SuccessMessage"] = "Tạo phòng ban thành công!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(request);
            }
        }

        // ======================= EDIT GET =======================
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
                return BadRequest("Mã phòng ban không được để trống");

            try
            {
                var phongBan = await _phongBanService.GetById(id);
                if (phongBan == null)
                    return NotFound("Không tìm thấy phòng ban");

                return View(phongBan);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // ======================= EDIT POST =======================
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Edit(string id, PhongBanViewModel request)
        {
            if (id != request.MaPB)
                return BadRequest("Mã phòng ban không khớp");

            if (!ModelState.IsValid)
                return View(request);

            try
            {
                await _phongBanService.Update(request);
                TempData["SuccessMessage"] = "Cập nhật phòng ban thành công!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(request);
            }
        }

        // ======================= DELETE GET =======================
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
                return BadRequest("Mã phòng ban không được để trống");

            try
            {
                var phongBan = await _phongBanService.GetById(id);
                if (phongBan == null)
                    return NotFound("Không tìm thấy phòng ban");

                return View(phongBan);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // ======================= DELETE POST =======================
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            try
            {
                await _phongBanService.Delete(id);
                TempData["SuccessMessage"] = "Xóa phòng ban thành công!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }
    }
}