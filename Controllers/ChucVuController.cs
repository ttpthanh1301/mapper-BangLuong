using Microsoft.AspNetCore.Mvc;
using BangLuong.Services;
using System.Threading.Tasks;
using static BangLuong.ViewModels.ChucVuViewModels;

namespace BangLuong.Controllers
{
    public class ChucVuController : Controller
    {
        private readonly IChucVuService _chucVuService;

        public ChucVuController(IChucVuService chucVuService)
        {
            _chucVuService = chucVuService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _chucVuService.GetAllAsync();
            return View(list);
        }

        public async Task<IActionResult> Details(string id)
        {
            var chucVu = await _chucVuService.GetByIdAsync(id);
            if (chucVu == null) return NotFound();
            return View(chucVu);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ChucVuRequest request)
        {
            if (!ModelState.IsValid) return View(request);
            await _chucVuService.CreateAsync(request);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(string id)
        {
            var chucVu = await _chucVuService.GetByIdAsync(id);
            if (chucVu == null) return NotFound();
            return View(chucVu);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, ChucVuViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);
            var result = await _chucVuService.UpdateAsync(id, viewModel);
            if (!result) return NotFound();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string id)
        {
            var chucVu = await _chucVuService.GetByIdAsync(id);
            if (chucVu == null) return NotFound();
            return View(chucVu);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _chucVuService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
