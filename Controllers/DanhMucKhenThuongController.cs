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
    public class DanhMucKhenThuongController : Controller
    {
        private readonly BangLuongDbContext _context;

        public DanhMucKhenThuongController(BangLuongDbContext context)
        {
            _context = context;
        }

        // GET: DanhMucKhenThuong
        public async Task<IActionResult> Index()
        {
            return View(await _context.DanhMucKhenThuong.ToListAsync());
        }

        // GET: DanhMucKhenThuong/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var danhMucKhenThuong = await _context.DanhMucKhenThuong
                .FirstOrDefaultAsync(m => m.MaKT == id);
            if (danhMucKhenThuong == null)
            {
                return NotFound();
            }

            return View(danhMucKhenThuong);
        }

        // GET: DanhMucKhenThuong/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DanhMucKhenThuong/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaKT,TenKhenThuong,SoTien")] DanhMucKhenThuong danhMucKhenThuong)
        {
            if (ModelState.IsValid)
            {
                _context.Add(danhMucKhenThuong);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(danhMucKhenThuong);
        }

        // GET: DanhMucKhenThuong/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var danhMucKhenThuong = await _context.DanhMucKhenThuong.FindAsync(id);
            if (danhMucKhenThuong == null)
            {
                return NotFound();
            }
            return View(danhMucKhenThuong);
        }

        // POST: DanhMucKhenThuong/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaKT,TenKhenThuong,SoTien")] DanhMucKhenThuong danhMucKhenThuong)
        {
            if (id != danhMucKhenThuong.MaKT)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(danhMucKhenThuong);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DanhMucKhenThuongExists(danhMucKhenThuong.MaKT))
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
            return View(danhMucKhenThuong);
        }

        // GET: DanhMucKhenThuong/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var danhMucKhenThuong = await _context.DanhMucKhenThuong
                .FirstOrDefaultAsync(m => m.MaKT == id);
            if (danhMucKhenThuong == null)
            {
                return NotFound();
            }

            return View(danhMucKhenThuong);
        }

        // POST: DanhMucKhenThuong/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var danhMucKhenThuong = await _context.DanhMucKhenThuong.FindAsync(id);
            if (danhMucKhenThuong != null)
            {
                _context.DanhMucKhenThuong.Remove(danhMucKhenThuong);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DanhMucKhenThuongExists(string id)
        {
            return _context.DanhMucKhenThuong.Any(e => e.MaKT == id);
        }
    }
}
