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
    public class BangTinhLuongController : Controller
    {
        private readonly BangLuongDbContext _context;

        public BangTinhLuongController(BangLuongDbContext context)
        {
            _context = context;
        }

        // GET: BangTinhLuong
        public async Task<IActionResult> Index()
        {
            var bangLuongDbContext = _context.BangTinhLuong.Include(b => b.NhanVien);
            return View(await bangLuongDbContext.ToListAsync());
        }

        // GET: BangTinhLuong/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bangTinhLuong = await _context.BangTinhLuong
                .Include(b => b.NhanVien)
                .FirstOrDefaultAsync(m => m.MaBL == id);
            if (bangTinhLuong == null)
            {
                return NotFound();
            }

            return View(bangTinhLuong);
        }

        // GET: BangTinhLuong/Create
        public IActionResult Create()
        {
            ViewData["MaNV"] = new SelectList(_context.NhanVien, "MaNV", "MaNV");
            return View();
        }

        // POST: BangTinhLuong/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaBL,KyLuongThang,KyLuongNam,LuongCoBan,TongPhuCap,TongKhenThuong,LuongTangCa,TongThuNhap,GiamTruBHXH,GiamTruBHYT,GiamTruBHTN,TongGiamTruKyLuat,GiamTruThueTNCN,ThucLanh,TrangThai,MaNV")] BangTinhLuong bangTinhLuong)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bangTinhLuong);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaNV"] = new SelectList(_context.NhanVien, "MaNV", "MaNV", bangTinhLuong.MaNV);
            return View(bangTinhLuong);
        }

        // GET: BangTinhLuong/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bangTinhLuong = await _context.BangTinhLuong.FindAsync(id);
            if (bangTinhLuong == null)
            {
                return NotFound();
            }
            ViewData["MaNV"] = new SelectList(_context.NhanVien, "MaNV", "MaNV", bangTinhLuong.MaNV);
            return View(bangTinhLuong);
        }

        // POST: BangTinhLuong/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaBL,KyLuongThang,KyLuongNam,LuongCoBan,TongPhuCap,TongKhenThuong,LuongTangCa,TongThuNhap,GiamTruBHXH,GiamTruBHYT,GiamTruBHTN,TongGiamTruKyLuat,GiamTruThueTNCN,ThucLanh,TrangThai,MaNV")] BangTinhLuong bangTinhLuong)
        {
            if (id != bangTinhLuong.MaBL)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bangTinhLuong);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BangTinhLuongExists(bangTinhLuong.MaBL))
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
            ViewData["MaNV"] = new SelectList(_context.NhanVien, "MaNV", "MaNV", bangTinhLuong.MaNV);
            return View(bangTinhLuong);
        }

        // GET: BangTinhLuong/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bangTinhLuong = await _context.BangTinhLuong
                .Include(b => b.NhanVien)
                .FirstOrDefaultAsync(m => m.MaBL == id);
            if (bangTinhLuong == null)
            {
                return NotFound();
            }

            return View(bangTinhLuong);
        }

        // POST: BangTinhLuong/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bangTinhLuong = await _context.BangTinhLuong.FindAsync(id);
            if (bangTinhLuong != null)
            {
                _context.BangTinhLuong.Remove(bangTinhLuong);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BangTinhLuongExists(int id)
        {
            return _context.BangTinhLuong.Any(e => e.MaBL == id);
        }
    }
}
