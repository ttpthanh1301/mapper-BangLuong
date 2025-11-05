using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BangLuong.Data;
using BangLuong.Data.Entities;
using AutoMapper;
using static BangLuong.ViewModels.HopDongViewModels;

namespace BangLuong.Controllers
{
    public class HopDongController : Controller
    {
        private readonly BangLuongDbContext _context;
        private readonly IMapper _mapper;
        public HopDongController(BangLuongDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: HopDong
        public async Task<IActionResult> Index()
        {
            var bangLuongDbContext = _context.HopDong.Include(h => h.NhanVien);
            var hopDong = await bangLuongDbContext.ToListAsync();
            return View(_mapper.Map<IEnumerable<HopDongViewModel>>(hopDong));
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

            return View(_mapper.Map<HopDongViewModel>(hopDong));
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
        public async Task<IActionResult> Create(HopDongRequest request)
        {
            if (ModelState.IsValid)
            {
                 var hopDong = _mapper.Map<HopDong>(request);
                _context.Add(hopDong);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaNV"] = new SelectList(_context.NhanVien, "MaNV", "MaNV", request.MaNV);
            return View(request);
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
            return View(_mapper.Map<HopDongViewModel>(hopDong));
        }

        // POST: HopDong/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, HopDongViewModel hopDong)
        {
            if (id != hopDong.MaHD)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(_mapper.Map<HopDong>(hopDong));
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

            return View(_mapper.Map<HopDongViewModel>(hopDong));
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
