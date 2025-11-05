using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BangLuong.Data;
using AutoMapper;
using static BangLuong.ViewModels.ThamSoHeThongViewModels;

namespace BangLuong.Controllers
{
    public class ThamSoHeThongController : Controller
    {
        private readonly BangLuongDbContext _context;
        private readonly IMapper _mapper;

        public ThamSoHeThongController(BangLuongDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: ThamSoHeThong
        public async Task<IActionResult> Index()
        {
            var thamSoHeThong = await _context.ThamSoHeThong.ToListAsync();
            return View(_mapper.Map<IEnumerable<ThamSoHeThongViewModel>>(thamSoHeThong));
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

            return View(_mapper.Map<ThamSoHeThongViewModel>(thamSoHeThong));
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
        public async Task<IActionResult> Create(ThamSoHeThongRequest request)
        {
            if (ModelState.IsValid)
            {
                var thamSoHeThong = _mapper.Map<ThamSoHeThong>(request);
                _context.Add(thamSoHeThong);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(request);
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
            return View(_mapper.Map<ThamSoHeThongViewModel>(thamSoHeThong));
        }

        // POST: ThamSoHeThong/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, ThamSoHeThongViewModel thamSoHeThong)
        {
            if (id != thamSoHeThong.MaTS)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(_mapper.Map<ThamSoHeThong>(thamSoHeThong));
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

            return View(_mapper.Map<ThamSoHeThongViewModel>(thamSoHeThong));
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
