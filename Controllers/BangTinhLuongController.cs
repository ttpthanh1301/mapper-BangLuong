using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BangLuong.Data;
using AutoMapper;
using static BangLuong.ViewModels.BangTinhLuongViewModels;
namespace BangLuong.Controllers
{
    public class BangTinhLuongController : Controller
    {
        private readonly BangLuongDbContext _context;
        private readonly IMapper _mapper;

        public BangTinhLuongController(BangLuongDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: BangTinhLuong
        public async Task<IActionResult> Index()
        {
            var bangLuongDbContext = _context.BangTinhLuong.Include(b => b.NhanVien);
            var bangTinhLuong = await bangLuongDbContext.ToListAsync();
            return View(_mapper.Map<IEnumerable<BangTinhLuongViewModel>>(bangTinhLuong));
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

            return View(_mapper.Map<BangTinhLuongViewModel>(bangTinhLuong));
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
        public async Task<IActionResult> Create(BangTinhLuongRequest request)
        {
            if (ModelState.IsValid)
            {
                var bangTinhLuong = _mapper.Map<BangTinhLuong>(request);
                _context.Add(bangTinhLuong);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaNV"] = new SelectList(_context.NhanVien, "MaNV", "MaNV", request.MaNV);
            return View(request);
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
            return View(_mapper.Map<BangTinhLuongViewModel>(bangTinhLuong));
        }

        // POST: BangTinhLuong/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BangTinhLuongViewModel bangTinhLuong)
        {
            if (id != bangTinhLuong.MaBL)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(_mapper.Map<BangTinhLuong>(bangTinhLuong));
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

            return View(_mapper.Map<BangTinhLuongViewModel>(bangTinhLuong));
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
