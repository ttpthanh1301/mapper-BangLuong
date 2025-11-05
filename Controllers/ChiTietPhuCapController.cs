using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BangLuong.Data;
using AutoMapper;
using static BangLuong.ViewModels.ChiTietPhuCapViewModels;
namespace BangLuong.Controllers
{
    public class ChiTietPhuCapController : Controller
    {
        private readonly BangLuongDbContext _context;
        private readonly IMapper _mapper;

        public ChiTietPhuCapController(BangLuongDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: ChiTietPhuCap
        public async Task<IActionResult> Index()
        {
            var bangLuongDbContext = _context.ChiTietPhuCap.Include(c => c.DanhMucPhuCap).Include(c => c.NhanVien);
            var chiTietPhuCap = await bangLuongDbContext.ToListAsync();
            return View(_mapper.Map<IEnumerable<ChiTietPhuCapViewModel>>(chiTietPhuCap));
        }

        // GET: ChiTietPhuCap/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chiTietPhuCap = await _context.ChiTietPhuCap
                .Include(c => c.DanhMucPhuCap)
                .Include(c => c.NhanVien)
                .FirstOrDefaultAsync(m => m.MaCTPC == id);
            if (chiTietPhuCap == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<ChiTietPhuCapViewModel>(chiTietPhuCap));
        }

        // GET: ChiTietPhuCap/Create
        public IActionResult Create()
        {
            ViewData["MaPC"] = new SelectList(_context.DanhMucPhuCap, "MaPC", "MaPC");
            ViewData["MaNV"] = new SelectList(_context.NhanVien, "MaNV", "MaNV");
            return View();
        }

        // POST: ChiTietPhuCap/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ChiTietPhuCapRequest request)
        {
            if (ModelState.IsValid)
            {
                var chiTietPhuCap = _mapper.Map<ChiTietPhuCap>(request);
                _context.Add(chiTietPhuCap);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaPC"] = new SelectList(_context.DanhMucPhuCap, "MaPC", "MaPC", request.MaPC);
            ViewData["MaNV"] = new SelectList(_context.NhanVien, "MaNV", "MaNV", request.MaNV);
            return View(request);
        }

        // GET: ChiTietPhuCap/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chiTietPhuCap = await _context.ChiTietPhuCap.FindAsync(id);
            if (chiTietPhuCap == null)
            {
                return NotFound();
            }
            ViewData["MaPC"] = new SelectList(_context.DanhMucPhuCap, "MaPC", "MaPC", chiTietPhuCap.MaPC);
            ViewData["MaNV"] = new SelectList(_context.NhanVien, "MaNV", "MaNV", chiTietPhuCap.MaNV);
            return View(_mapper.Map<ChiTietPhuCapViewModel>(chiTietPhuCap));
        }

        // POST: ChiTietPhuCap/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ChiTietPhuCapViewModel chiTietPhuCap)
        {
            if (id != chiTietPhuCap.MaCTPC)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(_mapper.Map<ChiTietPhuCap>(chiTietPhuCap));
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChiTietPhuCapExists(chiTietPhuCap.MaCTPC))
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
            ViewData["MaPC"] = new SelectList(_context.DanhMucPhuCap, "MaPC", "MaPC", chiTietPhuCap.MaPC);
            ViewData["MaNV"] = new SelectList(_context.NhanVien, "MaNV", "MaNV", chiTietPhuCap.MaNV);
            return View(chiTietPhuCap);
        }

        // GET: ChiTietPhuCap/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chiTietPhuCap = await _context.ChiTietPhuCap
                .Include(c => c.DanhMucPhuCap)
                .Include(c => c.NhanVien)
                .FirstOrDefaultAsync(m => m.MaCTPC == id);
            if (chiTietPhuCap == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<ChiTietPhuCapViewModel>(chiTietPhuCap));
        }

        // POST: ChiTietPhuCap/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chiTietPhuCap = await _context.ChiTietPhuCap.FindAsync(id);
            if (chiTietPhuCap != null)
            {
                _context.ChiTietPhuCap.Remove(chiTietPhuCap);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChiTietPhuCapExists(int id)
        {
            return _context.ChiTietPhuCap.Any(e => e.MaCTPC == id);
        }
    }
}
