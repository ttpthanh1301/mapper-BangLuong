using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BangLuong.Services;
using static BangLuong.ViewModels.ChamCongViewModels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace BangLuong.Controllers
{
    [Authorize] 
    public class ChamCongController : Controller
    {
        private readonly IChamCongService _chamCongService;
        private readonly INhanVienService _nhanVienService;

        public ChamCongController(IChamCongService chamCongService, INhanVienService nhanVienService)
        {
            _chamCongService = chamCongService;
            _nhanVienService = nhanVienService;
        }

        // ======================= INDEX =======================
        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? pageNumber)
        {
            int pageSize = 10;
            string? currentUserName = User.Identity?.Name ?? string.Empty;

            // Lấy danh sách role của người dùng hiện tại
            var userRoles = User.Claims
                .Where(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")
                .Select(c => c.Value)
                .ToList();

            bool isEmployeeOnly = userRoles.Contains("Employee") && 
                                  !userRoles.Contains("Admin") && 
                                  !userRoles.Contains("Manager");

            // ✅ Logic phân trang đúng
            if (searchString != null)
            {
                pageNumber = 1; // Reset về trang 1 khi tìm kiếm mới
            }
            else
            {
                searchString = currentFilter; // Giữ lại bộ lọc cũ khi chuyển trang
            }

            // Nếu là nhân viên thường, chỉ xem của chính mình
            if (isEmployeeOnly)
            {
                searchString = currentUserName; // Mã NV chính là userName
            }

            // ✅ TRUYỀN ĐÚNG THỨ TỰ THAM SỐ
            var list = await _chamCongService.GetAllFilter(
                sortOrder ?? string.Empty,        // sortOrder
                currentFilter ?? string.Empty,     // currentFilter
                searchString ?? string.Empty,      // searchString
                pageNumber,                        // pageNumber
                pageSize                          // pageSize
            );

            // ✅ Truyền đúng thông tin cho View
            ViewData["CurrentSort"] = sortOrder;
            ViewData["CurrentFilter"] = searchString; // ✅ Quan trọng: giữ filter hiện tại
            ViewBag.IsEmployeeOnly = isEmployeeOnly;

            return View(list);
        }

        // ======================= CREATE =======================
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Create()
        {
            ViewData["MaNV"] = new SelectList(await _nhanVienService.GetAll(), "MaNV", "MaNV");
            return View();
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ChamCongRequest request)
        {
            if (!ModelState.IsValid)
            {
                ViewData["MaNV"] = new SelectList(await _nhanVienService.GetAll(), "MaNV", "MaNV", request.MaNV);
                return View(request);
            }

            await _chamCongService.Create(request);
            return RedirectToAction(nameof(Index));
        }

        // ======================= DETAILS =======================
        public async Task<IActionResult> Details(int id)
        {
            var item = await _chamCongService.GetById(id);
            if (item == null) return NotFound();

            var currentUserName = User.Identity?.Name ?? string.Empty;

            // Kiểm tra quyền nếu là nhân viên chỉ được xem của chính mình
            if (User.IsInRole("Employee") && item.MaNV != currentUserName)
                return Forbid();

            return View(item);
        }

        // ======================= EDIT =======================
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _chamCongService.GetById(id);
            if (item == null) return NotFound();

            ViewData["MaNV"] = new SelectList(await _nhanVienService.GetAll(), "MaNV", "MaNV", item.MaNV);
            return View(item);
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ChamCongViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["MaNV"] = new SelectList(await _nhanVienService.GetAll(), "MaNV", "MaNV", model.MaNV);
                return View(model);
            }

            await _chamCongService.Update(model);
            return RedirectToAction(nameof(Index));
        }

        // ======================= DELETE =======================
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _chamCongService.GetById(id);
            if (item == null) return NotFound();
            return View(item);
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _chamCongService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}