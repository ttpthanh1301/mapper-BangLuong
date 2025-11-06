using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BangLuong.Services;
using System.Threading.Tasks;
using static BangLuong.ViewModels.ChiTietPhuCapViewModels;

namespace BangLuong.Controllers
{
    public class ChiTietPhuCapController : Controller
    {
        private readonly IChiTietPhuCapService _chiTietPhuCapService;
        private readonly INhanVienService _nhanVienService;
        private readonly IDanhMucPhuCapService _danhMucPhuCapService;

        public ChiTietPhuCapController(
            IChiTietPhuCapService chiTietPhuCapService,
            INhanVienService nhanVienService,
            IDanhMucPhuCapService danhMucPhuCapService)
        {
            _chiTietPhuCapService = chiTietPhuCapService;
            _nhanVienService = nhanVienService;
            _danhMucPhuCapService = danhMucPhuCapService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _chiTietPhuCapService.GetAll();
            return View(list);
        }

        public async Task<IActionResult> Details(int id)
        {
            var item = await _chiTietPhuCapService.GetById(id);
            if (item == null) return NotFound();
            return View(item);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["MaPC"] = new SelectList(await _danhMucPhuCapService.GetAllAsync(), "MaPC", "MaPC");
            ViewData["MaNV"] = new SelectList(await _nhanVienService.GetAll(), "MaNV", "MaNV");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ChiTietPhuCapRequest request)
        {
            if (!ModelState.IsValid)
            {
                ViewData["MaPC"] = new SelectList(await _danhMucPhuCapService.GetAllAsync(), "MaPC", "MaPC", request.MaPC);
                ViewData["MaNV"] = new SelectList(await _nhanVienService.GetAll(), "MaNV", "MaNV", request.MaNV);
                return View(request);
            }

            await _chiTietPhuCapService.Create(request);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var item = await _chiTietPhuCapService.GetById(id);
            if (item == null) return NotFound();

            ViewData["MaPC"] = new SelectList(await _danhMucPhuCapService.GetAllAsync(), "MaPC", "MaPC", item.MaPC);
            ViewData["MaNV"] = new SelectList(await _nhanVienService.GetAll(), "MaNV", "MaNV", item.MaNV);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ChiTietPhuCapViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["MaPC"] = new SelectList(await _danhMucPhuCapService.GetAllAsync(), "MaPC", "MaPC", model.MaPC);
                ViewData["MaNV"] = new SelectList(await _nhanVienService.GetAll(), "MaNV", "MaNV", model.MaNV);
                return View(model);
            }

            await _chiTietPhuCapService.Update(model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var item = await _chiTietPhuCapService.GetById(id);
            if (item == null) return NotFound();
            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _chiTietPhuCapService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
