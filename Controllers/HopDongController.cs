using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BangLuong.Services;
using static BangLuong.ViewModels.HopDongViewModels;
using System.Threading.Tasks;

namespace BangLuong.Controllers
{
    public class HopDongController : Controller
    {
        private readonly IHopDongService _hopDongService;
        private readonly INhanVienService _nhanVienService; // nếu có service nhân viên

        public HopDongController(IHopDongService hopDongService, INhanVienService nhanVienService)
        {
            _hopDongService = hopDongService;
            _nhanVienService = nhanVienService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _hopDongService.GetAll();
            return View(list);
        }

        public async Task<IActionResult> Details(int id)
        {
            var item = await _hopDongService.GetById(id);
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
        public async Task<IActionResult> Create(HopDongRequest request)
        {
            if (!ModelState.IsValid)
            {
                ViewData["MaNV"] = new SelectList(await _nhanVienService.GetAll(), "MaNV", "MaNV", request.MaNV);
                return View(request);
            }

            await _hopDongService.Create(request);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var item = await _hopDongService.GetById(id);
            if (item == null) return NotFound();

            ViewData["MaNV"] = new SelectList(await _nhanVienService.GetAll(), "MaNV", "MaNV", item.MaNV);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(HopDongViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["MaNV"] = new SelectList(await _nhanVienService.GetAll(), "MaNV", "MaNV", model.MaNV);
                return View(model);
            }

            await _hopDongService.Update(model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var item = await _hopDongService.GetById(id);
            if (item == null) return NotFound();
            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _hopDongService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
