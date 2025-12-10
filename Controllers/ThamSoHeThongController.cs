using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BangLuong.Services;
using static BangLuong.ViewModels.ThamSoHeThongViewModels;
using Microsoft.AspNetCore.Authorization;
using System; // Thêm để sử dụng Console.WriteLine cho debug/lỗi


namespace BangLuong.Controllers
{
    [Authorize(Roles = "Admin,Manager")] 
    public class ThamSoHeThongController : Controller
    {
        private readonly IThamSoHeThongService _service;

        public ThamSoHeThongController(IThamSoHeThongService service)
        {
            _service = service;
        }

        // ======================= INDEX =======================
        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? pageNumber)
        {
            int pageSize = 10; // Số bản ghi mỗi trang

            var list = await _service.GetAllFilter(sortOrder, currentFilter, searchString, pageNumber, pageSize);

            ViewData["CurrentSort"] = sortOrder;
            ViewData["CurrentFilter"] = searchString;

            return View(list);
        }

        // ======================= DETAILS =======================
        public async Task<IActionResult> Details(string id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        // ======================= CREATE =======================
        public IActionResult Create() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ThamSoHeThongRequest request)
        {
            if (!ModelState.IsValid) 
            {
                return View(request);
            }

            try
            {
                // BƯỚC 1: KIỂM TRA TRÙNG LẶP THAM SỐ
                // Giả định IThamSoHeThongService có phương thức GetByIdAsync(string id)
                var existingItem = await _service.GetByIdAsync(request.MaTS);
                
                if (existingItem != null)
                {
                    // LỖI ĐÃ ĐƯỢC FIX: Đã đổi "MaThamSo" thành "MaTS" để thông báo lỗi hiển thị đúng trên trường MaTS trong View.
                    ModelState.AddModelError("MaTS", $"Mã tham số '{request.MaTS}' đã tồn tại trong hệ thống.");
                    return View(request);
                }

                // BƯỚC 2: TẠO MỚI NẾU KHÔNG TRÙNG LẶP
                await _service.CreateAsync(request);
                TempData["SuccessMessage"] = "Tạo tham số hệ thống thành công!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Xử lý các lỗi khác có thể xảy ra trong quá trình tạo
                Console.WriteLine($"[ERROR] Exception in Create POST: {ex.Message}");
                ModelState.AddModelError("", "Lỗi khi tạo tham số hệ thống: " + ex.Message);
                return View(request);
            }
        }

        // ======================= EDIT =======================
        public async Task<IActionResult> Edit(string id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, ThamSoHeThongViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            // Giả định logic Edit không cần kiểm tra trùng MaTS vì MaTS là ID và không đổi trong Edit
            var result = await _service.UpdateAsync(id, model);
            if (!result) return NotFound();
            
            TempData["SuccessMessage"] = "Cập nhật tham số hệ thống thành công!";
            return RedirectToAction(nameof(Index));
        }

        // ======================= DELETE =======================
        public async Task<IActionResult> Delete(string id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            try
            {
                await _service.DeleteAsync(id);
                TempData["SuccessMessage"] = "Xóa tham số hệ thống thành công!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Exception in Delete: {ex.Message}");
                TempData["ErrorMessage"] = "Không thể xóa tham số hệ thống: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }
    }
}