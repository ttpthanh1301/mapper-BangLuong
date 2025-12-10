using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using BangLuong.Services;
using static BangLuong.ViewModels.ChiTietKhenThuongViewModels;

// SỬ DỤNG 'USING STATIC' ĐỂ KHẮC PHỤC LỖI CS0138
using static BangLuong.ViewModels.NhanVienViewModels;
using static BangLuong.ViewModels.DanhMucKhenThuongViewModels;


namespace BangLuong.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class ChiTietKhenThuongController : Controller
    {
        private readonly IChiTietKhenThuongService _service;
        private readonly INhanVienService _nhanVienService;
        private readonly IDanhMucKhenThuongService _khenThuongService;

        public ChiTietKhenThuongController(
            IChiTietKhenThuongService service,
            INhanVienService nhanVienService,
            IDanhMucKhenThuongService khenThuongService)
        {
            _service = service;
            _nhanVienService = nhanVienService;
            _khenThuongService = khenThuongService;
        }

        // ======================= INDEX =======================
        public async Task<IActionResult> Index(
           string sortOrder,
           string currentFilter,
           string searchString,
           int? pageNumber)
        {
            int pageSize = 10;
            var list = await _service.GetAllFilter(sortOrder, currentFilter, searchString, pageNumber, pageSize);

            ViewData["CurrentSort"] = sortOrder;
            ViewData["CurrentFilter"] = searchString;

            return View(list);
        }

        // ======================= DETAILS =======================
        public async Task<IActionResult> Details(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        // ======================= CREATE =======================
        public async Task<IActionResult> Create()
        {
            try
            {
                var nhanVienList = (await _nhanVienService.GetAll())?.ToList() ?? new List<NhanVienViewModel>();
                var khenThuongList = (await _khenThuongService.GetAllAsync())?.ToList() ?? new List<DanhMucKhenThuongViewModel>();

                // SỬA: Tạo SelectList với Text là MaNV và HoTen
                var nhanVienSelectItems = nhanVienList.Select(n => new
                {
                    Value = n.MaNV,
                    Text = $"{n.MaNV} - {n.HoTen}" // Kết hợp Mã và Tên để hiển thị
                }).ToList();

                ViewData["MaNV"] = new SelectList(nhanVienSelectItems, "Value", "Text");
                ViewData["MaKT"] = new SelectList(khenThuongList, "MaKT", "TenKhenThuong");

                return View();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Exception in Create GET: {ex.Message}");
                Console.WriteLine($"[ERROR] StackTrace: {ex.StackTrace}");
                
                ViewData["MaNV"] = new SelectList(new List<NhanVienViewModel>());
                ViewData["MaKT"] = new SelectList(new List<DanhMucKhenThuongViewModel>());
                
                TempData["ErrorMessage"] = $"Lỗi khi tải dữ liệu: {ex.Message}";
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ChiTietKhenThuongRequest request)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _service.CreateAsync(request);
                    TempData["SuccessMessage"] = "Tạo khen thưởng thành công!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ERROR] Exception in Create POST: {ex.Message}");
                    ModelState.AddModelError("", ex.Message);
                }
            }

            // Reload data on error
            var nhanVienList = (await _nhanVienService.GetAll())?.ToList() ?? new List<NhanVienViewModel>();
            var khenThuongList = (await _khenThuongService.GetAllAsync())?.ToList() ?? new List<DanhMucKhenThuongViewModel>();

            // SỬA: Tạo SelectList với Text là MaNV và HoTen (khi lỗi)
            var nhanVienSelectItems = nhanVienList.Select(n => new
            {
                Value = n.MaNV,
                Text = $"{n.MaNV} - {n.HoTen}" // Kết hợp Mã và Tên để hiển thị
            }).ToList();

            ViewData["MaNV"] = new SelectList(nhanVienSelectItems, "Value", "Text", request.MaNV);
            ViewData["MaKT"] = new SelectList(khenThuongList, "MaKT", "TenKhenThuong", request.MaKT);

            return View(request);
        }

        // ======================= EDIT =======================
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();

            var nhanVienList = (await _nhanVienService.GetAll())?.ToList() ?? new List<NhanVienViewModel>();
            var khenThuongList = (await _khenThuongService.GetAllAsync())?.ToList() ?? new List<DanhMucKhenThuongViewModel>();

            // SỬA: Tạo SelectList với Text là MaNV và HoTen (Edit GET)
            var nhanVienSelectItems = nhanVienList.Select(n => new
            {
                Value = n.MaNV,
                Text = $"{n.MaNV} - {n.HoTen}" // Kết hợp Mã và Tên để hiển thị
            }).ToList();

            ViewData["MaNV"] = new SelectList(nhanVienSelectItems, "Value", "Text", item.MaNV);
            ViewData["MaKT"] = new SelectList(khenThuongList, "MaKT", "TenKhenThuong", item.MaKT);

            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ChiTietKhenThuongViewModel request)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _service.UpdateAsync(id, request);
                    TempData["SuccessMessage"] = "Cập nhật khen thưởng thành công!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ERROR] Exception in Edit POST: {ex.Message}");
                    ModelState.AddModelError("", ex.Message);
                }
            }

            var nhanVienList = (await _nhanVienService.GetAll())?.ToList() ?? new List<NhanVienViewModel>();
            var khenThuongList = (await _khenThuongService.GetAllAsync())?.ToList() ?? new List<DanhMucKhenThuongViewModel>();

            // SỬA: Tạo SelectList với Text là MaNV và HoTen (Edit POST)
            var nhanVienSelectItems = nhanVienList.Select(n => new
            {
                Value = n.MaNV,
                Text = $"{n.MaNV} - {n.HoTen}" // Kết hợp Mã và Tên để hiển thị
            }).ToList();

            ViewData["MaNV"] = new SelectList(nhanVienSelectItems, "Value", "Text", request.MaNV);
            ViewData["MaKT"] = new SelectList(khenThuongList, "MaKT", "TenKhenThuong", request.MaKT);

            return View(request);
        }

        // ======================= DELETE =======================
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();

            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                TempData["SuccessMessage"] = "Xóa khen thưởng thành công!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Exception in Delete: {ex.Message}");
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }
    }
}