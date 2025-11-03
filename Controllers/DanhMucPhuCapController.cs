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
    public class DanhMucPhuCapController : Controller
    {
        private readonly BangLuongDbContext _context;

        public DanhMucPhuCapController(BangLuongDbContext context)
        {
            _context = context;
        }

        // GET: DanhMucPhuCap
        public async Task<IActionResult> Index()
        {
            return View(await _context.DanhMucPhuCap.ToListAsync());
        }

        // GET: DanhMucPhuCap/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var danhMucPhuCap = await _context.DanhMucPhuCap
                .FirstOrDefaultAsync(m => m.MaPC == id);
            if (danhMucPhuCap == null)
            {
                return NotFound();
            }

            return View(danhMucPhuCap);
        }

        // GET: DanhMucPhuCap/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DanhMucPhuCap/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaPC,TenPhuCap,SoTien")] DanhMucPhuCap danhMucPhuCap)
        {
            if (ModelState.IsValid)
            {
                _context.Add(danhMucPhuCap);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(danhMucPhuCap);
        }

        // GET: DanhMucPhuCap/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var danhMucPhuCap = await _context.DanhMucPhuCap.FindAsync(id);
            if (danhMucPhuCap == null)
            {
                return NotFound();
            }
            return View(danhMucPhuCap);
        }

        // POST: DanhMucPhuCap/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaPC,TenPhuCap,SoTien")] DanhMucPhuCap danhMucPhuCap)
        {
            if (id != danhMucPhuCap.MaPC)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(danhMucPhuCap);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DanhMucPhuCapExists(danhMucPhuCap.MaPC))
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
            return View(danhMucPhuCap);
        }

        // GET: DanhMucPhuCap/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var danhMucPhuCap = await _context.DanhMucPhuCap
                .FirstOrDefaultAsync(m => m.MaPC == id);
            if (danhMucPhuCap == null)
            {
                return NotFound();
            }

            return View(danhMucPhuCap);
        }

        // POST: DanhMucPhuCap/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var danhMucPhuCap = await _context.DanhMucPhuCap.FindAsync(id);
            if (danhMucPhuCap != null)
            {
                _context.DanhMucPhuCap.Remove(danhMucPhuCap);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DanhMucPhuCapExists(string id)
        {
            return _context.DanhMucPhuCap.Any(e => e.MaPC == id);
        }
    }
}
