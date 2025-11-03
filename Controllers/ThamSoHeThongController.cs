using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BangLuong.Data;

namespace BangLuong.Controllers
{
    public class ThamSoHeThongController : Controller
    {
        private readonly BangLuongDbContext _context;

        public ThamSoHeThongController(BangLuongDbContext context)
        {
            _context = context;
        }

        // GET: ThamSoHeThong
        public async Task<IActionResult> Index()
        {
            return View(await _context.ThamSoHeThong.ToListAsync());
        }

        // GET: ThamSoHeThong/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thamSoHeThong = await _context.ThamSoHeThong
                .FirstOrDefaultAsync(m => m.MaTS == id);
            if (thamSoHeThong == null)
            {
                return NotFound();
            }

            return View(thamSoHeThong);
        }

        // GET: ThamSoHeThong/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ThamSoHeThong/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaTS,TenThamSo,GiaTri,NgayApDung")] ThamSoHeThong thamSoHeThong)
        {
            if (ModelState.IsValid)
            {
                _context.Add(thamSoHeThong);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(thamSoHeThong);
        }

        // GET: ThamSoHeThong/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thamSoHeThong = await _context.ThamSoHeThong.FindAsync(id);
            if (thamSoHeThong == null)
            {
                return NotFound();
            }
            return View(thamSoHeThong);
        }

        // POST: ThamSoHeThong/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaTS,TenThamSo,GiaTri,NgayApDung")] ThamSoHeThong thamSoHeThong)
        {
            if (id != thamSoHeThong.MaTS)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(thamSoHeThong);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ThamSoHeThongExists(thamSoHeThong.MaTS))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(thamSoHeThong);
        }

        // GET: ThamSoHeThong/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thamSoHeThong = await _context.ThamSoHeThong
                .FirstOrDefaultAsync(m => m.MaTS == id);
            if (thamSoHeThong == null)
            {
                return NotFound();
            }

            return View(thamSoHeThong);
        }

        // POST: ThamSoHeThong/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var thamSoHeThong = await _context.ThamSoHeThong.FindAsync(id);
            if (thamSoHeThong != null)
            {
                _context.ThamSoHeThong.Remove(thamSoHeThong);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ThamSoHeThongExists(string id)
        {
            return _context.ThamSoHeThong.Any(e => e.MaTS == id);
        }
    }
}
