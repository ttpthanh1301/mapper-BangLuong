using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BangLuong.Services;
using static BangLuong.ViewModels.BangTinhLuongViewModels;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace BangLuong.Controllers
{
    [Authorize]
    public class BangTinhLuongController : Controller
    {
        private readonly IBangTinhLuongService _bangTinhLuongService;
        private readonly INhanVienService _nhanVienService;

        public BangTinhLuongController(IBangTinhLuongService bangTinhLuongService, INhanVienService nhanVienService)
        {
            _bangTinhLuongService = bangTinhLuongService;
            _nhanVienService = nhanVienService;
        }

        // ======================= INDEX =======================
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            int pageSize = 10;
            var list = await _bangTinhLuongService.GetAllFilter(sortOrder, currentFilter, searchString, pageNumber, pageSize);

            ViewData["CurrentSort"] = sortOrder;
            ViewData["CurrentFilter"] = searchString;

            return View(list);
        }

        // ===================== TÍNH LƯƠNG THEO KỲ (GET) =====================
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult TinhLuongTheoKy()
        {
            ViewBag.CurrentMonth = DateTime.Now.Month;
            ViewBag.CurrentYear = DateTime.Now.Year;
            return View();
        }

        // ===================== TÍNH LƯƠNG THEO KỲ (POST) =====================
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> TinhLuongTheoKy(int kyLuongThang, int kyLuongNam)
        {
            if (kyLuongThang < 1 || kyLuongThang > 12)
            {
                ModelState.AddModelError("kyLuongThang", "Tháng phải từ 1 đến 12");
                return View();
            }

            if (kyLuongNam < 2000 || kyLuongNam > 2100)
            {
                ModelState.AddModelError("kyLuongNam", "Năm phải từ 2000 đến 2100");
                return View();
            }

            var currentDate = DateTime.Now;
            var kyLuongDate = new DateTime(kyLuongNam, kyLuongThang, 1);
            if (kyLuongDate > currentDate.AddMonths(1))
            {
                ModelState.AddModelError("", $"Không thể tính lương cho kỳ tương lai (tháng {kyLuongThang}/{kyLuongNam})");
                return View();
            }

            try
            {
                var result = await _bangTinhLuongService.TinhLuongTheoKyAsync(kyLuongThang, kyLuongNam);

                if (result)
                {
                    TempData["SuccessMessage"] = $"✓ Đã tính lương thành công cho kỳ {kyLuongThang}/{kyLuongNam}";
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("", "Có lỗi xảy ra khi tính lương. Vui lòng thử lại.");
                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Lỗi: {ex.Message}");
                return View();
            }
        }

        // ======================= DETAILS =======================
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Details(int id)
        {
            var item = await _bangTinhLuongService.GetByIdAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        // ======================= CREATE GET =======================
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Create()
        {
            var nhanVienList = await _nhanVienService.GetAll();
            ViewData["MaNV"] = new SelectList(nhanVienList, "MaNV", "TenNV");
            return View();
        }

        // ======================= CREATE POST =======================
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Create(BangTinhLuongRequest request)
        {
            if (ModelState.IsValid)
            {
                await _bangTinhLuongService.CreateAsync(request);
                TempData["SuccessMessage"] = "✓ Thêm mới bảng lương thành công";
                return RedirectToAction(nameof(Index));
            }

            var nhanVienList = await _nhanVienService.GetAll();
            ViewData["MaNV"] = new SelectList(nhanVienList, "MaNV", "TenNV", request.MaNV);
            return View(request);
        }

        // ======================= EDIT GET =======================
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _bangTinhLuongService.GetByIdAsync(id);
            if (item == null) return NotFound();

            var nhanVienList = await _nhanVienService.GetAll();
            ViewData["MaNV"] = new SelectList(nhanVienList, "MaNV", "TenNV", item.MaNV);
            return View(item);
        }

        // ======================= EDIT POST =======================
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Edit(int id, BangTinhLuongViewModel request)
        {
            if (ModelState.IsValid)
            {
                await _bangTinhLuongService.UpdateAsync(id, request);
                TempData["SuccessMessage"] = "✓ Cập nhật bảng lương thành công";
                return RedirectToAction(nameof(Index));
            }

            var nhanVienList = await _nhanVienService.GetAll();
            ViewData["MaNV"] = new SelectList(nhanVienList, "MaNV", "TenNV", request.MaNV);
            return View(request);
        }

        // ======================= DELETE GET =======================
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _bangTinhLuongService.GetByIdAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        // ======================= DELETE POST =======================
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _bangTinhLuongService.DeleteAsync(id);
            TempData["SuccessMessage"] = "✓ Xóa bảng lương thành công";
            return RedirectToAction(nameof(Index));
        }
    }
}
