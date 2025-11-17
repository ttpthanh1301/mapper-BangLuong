using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BangLuong.Services;
using static BangLuong.ViewModels.ChiTietKyLuatViewModels;
using Microsoft.AspNetCore.Authorization;

namespace BangLuong.Controllers
{
    [Authorize(Roles = "Admin,Manager")] // Chỉ Admin và Manager mới được truy cập
    public class ChiTietKyLuatController : Controller
    {
        private readonly IChiTietKyLuatService _chiTietKyLuatService;
        private readonly INhanVienService _nhanVienService;
        private readonly IDanhMucKyLuatService _danhMucKyLuatService;

        public ChiTietKyLuatController(
            IChiTietKyLuatService chiTietKyLuatService,
            INhanVienService nhanVienService,
            IDanhMucKyLuatService danhMucKyLuatService)
        {
            _chiTietKyLuatService = chiTietKyLuatService;
            _nhanVienService = nhanVienService;
            _danhMucKyLuatService = danhMucKyLuatService;
        }

        // ======================= INDEX =======================
        public async Task<IActionResult> Index(
         string sortOrder,
         string currentFilter,
         string searchString,
         int? pageNumber)
        {
            int pageSize = 10;
            var list = await _chiTietKyLuatService.GetAllFilter(sortOrder, currentFilter, searchString, pageNumber, pageSize);

            ViewData["CurrentSort"] = sortOrder;
            ViewData["CurrentFilter"] = searchString;

            return View(list);
        }

        // ======================= DETAILS =======================
        public async Task<IActionResult> Details(int id)
        {
            var item = await _chiTietKyLuatService.GetByIdAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        // ======================= CREATE =======================
        public async Task<IActionResult> Create()
        {
            await LoadDropdownDataAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ChiTietKyLuatRequest request)
        {
            if (ModelState.IsValid)
            {
                await _chiTietKyLuatService.CreateAsync(request);
                return RedirectToAction(nameof(Index));
            }

            await LoadDropdownDataAsync(request.MaKL, request.MaNV);
            return View(request);
        }

        // ======================= EDIT =======================
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _chiTietKyLuatService.GetByIdAsync(id);
            if (item == null) return NotFound();

            await LoadDropdownDataAsync(item.MaKL, item.MaNV);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ChiTietKyLuatViewModel request)
        {
            if (ModelState.IsValid)
            {
                await _chiTietKyLuatService.UpdateAsync(id, request);
                return RedirectToAction(nameof(Index));
            }

            await LoadDropdownDataAsync(request.MaKL, request.MaNV);
            return View(request);
        }

        // ======================= DELETE =======================
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _chiTietKyLuatService.GetByIdAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _chiTietKyLuatService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // ======================= LOAD DROPDOWN =======================
        private async Task LoadDropdownDataAsync(string? selectedMaKL = null, string? selectedMaNV = null)
        {
            var kyLuatList = await _danhMucKyLuatService.GetAllAsync();
            var nhanVienList = await _nhanVienService.GetAll();

            ViewData["MaKL"] = new SelectList(kyLuatList, "MaKL", "TenKyLuat", selectedMaKL);
            ViewData["MaNV"] = new SelectList(nhanVienList, "MaNV", "HoTen", selectedMaNV);
        }
    }
}
