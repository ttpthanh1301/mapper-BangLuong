using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BangLuong.Services;
using static BangLuong.ViewModels.HopDongViewModels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace BangLuong.Controllers
{
    [Authorize(Roles = "Admin,Manager")] 
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
                var list = await _hopDongService.GetAllFilter(sortOrder, currentFilter, searchString, pageNumber, pageSize);

                ViewData["CurrentSort"] = sortOrder;
                ViewData["CurrentFilter"] = searchString;

                return View(list);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Lỗi khi tải danh sách: {ex.Message}";
                return View(new List<HopDongViewModel>());
            }
        }

        // ======================= DETAILS =======================
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var item = await _hopDongService.GetById(id);
                if (item == null)
                    return NotFound("Không tìm thấy hợp đồng");

                return View(item);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // ======================= CREATE GET =======================
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Create()
        {
            ViewData["MaNV"] = new SelectList(await _nhanVienService.GetAll(), "MaNV", "MaNV");
            return View();
        }

        // ======================= CREATE POST =======================
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

            try
            {
                await _hopDongService.Create(request);
                TempData["SuccessMessage"] = "Tạo hợp đồng thành công!";
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
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var item = await _hopDongService.GetById(id);
                if (item == null)
                    return NotFound("Không tìm thấy hợp đồng");

                ViewData["MaNV"] = new SelectList(await _nhanVienService.GetAll(), "MaNV", "MaNV", item.MaNV);
                return View(item);
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
        public async Task<IActionResult> Edit(HopDongViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["MaNV"] = new SelectList(await _nhanVienService.GetAll(), "MaNV", "MaNV", model.MaNV);
                return View(model);
            }

            try
            {
                await _hopDongService.Update(model);
                TempData["SuccessMessage"] = "Cập nhật hợp đồng thành công!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }

        // ======================= DELETE GET =======================
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var item = await _hopDongService.GetById(id);
                if (item == null)
                    return NotFound("Không tìm thấy hợp đồng");

                return View(item);
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
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _hopDongService.Delete(id);
                TempData["SuccessMessage"] = "Xóa hợp đồng thành công!";
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
