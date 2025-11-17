using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BangLuong.Services;
using System.Threading.Tasks;
using static BangLuong.ViewModels.ChucVuViewModels;

namespace BangLuong.Controllers
{
    [Authorize] // Yêu cầu đăng nhập cho tất cả action
    public class ChucVuController : Controller
    {
        private readonly IChucVuService _chucVuService;

        public ChucVuController(IChucVuService chucVuService)
        {
            _chucVuService = chucVuService;
        }

        // ======================= INDEX =======================
        // Chỉ Admin, Manager mới được xem danh sách
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? pageNumber)
        {
            int pageSize = 10; // Số bản ghi mỗi trang

            var list = await _chucVuService.GetAllFilter(sortOrder, currentFilter, searchString, pageNumber, pageSize);

            ViewData["CurrentSort"] = sortOrder;
            ViewData["CurrentFilter"] = searchString;

            return View(list);
        }

        // ======================= DETAILS =======================
        // Tất cả user đăng nhập đều xem được chi tiết
        public async Task<IActionResult> Details(string id)
        {
            var chucVu = await _chucVuService.GetByIdAsync(id);
            if (chucVu == null) return NotFound("Không tìm thấy chức vụ");
            return View(chucVu);
        }

        // ======================= CREATE GET =======================
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult Create() => View();

        // ======================= CREATE POST =======================
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Create(ChucVuRequest request)
        {
            if (!ModelState.IsValid) return View(request);

            await _chucVuService.CreateAsync(request);
            TempData["SuccessMessage"] = "Tạo chức vụ thành công!";
            return RedirectToAction(nameof(Index));
        }

        // ======================= EDIT GET =======================
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Edit(string id)
        {
            var chucVu = await _chucVuService.GetByIdAsync(id);
            if (chucVu == null) return NotFound("Không tìm thấy chức vụ");
            return View(chucVu);
        }

        // ======================= EDIT POST =======================
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Edit(string id, ChucVuViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            var result = await _chucVuService.UpdateAsync(id, viewModel);
            if (!result) return NotFound("Cập nhật thất bại");

            TempData["SuccessMessage"] = "Cập nhật chức vụ thành công!";
            return RedirectToAction(nameof(Index));
        }

        // ======================= DELETE GET =======================
        [Authorize(Roles = "Admin")] // Chỉ Admin mới được xóa
        public async Task<IActionResult> Delete(string id)
        {
            var chucVu = await _chucVuService.GetByIdAsync(id);
            if (chucVu == null) return NotFound("Không tìm thấy chức vụ");
            return View(chucVu);
        }

        // ======================= DELETE POST =======================
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _chucVuService.DeleteAsync(id);
            TempData["SuccessMessage"] = "Xóa chức vụ thành công!";
            return RedirectToAction(nameof(Index));
        }
    }
}
