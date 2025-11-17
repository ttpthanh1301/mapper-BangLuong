using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BangLuong.Services;
using static BangLuong.ViewModels.ThamSoHeThongViewModels;
using Microsoft.AspNetCore.Authorization;

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


        public async Task<IActionResult> Details(string id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        public IActionResult Create() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ThamSoHeThongRequest request)
        {
            if (!ModelState.IsValid) return View(request);

            await _service.CreateAsync(request);
            return RedirectToAction(nameof(Index));
        }

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

            var result = await _service.UpdateAsync(id, model);
            if (!result) return NotFound();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
