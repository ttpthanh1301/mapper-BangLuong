using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BangLuong.Services;
using static BangLuong.ViewModels.HopDongViewModels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace BangLuong.Controllers
{
    [Authorize]
    public class HopDongController : Controller
    {
        private readonly IHopDongService _hopDongService;
        private readonly INhanVienService _nhanVienService;

        public HopDongController(IHopDongService hopDongService, INhanVienService nhanVienService)
        {
            _hopDongService = hopDongService;
            _nhanVienService = nhanVienService;
        }

        // ======================= INDEX =======================
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            int pageSize = 10;
            bool isAdminOrManager = User.IsInRole("Admin") || User.IsInRole("Manager");
            string? currentUserMaNV = User.Identity?.Name;

            if (!isAdminOrManager && string.IsNullOrEmpty(currentUserMaNV))
                return Forbid();

            var list = isAdminOrManager
                ? await _hopDongService.GetAllFilter(sortOrder, currentFilter, searchString, pageNumber, pageSize)
                : await _hopDongService.GetAllFilter(sortOrder, currentFilter, searchString, pageNumber, pageSize, currentUserMaNV);

            ViewData["CurrentSort"] = sortOrder;
            ViewData["CurrentFilter"] = searchString;
            ViewBag.IsAdminOrManager = isAdminOrManager;

            return View(list);
        }

        // ======================= CREATE =======================
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Create()
        {
            var nhanViens = await _nhanVienService.GetAll();
            ViewData["MaNV"] = new SelectList(nhanViens, "MaNV", "HoTen");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Create(HopDongRequest request)
        {
            if (!ModelState.IsValid)
            {
                var nhanViens = await _nhanVienService.GetAll();
                ViewData["MaNV"] = new SelectList(nhanViens, "MaNV", "HoTen", request.MaNV);
                return View(request);
            }

            await _hopDongService.Create(request);
            TempData["SuccessMessage"] = "Tạo hợp đồng thành công!";
            return RedirectToAction(nameof(Index));
        }

        // ======================= EDIT =======================
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _hopDongService.GetById(id);
            if (item == null) return NotFound();

            // Dropdown Nhân viên
            var nhanViens = await _nhanVienService.GetAll();
            ViewData["MaNV"] = new SelectList(nhanViens, "MaNV", "HoTen", item.MaNV);

            // Dropdown Loại hợp đồng
            ViewBag.ListLoaiHD = new SelectList(new[]
            {
        "Thử việc",
        "Có thời hạn",
        "Chính thức"
    }, item.LoaiHD);

            // Dropdown Trạng thái
            ViewBag.ListTrangThai = new SelectList(new[]
            {
        "Đang hiệu lực",
        "Hết hiệu lực",
        "Hủy"
    }, item.TrangThai);

            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Edit(HopDongViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var nhanViens = await _nhanVienService.GetAll();
                ViewData["MaNV"] = new SelectList(nhanViens, "MaNV", "HoTen", model.MaNV);

                ViewBag.ListLoaiHD = new SelectList(new[]
                {
            "Thử việc",
            "Có thời hạn",
            "Chính thức"
        }, model.LoaiHD);

                ViewBag.ListTrangThai = new SelectList(new[]
                {
            "Đang hiệu lực",
            "Hết hiệu lực",
            "Hủy"
        }, model.TrangThai);

                return View(model);
            }

            await _hopDongService.Update(model);
            TempData["SuccessMessage"] = "Cập nhật hợp đồng thành công!";
            return RedirectToAction(nameof(Index));
        }

        // ======================= DELETE =======================
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _hopDongService.GetById(id);
            if (item == null) return NotFound();
            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _hopDongService.Delete(id);
                TempData["SuccessMessage"] = "Xóa hợp đồng thành công!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }

        // ======================= DETAILS =======================
        public async Task<IActionResult> Details(int id)
        {
            var item = await _hopDongService.GetById(id);
            if (item == null) return NotFound();

            string? currentUserMaNV = User.Identity?.Name;
            if (!User.IsInRole("Admin") && !User.IsInRole("Manager") && item.MaNV != currentUserMaNV)
                return Forbid();

            return View(item);
        }
    }
}
