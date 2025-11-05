using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BangLuong.Data;
using AutoMapper;
using static BangLuong.ViewModels.ChamCongViewModels;

namespace BangLuong.Controllers
{
    public class ChamCongController : Controller
    {
        private readonly BangLuongDbContext _context;
        private readonly IMapper _mapper;

        public ChamCongController(BangLuongDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: ChamCong
        public async Task<IActionResult> Index()
        {
            var bangLuongDbContext = _context.ChamCong.Include(c => c.NhanVien);
            var chamCong = await bangLuongDbContext.ToListAsync();
            return View(_mapper.Map<IEnumerable<ChamCongViewModel>>(chamCong));
        }

        // GET: ChamCong/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chamCong = await _context.ChamCong
                .Include(c => c.NhanVien)
                .FirstOrDefaultAsync(m => m.MaCC == id);
            if (chamCong == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<ChamCongViewModel>(chamCong));
        }

        // GET: ChamCong/Create
        public IActionResult Create()
        {
            ViewData["MaNV"] = new SelectList(_context.NhanVien, "MaNV", "MaNV");
            return View();
        }

        // POST: ChamCong/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ChamCongRequest request)
        {
            if (ModelState.IsValid)
            {
                var chamCong = _mapper.Map<ChamCong>(Request);
                _context.Add(chamCong);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaNV"] = new SelectList(_context.NhanVien, "MaNV", "MaNV", request.MaNV);
            return View(request);
        }

        // GET: ChamCong/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chamCong = await _context.ChamCong.FindAsync(id);
            if (chamCong == null)
            {
                return NotFound();
            }
            ViewData["MaNV"] = new SelectList(_context.NhanVien, "MaNV", "MaNV", chamCong.MaNV);
            return View(_mapper.Map<ChamCongViewModel>(chamCong));
        }

        // POST: ChamCong/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ChamCongViewModel chamCong)
        {
            if (id != chamCong.MaCC)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(_mapper.Map<ChamCong>(chamCong));
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChamCongExists(chamCong.MaCC))
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
            ViewData["MaNV"] = new SelectList(_context.NhanVien, "MaNV", "MaNV", chamCong.MaNV);
            return View(chamCong);
        }

        // GET: ChamCong/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chamCong = await _context.ChamCong
                .Include(c => c.NhanVien)
                .FirstOrDefaultAsync(m => m.MaCC == id);
            if (chamCong == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<ChamCongViewModel>(chamCong));
        }

        // POST: ChamCong/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chamCong = await _context.ChamCong.FindAsync(id);
            if (chamCong != null)
            {
                _context.ChamCong.Remove(chamCong);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChamCongExists(int id)
        {
            return _context.ChamCong.Any(e => e.MaCC == id);
        }
    }
}
