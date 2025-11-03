using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BangLuong.Data;
using BangLuong.Data.Entities;

namespace BangLuong.Controllers
{
    public class HopDongController : Controller
    {
        private readonly BangLuongDbContext _context;

        public HopDongController(BangLuongDbContext context)
        {
            _context = context;
        }

        // GET: HopDong
        public async Task<IActionResult> Index()
        {
            var bangLuongDbContext = _context.HopDong.Include(h => h.NhanVien);
            return View(await bangLuongDbContext.ToListAsync());
        }

        // GET: HopDong/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hopDong = await _context.HopDong
                .Include(h => h.NhanVien)
                .FirstOrDefaultAsync(m => m.MaHD == id);
            if (hopDong == null)
            {
                return NotFound();
            }

            return View(hopDong);
        }

        // GET: HopDong/Create
        public IActionResult Create()
        {
            ViewData["MaNV"] = new SelectList(_context.NhanVien, "MaNV", "MaNV");
            return View();
        }

        // POST: HopDong/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaHD,SoHopDong,LoaiHD,NgayBatDau,NgayKetThuc,LuongCoBan,PhuCapAnTrua,PhuCapXangXe,PhuCapDienThoai,PhuCapTrachNhiem,PhuCapKhac,TrangThai,MaNV")] HopDong hopDong)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hopDong);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaNV"] = new SelectList(_context.NhanVien, "MaNV", "MaNV", hopDong.MaNV);
            return View(hopDong);
        }

        // GET: HopDong/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hopDong = await _context.HopDong.FindAsync(id);
            if (hopDong == null)
            {
                return NotFound();
            }
            ViewData["MaNV"] = new SelectList(_context.NhanVien, "MaNV", "MaNV", hopDong.MaNV);
            return View(hopDong);
        }

        // POST: HopDong/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaHD,SoHopDong,LoaiHD,NgayBatDau,NgayKetThuc,LuongCoBan,PhuCapAnTrua,PhuCapXangXe,PhuCapDienThoai,PhuCapTrachNhiem,PhuCapKhac,TrangThai,MaNV")] HopDong hopDong)
        {
            if (id != hopDong.MaHD)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hopDong);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HopDongExists(hopDong.MaHD))
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
            ViewData["MaNV"] = new SelectList(_context.NhanVien, "MaNV", "MaNV", hopDong.MaNV);
            return View(hopDong);
        }

        // GET: HopDong/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hopDong = await _context.HopDong
                .Include(h => h.NhanVien)
                .FirstOrDefaultAsync(m => m.MaHD == id);
            if (hopDong == null)
            {
                return NotFound();
            }

            return View(hopDong);
        }

        // POST: HopDong/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hopDong = await _context.HopDong.FindAsync(id);
            if (hopDong != null)
            {
                _context.HopDong.Remove(hopDong);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HopDongExists(int id)
        {
            return _context.HopDong.Any(e => e.MaHD == id);
        }
    }
}
