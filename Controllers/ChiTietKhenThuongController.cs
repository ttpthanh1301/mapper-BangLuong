using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BangLuong.Services;
using static BangLuong.ViewModels.ChiTietKhenThuongViewModels;
using Microsoft.AspNetCore.Authorization;

namespace BangLuong.Controllers
{
    [Authorize(Roles = "Admin,Manager")] // Chỉ Admin và Manager mới được truy cập
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
            var nhanVienList = await _nhanVienService.GetAll();
            var khenThuongList = await _khenThuongService.GetAllAsync();

            ViewData["MaNV"] = new SelectList(nhanVienList, "MaNV", "TenNV");
            ViewData["MaKT"] = new SelectList(khenThuongList, "MaKT", "TenKhenThuong");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ChiTietKhenThuongRequest request)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateAsync(request);
                return RedirectToAction(nameof(Index));
            }

            var nhanVienList = await _nhanVienService.GetAll();
            var khenThuongList = await _khenThuongService.GetAllAsync();

            ViewData["MaNV"] = new SelectList(nhanVienList, "MaNV", "TenNV", request.MaNV);
            ViewData["MaKT"] = new SelectList(khenThuongList, "MaKT", "TenKhenThuong", request.MaKT);

            return View(request);
        }

        // ======================= EDIT =======================
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();

            var nhanVienList = await _nhanVienService.GetAll();
            var khenThuongList = await _khenThuongService.GetAllAsync();

            ViewData["MaNV"] = new SelectList(nhanVienList, "MaNV", "TenNV", item.MaNV);
            ViewData["MaKT"] = new SelectList(khenThuongList, "MaKT", "TenKhenThuong", item.MaKT);

            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ChiTietKhenThuongViewModel request)
        {
            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(id, request);
                return RedirectToAction(nameof(Index));
            }

            var nhanVienList = await _nhanVienService.GetAll();
            var khenThuongList = await _khenThuongService.GetAllAsync();

            ViewData["MaNV"] = new SelectList(nhanVienList, "MaNV", "TenNV", request.MaNV);
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
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
