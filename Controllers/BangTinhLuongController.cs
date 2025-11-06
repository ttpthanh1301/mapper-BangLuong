using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BangLuong.Services;
using static BangLuong.ViewModels.BangTinhLuongViewModels;

namespace BangLuong.Controllers
{
    public class BangTinhLuongController : Controller
    {
        private readonly IBangTinhLuongService _bangTinhLuongService;
        private readonly INhanVienService _nhanVienService;

        public BangTinhLuongController(IBangTinhLuongService bangTinhLuongService, INhanVienService nhanVienService)
        {
            _bangTinhLuongService = bangTinhLuongService;
            _nhanVienService = nhanVienService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _bangTinhLuongService.GetAllAsync();
            return View(list);
        }

        public async Task<IActionResult> Details(int id)
        {
            var item = await _bangTinhLuongService.GetByIdAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        public async Task<IActionResult> Create()
        {
            var nhanVienList = await _nhanVienService.GetAll();
            ViewData["MaNV"] = new SelectList(nhanVienList, "MaNV", "MaNV");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BangTinhLuongRequest request)
        {
            if (ModelState.IsValid)
            {
                await _bangTinhLuongService.CreateAsync(request);
                return RedirectToAction(nameof(Index));
            }

            var nhanVienList = await _nhanVienService.GetAll();
            ViewData["MaNV"] = new SelectList(nhanVienList, "MaNV", "MaNV", request.MaNV);
            return View(request);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var item = await _bangTinhLuongService.GetByIdAsync(id);
            if (item == null) return NotFound();

            var nhanVienList = await _nhanVienService.GetAll();
            ViewData["MaNV"] = new SelectList(nhanVienList, "MaNV", "MaNV", item.MaNV);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BangTinhLuongViewModel request)
        {
            if (ModelState.IsValid)
            {
                await _bangTinhLuongService.UpdateAsync(id, request);
                return RedirectToAction(nameof(Index));
            }

            var nhanVienList = await _nhanVienService.GetAll();
            ViewData["MaNV"] = new SelectList(nhanVienList, "MaNV", "MaNV", request.MaNV);
            return View(request);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var item = await _bangTinhLuongService.GetByIdAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _bangTinhLuongService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
