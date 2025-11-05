using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BangLuong.Data;
using AutoMapper;
using static BangLuong.ViewModels.ChiTietKyLuatViewModels;

namespace BangLuong.Controllers
{
    public class ChiTietKyLuatController : Controller
    {
        private readonly BangLuongDbContext _context;
        private readonly IMapper _mapper;

        public ChiTietKyLuatController(BangLuongDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: ChiTietKyLuat
        public async Task<IActionResult> Index()
        {
            var bangLuongDbContext = _context.ChiTietKyLuat.Include(c => c.DanhMucKyLuat).Include(c => c.NhanVien);
            var chiTietKyLuat = await bangLuongDbContext.ToListAsync();
            return View(_mapper.Map<IEnumerable<ChiTietKyLuatViewModel>>(chiTietKyLuat));
        }

        // GET: ChiTietKyLuat/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chiTietKyLuat = await _context.ChiTietKyLuat
                .Include(c => c.DanhMucKyLuat)
                .Include(c => c.NhanVien)
                .FirstOrDefaultAsync(m => m.MaCTKL == id);
            if (chiTietKyLuat == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<ChiTietKyLuatViewModel>(chiTietKyLuat));
        }

        // GET: ChiTietKyLuat/Create
        public IActionResult Create()
        {
            ViewData["MaKL"] = new SelectList(_context.DanhMucKyLuat, "MaKL", "MaKL");
            ViewData["MaNV"] = new SelectList(_context.NhanVien, "MaNV", "MaNV");
            return View();
        }

        // POST: ChiTietKyLuat/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ChiTietKyLuatRequest request)
        {
            if (ModelState.IsValid)
            {
                var chiTietKyLuat = _mapper.Map<ChiTietKyLuat>(request);
                _context.Add(chiTietKyLuat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaKL"] = new SelectList(_context.DanhMucKyLuat, "MaKL", "MaKL", request.MaKL);
            ViewData["MaNV"] = new SelectList(_context.NhanVien, "MaNV", "MaNV", request.MaNV);
            return View(request);
        }

        // GET: ChiTietKyLuat/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chiTietKyLuat = await _context.ChiTietKyLuat.FindAsync(id);
            if (chiTietKyLuat == null)
            {
                return NotFound();
            }
            ViewData["MaKL"] = new SelectList(_context.DanhMucKyLuat, "MaKL", "MaKL", chiTietKyLuat.MaKL);
            ViewData["MaNV"] = new SelectList(_context.NhanVien, "MaNV", "MaNV", chiTietKyLuat.MaNV);
            return View(_mapper.Map<ChiTietKyLuatViewModel>(chiTietKyLuat));
        }

        // POST: ChiTietKyLuat/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ChiTietKyLuatViewModel chiTietKyLuat)
        {
            if (id != chiTietKyLuat.MaCTKL)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(_mapper.Map<ChiTietKyLuat>(chiTietKyLuat));
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChiTietKyLuatExists(chiTietKyLuat.MaCTKL))
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
            ViewData["MaKL"] = new SelectList(_context.DanhMucKyLuat, "MaKL", "MaKL", chiTietKyLuat.MaKL);
            ViewData["MaNV"] = new SelectList(_context.NhanVien, "MaNV", "MaNV", chiTietKyLuat.MaNV);
            return View(chiTietKyLuat);
        }

        // GET: ChiTietKyLuat/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chiTietKyLuat = await _context.ChiTietKyLuat
                .Include(c => c.DanhMucKyLuat)
                .Include(c => c.NhanVien)
                .FirstOrDefaultAsync(m => m.MaCTKL == id);
            if (chiTietKyLuat == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<ChiTietKyLuatViewModel>(chiTietKyLuat));
        }

        // POST: ChiTietKyLuat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chiTietKyLuat = await _context.ChiTietKyLuat.FindAsync(id);
            if (chiTietKyLuat != null)
            {
                _context.ChiTietKyLuat.Remove(chiTietKyLuat);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChiTietKyLuatExists(int id)
        {
            return _context.ChiTietKyLuat.Any(e => e.MaCTKL == id);
        }
    }
}
