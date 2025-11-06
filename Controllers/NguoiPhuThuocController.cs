using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BangLuong.Services;
using System.Threading.Tasks;
using static BangLuong.ViewModels.NguoiPhuThuocViewModels;

namespace BangLuong.Controllers
{
    public class NguoiPhuThuocController : Controller
    {
        private readonly INguoiPhuThuocService _service;

        public NguoiPhuThuocController(INguoiPhuThuocService service)
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


        public async Task<IActionResult> Details(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        public async Task<IActionResult> Create()
        {
            var maNVList = await _service.GetAllNhanVienIdsAsync();
            ViewData["MaNV"] = new SelectList(maNVList);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NguoiPhuThuocRequest request)
        {
            if (!ModelState.IsValid)
            {
                var maNVList = await _service.GetAllNhanVienIdsAsync();
                ViewData["MaNV"] = new SelectList(maNVList, request.MaNV);
                return View(request);
            }

            await _service.CreateAsync(request);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();

            var maNVList = await _service.GetAllNhanVienIdsAsync();
            ViewData["MaNV"] = new SelectList(maNVList, item.MaNV);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, NguoiPhuThuocViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var maNVList = await _service.GetAllNhanVienIdsAsync();
                ViewData["MaNV"] = new SelectList(maNVList, viewModel.MaNV);
                return View(viewModel);
            }

            var result = await _service.UpdateAsync(id, viewModel);
            if (!result) return NotFound();
            return RedirectToAction(nameof(Index));
        }

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
