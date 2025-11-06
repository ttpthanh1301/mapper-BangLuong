using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using BangLuong.Services;
using static BangLuong.ViewModels.PhongBanViewModels;

namespace BangLuong.Controllers
{
    public class PhongBanController : Controller
    {
        private readonly IPhongBanService _phongBanService;

        public PhongBanController(IPhongBanService phongBanService)
        {
            _phongBanService = phongBanService;
        }

        // GET: PhongBan
        public async Task<IActionResult> Index()
        {
            var list = await _phongBanService.GetAll();
            return View(list);
        }

        // GET: PhongBan/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null) return NotFound();

            var phongBan = await _phongBanService.GetById(id);
            if (phongBan == null) return NotFound();

            return View(phongBan);
        }

        // GET: PhongBan/Create
        public IActionResult Create() => View();

        // POST: PhongBan/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PhongBanRequest request)
        {
            if (!ModelState.IsValid) return View(request);

            await _phongBanService.Create(request);
            return RedirectToAction(nameof(Index));
        }

        // GET: PhongBan/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null) return NotFound();

            var phongBan = await _phongBanService.GetById(id);
            if (phongBan == null) return NotFound();

            return View(phongBan);
        }

        // POST: PhongBan/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, PhongBanViewModel request)
        {
            if (id != request.MaPB) return NotFound();
            if (!ModelState.IsValid) return View(request);

            await _phongBanService.Update(request);
            return RedirectToAction(nameof(Index));
        }

        // GET: PhongBan/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null) return NotFound();

            var phongBan = await _phongBanService.GetById(id);
            if (phongBan == null) return NotFound();

            return View(phongBan);
        }

        // POST: PhongBan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _phongBanService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
