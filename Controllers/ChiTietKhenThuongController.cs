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
    public class ChiTietKhenThuongController : Controller
    {
        private readonly BangLuongDbContext _context;

        public ChiTietKhenThuongController(BangLuongDbContext context)
        {
            _context = context;
        }

        // GET: ChiTietKhenThuong
        public async Task<IActionResult> Index()
        {
            var bangLuongDbContext = _context.ChiTietKhenThuong.Include(c => c.DanhMucKhenThuong).Include(c => c.NhanVien);
            return View(await bangLuongDbContext.ToListAsync());
        }

        // GET: ChiTietKhenThuong/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chiTietKhenThuong = await _context.ChiTietKhenThuong
                .Include(c => c.DanhMucKhenThuong)
                .Include(c => c.NhanVien)
                .FirstOrDefaultAsync(m => m.MaCTKT == id);
            if (chiTietKhenThuong == null)
            {
                return NotFound();
            }

            return View(chiTietKhenThuong);
        }

        // GET: ChiTietKhenThuong/Create
        public IActionResult Create()
        {
            ViewData["MaKT"] = new SelectList(_context.DanhMucKhenThuong, "MaKT", "MaKT");
            ViewData["MaNV"] = new SelectList(_context.NhanVien, "MaNV", "MaNV");
            return View();
        }

        // POST: ChiTietKhenThuong/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaCTKT,NgayKhenThuong,LyDo,MaKT,MaNV")] ChiTietKhenThuong chiTietKhenThuong)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chiTietKhenThuong);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaKT"] = new SelectList(_context.DanhMucKhenThuong, "MaKT", "MaKT", chiTietKhenThuong.MaKT);
            ViewData["MaNV"] = new SelectList(_context.NhanVien, "MaNV", "MaNV", chiTietKhenThuong.MaNV);
            return View(chiTietKhenThuong);
        }

        // GET: ChiTietKhenThuong/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chiTietKhenThuong = await _context.ChiTietKhenThuong.FindAsync(id);
            if (chiTietKhenThuong == null)
            {
                return NotFound();
            }
            ViewData["MaKT"] = new SelectList(_context.DanhMucKhenThuong, "MaKT", "MaKT", chiTietKhenThuong.MaKT);
            ViewData["MaNV"] = new SelectList(_context.NhanVien, "MaNV", "MaNV", chiTietKhenThuong.MaNV);
            return View(chiTietKhenThuong);
        }

        // POST: ChiTietKhenThuong/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaCTKT,NgayKhenThuong,LyDo,MaKT,MaNV")] ChiTietKhenThuong chiTietKhenThuong)
        {
            if (id != chiTietKhenThuong.MaCTKT)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chiTietKhenThuong);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChiTietKhenThuongExists(chiTietKhenThuong.MaCTKT))
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
            ViewData["MaKT"] = new SelectList(_context.DanhMucKhenThuong, "MaKT", "MaKT", chiTietKhenThuong.MaKT);
            ViewData["MaNV"] = new SelectList(_context.NhanVien, "MaNV", "MaNV", chiTietKhenThuong.MaNV);
            return View(chiTietKhenThuong);
        }

        // GET: ChiTietKhenThuong/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chiTietKhenThuong = await _context.ChiTietKhenThuong
                .Include(c => c.DanhMucKhenThuong)
                .Include(c => c.NhanVien)
                .FirstOrDefaultAsync(m => m.MaCTKT == id);
            if (chiTietKhenThuong == null)
            {
                return NotFound();
            }

            return View(chiTietKhenThuong);
        }

        // POST: ChiTietKhenThuong/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chiTietKhenThuong = await _context.ChiTietKhenThuong.FindAsync(id);
            if (chiTietKhenThuong != null)
            {
                _context.ChiTietKhenThuong.Remove(chiTietKhenThuong);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChiTietKhenThuongExists(int id)
        {
            return _context.ChiTietKhenThuong.Any(e => e.MaCTKT == id);
        }
    }
}
