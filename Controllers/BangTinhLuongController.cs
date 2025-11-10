using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BangLuong.Services;
using static BangLuong.ViewModels.BangTinhLuongViewModels;
using System;
using System.Threading.Tasks;

namespace BangLuong.Controllers
{
    public class BangTinhLuongController : Controller
    {
        private readonly IBangTinhLuongService _bangTinhLuongService;
        private readonly INhanVienService _nhanVienService;

        public BangTinhLuongController(IBangTinhLuongService bangTinhLuongService, INhanVienService nhanVienService)
        {
            _bangTinhLuongService = bangTinhLuongService;
            _nhanVienService = nhanVienService;
        }

        /// <summary>
        /// Danh sách bảng lương
        /// </summary>
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            int pageSize = 10;
            var list = await _bangTinhLuongService.GetAllFilter(sortOrder, currentFilter, searchString, pageNumber, pageSize);

            ViewData["CurrentSort"] = sortOrder;
            ViewData["CurrentFilter"] = searchString;

            return View(list); // Chỉ hiển thị danh sách, không liên quan đến tính lương
        }

        /// <summary>
        /// Form tính lương theo kỳ (GET)
        /// </summary>
        public IActionResult TinhLuongTheoKy()
        {
            ViewBag.CurrentMonth = DateTime.Now.Month;
            ViewBag.CurrentYear = DateTime.Now.Year;
            return View();
        }

        /// <summary>
        /// Xử lý tính lương tự động (POST)
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TinhLuongTheoKy(int kyLuongThang, int kyLuongNam)
        {
            if (kyLuongThang < 1 || kyLuongThang > 12)
            {
                ModelState.AddModelError("kyLuongThang", "Tháng phải từ 1 đến 12");
                ViewBag.CurrentMonth = DateTime.Now.Month;
                ViewBag.CurrentYear = DateTime.Now.Year;
                return View();
            }

            if (kyLuongNam < 2000 || kyLuongNam > 2100)
            {
                ModelState.AddModelError("kyLuongNam", "Năm phải từ 2000 đến 2100");
                ViewBag.CurrentMonth = kyLuongThang;
                ViewBag.CurrentYear = DateTime.Now.Year;
                return View();
            }

            var currentDate = DateTime.Now;
            var kyLuongDate = new DateTime(kyLuongNam, kyLuongThang, 1);
            if (kyLuongDate > currentDate.AddMonths(1))
            {
                ModelState.AddModelError("", $"Không thể tính lương cho kỳ tương lai (tháng {kyLuongThang}/{kyLuongNam})");
                ViewBag.CurrentMonth = kyLuongThang;
                ViewBag.CurrentYear = kyLuongNam;
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
                ViewBag.CurrentMonth = kyLuongThang;
                ViewBag.CurrentYear = kyLuongNam;
                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Lỗi: {ex.Message}");
                ViewBag.CurrentMonth = kyLuongThang;
                ViewBag.CurrentYear = kyLuongNam;
                return View();
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            var item = await _bangTinhLuongService.GetByIdAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        public async Task<IActionResult> Create()
        {
            var nhanVienList = await _nhanVienService.GetAll();
            ViewData["MaNV"] = new SelectList(nhanVienList, "MaNV", "TenNV");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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

        public async Task<IActionResult> Edit(int id)
        {
            var item = await _bangTinhLuongService.GetByIdAsync(id);
            if (item == null) return NotFound();

            var nhanVienList = await _nhanVienService.GetAll();
            ViewData["MaNV"] = new SelectList(nhanVienList, "MaNV", "TenNV", item.MaNV);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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

        public async Task<IActionResult> Delete(int id)
        {
            var item = await _bangTinhLuongService.GetByIdAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _bangTinhLuongService.DeleteAsync(id);
            TempData["SuccessMessage"] = "✓ Xóa bảng lương thành công";
            return RedirectToAction(nameof(Index));
        }
    }
}
