using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BangLuong.Services;
using static BangLuong.ViewModels.ChiTietKyLuatViewModels;

namespace BangLuong.Controllers
{
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

        // GET: ChiTietKyLuat
        public async Task<IActionResult> Index()
        {
            var list = await _chiTietKyLuatService.GetAllAsync();
            return View(list);
        }

        // GET: ChiTietKyLuat/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var item = await _chiTietKyLuatService.GetByIdAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        // GET: ChiTietKyLuat/Create
        public async Task<IActionResult> Create()
        {
            await LoadDropdownDataAsync();
            return View();
        }

        // POST: ChiTietKyLuat/Create
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

        // GET: ChiTietKyLuat/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _chiTietKyLuatService.GetByIdAsync(id);
            if (item == null) return NotFound();

            await LoadDropdownDataAsync(item.MaKL, item.MaNV);
            return View(item);
        }

        // POST: ChiTietKyLuat/Edit/5
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

        // GET: ChiTietKyLuat/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _chiTietKyLuatService.GetByIdAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        // POST: ChiTietKyLuat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _chiTietKyLuatService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // ✅ Hàm dùng để nạp dữ liệu dropdown list (dùng service, không dùng DbContext)
        private async Task LoadDropdownDataAsync(string? selectedMaKL = null, string? selectedMaNV = null)
        {
            var kyLuatList = await _danhMucKyLuatService.GetAllAsync();
            var nhanVienList = await _nhanVienService.GetAll();

            ViewData["MaKL"] = new SelectList(kyLuatList, "MaKL", "TenKyLuat", selectedMaKL);
            ViewData["MaNV"] = new SelectList(nhanVienList, "MaNV", "HoTen", selectedMaNV);
        }
    }
}
