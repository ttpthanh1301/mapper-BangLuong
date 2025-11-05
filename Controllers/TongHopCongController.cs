using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BangLuong.Data;
using AutoMapper;
using static BangLuong.ViewModels.TongHopCongViewModels;

namespace BangLuong.Controllers
{
    public class TongHopCongController : Controller
    {
        private readonly BangLuongDbContext _context;
        private readonly IMapper _mapper;

        public TongHopCongController(BangLuongDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: TongHopCong
        public async Task<IActionResult> Index()
        {
            var bangLuongDbContext = _context.TongHopCong.Include(t => t.NhanVien);
            var tongHopCong = await bangLuongDbContext.ToListAsync();
            return View(_mapper.Map<IEnumerable<TongHopCongViewModel>>(tongHopCong));
        }

        // GET: TongHopCong/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tongHopCong = await _context.TongHopCong
                .Include(t => t.NhanVien)
                .FirstOrDefaultAsync(m => m.MaTHC == id);
            if (tongHopCong == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<TongHopCongViewModel>(tongHopCong));
        }

        // GET: TongHopCong/Create
        public IActionResult Create()
        {
            ViewData["MaNV"] = new SelectList(_context.NhanVien, "MaNV", "MaNV");
            return View();
        }

        // POST: TongHopCong/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TongHopCongRequest request)
        {
            if (ModelState.IsValid)
            {
                var tongHopCong = _mapper.Map<TongHopCong>(request);
                _context.Add(tongHopCong);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaNV"] = new SelectList(_context.NhanVien, "MaNV", "MaNV", request.MaNV);
            return View(request);
        }

        // GET: TongHopCong/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tongHopCong = await _context.TongHopCong.FindAsync(id);
            if (tongHopCong == null)
            {
                return NotFound();
            }
            ViewData["MaNV"] = new SelectList(_context.NhanVien, "MaNV", "MaNV", tongHopCong.MaNV);
            return View(_mapper.Map<TongHopCongViewModel>(tongHopCong));
        }

        // POST: TongHopCong/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TongHopCongViewModel tongHopCong)
        {
            if (id != tongHopCong.MaTHC)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(_mapper.Map<TongHopCong>(tongHopCong));
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TongHopCongExists(tongHopCong.MaTHC))
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
            ViewData["MaNV"] = new SelectList(_context.NhanVien, "MaNV", "MaNV", tongHopCong.MaNV);
            return View(tongHopCong);
        }

        // GET: TongHopCong/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tongHopCong = await _context.TongHopCong
                .Include(t => t.NhanVien)
                .FirstOrDefaultAsync(m => m.MaTHC == id);
            if (tongHopCong == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<TongHopCongViewModel>(tongHopCong));
        }

        // POST: TongHopCong/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tongHopCong = await _context.TongHopCong.FindAsync(id);
            if (tongHopCong != null)
            {
                _context.TongHopCong.Remove(tongHopCong);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TongHopCongExists(int id)
        {
            return _context.TongHopCong.Any(e => e.MaTHC == id);
        }
    }
}
