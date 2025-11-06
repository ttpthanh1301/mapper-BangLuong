using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static BangLuong.ViewModels.DanhMucKhenThuongViewModels;

namespace BangLuong.Controllers
{
    public class DanhMucKhenThuongController : Controller
    {
        private readonly IDanhMucKhenThuongService _service;

        public DanhMucKhenThuongController(IDanhMucKhenThuongService service)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DanhMucKhenThuongRequest request)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, DanhMucKhenThuongViewModel model)
        {
            if (id != model.MaKT) return NotFound();
            if (!ModelState.IsValid) return View(model);

            await _service.UpdateAsync(model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
