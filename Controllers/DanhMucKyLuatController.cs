using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BangLuong.Services;
using static BangLuong.ViewModels.DanhMucKyLuatViewModels;

namespace BangLuong.Controllers
{
    public class DanhMucKyLuatController : Controller
    {
        private readonly IDanhMucKyLuatService _danhMucKyLuatService;

        public DanhMucKyLuatController(IDanhMucKyLuatService danhMucKyLuatService)
        {
            _danhMucKyLuatService = danhMucKyLuatService;
        }

        // GET: DanhMucKyLuat
        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? pageNumber)
        {
            int pageSize = 10; // Số bản ghi mỗi trang

            var list = await _danhMucKyLuatService.GetAllFilter(sortOrder, currentFilter, searchString, pageNumber, pageSize);

            ViewData["CurrentSort"] = sortOrder;
            ViewData["CurrentFilter"] = searchString;

            return View(list);
        }

        // GET: DanhMucKyLuat/Details/{id}
        public async Task<IActionResult> Details(string id)
        {
            if (id == null) return NotFound();

            var item = await _danhMucKyLuatService.GetByIdAsync(id);
            if (item == null) return NotFound();

            return View(item);
        }

        // GET: DanhMucKyLuat/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DanhMucKyLuat/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DanhMucKyLuatRequest request)
        {
            if (ModelState.IsValid)
            {
                var success = await _danhMucKyLuatService.CreateAsync(request);
                if (success)
                    return RedirectToAction(nameof(Index));
            }
            return View(request);
        }

        // GET: DanhMucKyLuat/Edit/{id}
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null) return NotFound();

            var item = await _danhMucKyLuatService.GetByIdAsync(id);
            if (item == null) return NotFound();

            return View(item);
        }

        // POST: DanhMucKyLuat/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, DanhMucKyLuatViewModel model)
        {
            if (id != model.MaKL) return NotFound();

            if (ModelState.IsValid)
            {
                var success = await _danhMucKyLuatService.UpdateAsync(model);
                if (success)
                    return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: DanhMucKyLuat/Delete/{id}
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null) return NotFound();

            var item = await _danhMucKyLuatService.GetByIdAsync(id);
            if (item == null) return NotFound();

            return View(item);
        }

        // POST: DanhMucKyLuat/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _danhMucKyLuatService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
