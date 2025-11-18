using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BangLuong.Services;
using static BangLuong.ViewModels.HopDongViewModels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace BangLuong.Controllers
{
    [Authorize] // Tất cả phải đăng nhập
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
            string? currentUserMaNV = User.Identity?.Name; // UserName chính là MaNV

            // Nhân viên bình thường mà không có UserName -> không cho xem
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
            ViewData["MaNV"] = new SelectList(await _nhanVienService.GetAll(), "MaNV", "MaNV");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Create(HopDongRequest request)
        {
            if (!ModelState.IsValid)
            {
                ViewData["MaNV"] = new SelectList(await _nhanVienService.GetAll(), "MaNV", "MaNV", request.MaNV);
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

            ViewData["MaNV"] = new SelectList(await _nhanVienService.GetAll(), "MaNV", "MaNV", item.MaNV);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Edit(HopDongViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["MaNV"] = new SelectList(await _nhanVienService.GetAll(), "MaNV", "MaNV", model.MaNV);
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
            await _hopDongService.Delete(id);
            TempData["SuccessMessage"] = "Xóa hợp đồng thành công!";
            return RedirectToAction(nameof(Index));
        }

        // ======================= DETAILS =======================
        public async Task<IActionResult> Details(int id)
        {
            var item = await _hopDongService.GetById(id);
            if (item == null) return NotFound();

            string? currentUserMaNV = User.Identity?.Name;
            if (!User.IsInRole("Admin") && !User.IsInRole("Manager") && item.MaNV != currentUserMaNV)
                return Forbid(); // Nhân viên chỉ xem hợp đồng của mình

            return View(item);
        }
    }
}
