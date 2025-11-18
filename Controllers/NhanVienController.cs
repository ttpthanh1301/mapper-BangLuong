using BangLuong.Services;
using BangLuong.ViewModels;
using BangLuong.Data; // ✅ Thêm namespace
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering; // ✅ Thêm namespace
using Microsoft.EntityFrameworkCore; // ✅ Thêm namespace

namespace BangLuong.Controllers
{
    [Authorize] // ✅ Tất cả user phải đăng nhập
    public class NhanVienController : Controller
    {
        private readonly INhanVienService _nhanVienService;
        private readonly BangLuongDbContext _context; // ✅ Thêm DbContext

        public NhanVienController(INhanVienService nhanVienService, BangLuongDbContext context)
        {
            _nhanVienService = nhanVienService;
            _context = context;
        }

        // ======================= INDEX WITH FILTER, SORT, PAGING =======================
        [Authorize] // ✅ Tất cả user đăng nhập đều xem được
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            const int pageSize = 10;
            string? currentUserName = User.Identity?.Name ?? string.Empty;

            // ✅ Lấy danh sách role của người dùng
            var userRoles = User.Claims
                .Where(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")
                .Select(c => c.Value)
                .ToList();

            bool isEmployeeOnly = userRoles.Contains("Employee") && 
                                  !userRoles.Contains("Admin") && 
                                  !userRoles.Contains("Manager");

            // ✅ Logic phân trang
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            // ✅ Nếu là Employee, chỉ xem thông tin của chính mình
            if (isEmployeeOnly)
            {
                searchString = currentUserName; // Mã NV = Username
            }

            var employees = await _nhanVienService.GetAllFilter(
                sortOrder ?? string.Empty, 
                currentFilter ?? string.Empty, 
                searchString ?? string.Empty, 
                pageNumber, 
                pageSize
            );

            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["GenderSortParm"] = sortOrder == "gender" ? "gender_desc" : "gender";
            ViewData["DateSortParm"] = sortOrder == "date" ? "date_desc" : "date";
            ViewData["CurrentFilter"] = searchString;
            ViewBag.IsEmployeeOnly = isEmployeeOnly; // ✅ Gửi thông tin role cho View

            return View(employees);
        }

        // ======================= DETAILS =======================
        [Authorize] // ✅ Tất cả user có thể xem chi tiết
        public async Task<IActionResult> Details(string id)
        {
            if (String.IsNullOrEmpty(id))
                return BadRequest("Mã nhân viên không được để trống.");

            try
            {
                var employee = await _nhanVienService.GetById(id);
                var currentUserName = User.Identity?.Name ?? string.Empty;

                // ✅ Employee chỉ được xem thông tin của chính mình
                if (User.IsInRole("Employee") && 
                    !User.IsInRole("Admin") && 
                    !User.IsInRole("Manager") && 
                    employee.MaNV != currentUserName)
                {
                    return Forbid();
                }

                return View(employee);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // ======================= CREATE =======================
        [Authorize(Roles = "Admin")] // ✅ Chỉ Admin mới tạo mới
        public async Task<IActionResult> Create()
        {
            // ✅ Load dropdown Phòng ban và Chức vụ
            await LoadDropdownData();
            return View(new NhanVienViewModels.NhanVienRequest());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(NhanVienViewModels.NhanVienRequest request)
        {
            if (!ModelState.IsValid)
            {
                // ✅ Reload dropdown nếu validation fail
                await LoadDropdownData();
                return View(request);
            }

            try
            {
                var result = await _nhanVienService.Create(request);
                if (result <= 0)
                {
                    ModelState.AddModelError("", "Thêm nhân viên thất bại!");
                    await LoadDropdownData();
                    return View(request);
                }

                TempData["SuccessMessage"] = "Thêm nhân viên thành công!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                await LoadDropdownData();
                return View(request);
            }
        }

        // ======================= EDIT =======================
        [Authorize] // ✅ Employee có thể sửa thông tin của mình
        public async Task<IActionResult> Edit(string id)
        {
            if (String.IsNullOrEmpty(id))
                return BadRequest("Mã nhân viên không được để trống.");

            try
            {
                var employee = await _nhanVienService.GetById(id);
                var currentUserName = User.Identity?.Name ?? string.Empty;

                // ✅ Employee chỉ được sửa thông tin của chính mình
                if (User.IsInRole("Employee") && 
                    !User.IsInRole("Admin") && 
                    !User.IsInRole("Manager") && 
                    employee.MaNV != currentUserName)
                {
                    return Forbid();
                }

                // ✅ Load dropdown cho Edit
                await LoadDropdownData(employee.MaPB, employee.MaCV);
                return View(employee);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(NhanVienViewModels.NhanVienViewModel request)
        {
            if (!ModelState.IsValid)
            {
                // ✅ Reload dropdown nếu validation fail
                await LoadDropdownData(request.MaPB, request.MaCV);
                return View(request);
            }

            try
            {
                var currentUserName = User.Identity?.Name ?? string.Empty;

                // ✅ Employee chỉ được cập nhật thông tin của chính mình
                if (User.IsInRole("Employee") && 
                    !User.IsInRole("Admin") && 
                    !User.IsInRole("Manager") && 
                    request.MaNV != currentUserName)
                {
                    return Forbid();
                }

                var result = await _nhanVienService.Update(request);
                if (result <= 0)
                {
                    ModelState.AddModelError("", "Cập nhật thất bại!");
                    await LoadDropdownData(request.MaPB, request.MaCV);
                    return View(request);
                }

                TempData["SuccessMessage"] = "Cập nhật thông tin nhân viên thành công!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                await LoadDropdownData(request.MaPB, request.MaCV);
                return View(request);
            }
        }

        // ======================= DELETE =======================
        [Authorize(Roles = "Admin")] // ✅ Chỉ Admin mới xóa được
        public async Task<IActionResult> Delete(string id)
        {
            if (String.IsNullOrEmpty(id))
                return BadRequest("Mã nhân viên không được để trống.");

            try
            {
                var employee = await _nhanVienService.GetById(id);
                return View(employee);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            try
            {
                var result = await _nhanVienService.Delete(id);
                TempData["SuccessMessage"] = "Xóa nhân viên thành công!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }

        // ======================= EXPORT TO EXCEL =======================
        [Authorize(Roles = "Admin,Manager")] // ✅ Admin và Manager export
        public async Task<IActionResult> ExportToExcel()
        {
            var fileContent = await _nhanVienService.ExportToExcel();
            return File(fileContent, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "DanhSachNhanVien.xlsx");
        }

        // ======================= IMPORT FROM EXCEL =======================
        [HttpPost]
        [Authorize(Roles = "Admin")] // ✅ Chỉ Admin import
        public async Task<IActionResult> ImportFromExcel(IFormFile file)
        {
            var (success, message, importedCount) = await _nhanVienService.ImportFromExcel(file);
            TempData[success ? "SuccessMessage" : "ErrorMessage"] = message;
            return RedirectToAction(nameof(Index));
        }

        // ✅ HELPER METHOD - Load dropdown data
        private async Task LoadDropdownData(string? selectedMaPB = null, string? selectedMaCV = null)
        {
            var phongBans = await _context.PhongBan.ToListAsync();
            var chucVus = await _context.ChucVu.ToListAsync();
            
            ViewBag.MaPB = new SelectList(phongBans, "MaPB", "TenPB", selectedMaPB);
            ViewBag.MaCV = new SelectList(chucVus, "MaCV", "TenCV", selectedMaCV);
        }
    }
}