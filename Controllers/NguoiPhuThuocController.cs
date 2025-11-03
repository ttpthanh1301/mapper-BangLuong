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
    public class NguoiPhuThuocController : Controller
    {
        private readonly BangLuongDbContext _context;

        public NguoiPhuThuocController(BangLuongDbContext context)
        {
            _context = context;
        }

        // GET: NguoiPhuThuoc
        public async Task<IActionResult> Index()
        {
            var bangLuongDbContext = _context.NguoiPhuThuoc.Include(n => n.NhanVien);
            return View(await bangLuongDbContext.ToListAsync());
        }

        // GET: NguoiPhuThuoc/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nguoiPhuThuoc = await _context.NguoiPhuThuoc
                .Include(n => n.NhanVien)
                .FirstOrDefaultAsync(m => m.MaNPT == id);
            if (nguoiPhuThuoc == null)
            {
                return NotFound();
            }

            return View(nguoiPhuThuoc);
        }

        // GET: NguoiPhuThuoc/Create
        public IActionResult Create()
        {
            ViewData["MaNV"] = new SelectList(_context.NhanVien, "MaNV", "MaNV");
            return View();
        }

        // POST: NguoiPhuThuoc/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaNPT,HoTen,NgaySinh,MoiQuanHe,ThoiGianBatDauGiamTru,ThoiGianKetThucGiamTru,MaNV")] NguoiPhuThuoc nguoiPhuThuoc)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nguoiPhuThuoc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaNV"] = new SelectList(_context.NhanVien, "MaNV", "MaNV", nguoiPhuThuoc.MaNV);
            return View(nguoiPhuThuoc);
        }

        // GET: NguoiPhuThuoc/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nguoiPhuThuoc = await _context.NguoiPhuThuoc.FindAsync(id);
            if (nguoiPhuThuoc == null)
            {
                return NotFound();
            }
            ViewData["MaNV"] = new SelectList(_context.NhanVien, "MaNV", "MaNV", nguoiPhuThuoc.MaNV);
            return View(nguoiPhuThuoc);
        }

        // POST: NguoiPhuThuoc/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaNPT,HoTen,NgaySinh,MoiQuanHe,ThoiGianBatDauGiamTru,ThoiGianKetThucGiamTru,MaNV")] NguoiPhuThuoc nguoiPhuThuoc)
        {
            if (id != nguoiPhuThuoc.MaNPT)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nguoiPhuThuoc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NguoiPhuThuocExists(nguoiPhuThuoc.MaNPT))
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
            ViewData["MaNV"] = new SelectList(_context.NhanVien, "MaNV", "MaNV", nguoiPhuThuoc.MaNV);
            return View(nguoiPhuThuoc);
        }

        // GET: NguoiPhuThuoc/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nguoiPhuThuoc = await _context.NguoiPhuThuoc
                .Include(n => n.NhanVien)
                .FirstOrDefaultAsync(m => m.MaNPT == id);
            if (nguoiPhuThuoc == null)
            {
                return NotFound();
            }

            return View(nguoiPhuThuoc);
        }

        // POST: NguoiPhuThuoc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nguoiPhuThuoc = await _context.NguoiPhuThuoc.FindAsync(id);
            if (nguoiPhuThuoc != null)
            {
                _context.NguoiPhuThuoc.Remove(nguoiPhuThuoc);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NguoiPhuThuocExists(int id)
        {
            return _context.NguoiPhuThuoc.Any(e => e.MaNPT == id);
        }
    }
}
