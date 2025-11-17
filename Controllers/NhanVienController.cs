using BangLuong.Services;
using BangLuong.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BangLuong.Controllers

{
    [Authorize(Roles = "Admin,Manager")]
    public class NhanVienController : Controller
    {
        private readonly INhanVienService _nhanVienService;

        public NhanVienController(INhanVienService nhanVienService)
        {
            _nhanVienService = nhanVienService;
        }

        // ======================= INDEX WITH FILTER, SORT, PAGING =======================
        [Authorize]
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            const int pageSize = 10;

            var employees = await _nhanVienService.GetAllFilter(sortOrder, currentFilter ?? searchString, searchString, pageNumber, pageSize);

            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["GenderSortParm"] = sortOrder == "gender" ? "gender_desc" : "gender";
            ViewData["DateSortParm"] = sortOrder == "date" ? "date_desc" : "date";
            ViewData["CurrentFilter"] = searchString;

            return View(employees);
        }

        // ======================= CREATE =======================
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View(new NhanVienViewModels.NhanVienRequest());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(NhanVienViewModels.NhanVienRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            try
            {
                var result = await _nhanVienService.Create(request);
                if (result <= 0)
                {
                    ModelState.AddModelError("", "Thêm nhân viên thất bại!");
                    return View(request);
                }

                TempData["SuccessMessage"] = "Thêm nhân viên thành công!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(request);
            }
        }

        // ======================= EDIT =======================
        [Authorize]
        public async Task<IActionResult> Edit(string id)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(NhanVienViewModels.NhanVienViewModel request)
        {
            if (!ModelState.IsValid)
                return View(request);

            try
            {
                var result = await _nhanVienService.Update(request);
                if (result <= 0)
                {
                    ModelState.AddModelError("", "Cập nhật thất bại!");
                    return View(request);
                }

                TempData["SuccessMessage"] = "Cập nhật thông tin nhân viên thành công!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(request);
            }
        }

        // ======================= DELETE =======================
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ExportToExcel()
        {
            var fileContent = await _nhanVienService.ExportToExcel();
            return File(fileContent, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "DanhSachNhanVien.xlsx");
        }

        // ======================= IMPORT FROM EXCEL =======================
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ImportFromExcel(IFormFile file)
        {
            var (success, message, importedCount) = await _nhanVienService.ImportFromExcel(file);
            TempData[success ? "SuccessMessage" : "ErrorMessage"] = message;
            return RedirectToAction(nameof(Index));
        }
    }
}
