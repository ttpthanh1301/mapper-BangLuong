using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BangLuong.Services;
using static BangLuong.ViewModels.ChamCongViewModels;
using System.Threading.Tasks;

namespace BangLuong.Controllers
{
    public class ChamCongController : Controller
    {
        private readonly IChamCongService _chamCongService;
        private readonly INhanVienService _nhanVienService; // nếu có service nhân viên

        public ChamCongController(IChamCongService chamCongService, INhanVienService nhanVienService)
        {
            _chamCongService = chamCongService;
            _nhanVienService = nhanVienService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _chamCongService.GetAll();
            return View(list);
        }

        public async Task<IActionResult> Details(int id)
        {
            var item = await _chamCongService.GetById(id);
            if (item == null) return NotFound();
            return View(item);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["MaNV"] = new SelectList(await _nhanVienService.GetAll(), "MaNV", "MaNV");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ChamCongRequest request)
        {
            if (!ModelState.IsValid)
            {
                ViewData["MaNV"] = new SelectList(await _nhanVienService.GetAll(), "MaNV", "MaNV", request.MaNV);
                return View(request);
            }

            await _chamCongService.Create(request);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var item = await _chamCongService.GetById(id);
            if (item == null) return NotFound();

            ViewData["MaNV"] = new SelectList(await _nhanVienService.GetAll(), "MaNV", "MaNV", item.MaNV);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ChamCongViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["MaNV"] = new SelectList(await _nhanVienService.GetAll(), "MaNV", "MaNV", model.MaNV);
                return View(model);
            }

            await _chamCongService.Update(model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var item = await _chamCongService.GetById(id);
            if (item == null) return NotFound();
            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _chamCongService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
