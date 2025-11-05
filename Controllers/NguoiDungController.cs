using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BangLuong.Data;
using AutoMapper;
using static BangLuong.ViewModels.NguoiDungViewModels;

namespace BangLuong.Controllers
{
    public class NguoiDungController : Controller
    {
        private readonly BangLuongDbContext _context;
        private readonly IMapper _mapper;

        public NguoiDungController(BangLuongDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: NguoiDung
        public async Task<IActionResult> Index()
        {
            var bangLuongDbContext = _context.NguoiDung.Include(n => n.NhanVien);
            var nguoiDung = await bangLuongDbContext.ToListAsync();
            return View(_mapper.Map<IEnumerable<NguoiDungViewModel>>(nguoiDung));
        }

        // GET: NguoiDung/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nguoiDung = await _context.NguoiDung
                .Include(n => n.NhanVien)
                .FirstOrDefaultAsync(m => m.MaNV == id);
            if (nguoiDung == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<NguoiDungViewModel>(nguoiDung));
        }

        // GET: NguoiDung/Create
        public IActionResult Create()
        {
            ViewData["MaNV"] = new SelectList(_context.NhanVien, "MaNV", "MaNV");
            return View();
        }

        // POST: NguoiDung/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NguoiDungRequest request)
        {
            if (ModelState.IsValid)
            {
                var nguoiDung = _mapper.Map<NguoiDung>(request);
                _context.Add(nguoiDung);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaNV"] = new SelectList(_context.NhanVien, "MaNV", "MaNV", request.MaNV);
            return View(request);
        }

        // GET: NguoiDung/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nguoiDung = await _context.NguoiDung.FindAsync(id);
            if (nguoiDung == null)
            {
                return NotFound();
            }
            ViewData["MaNV"] = new SelectList(_context.NhanVien, "MaNV", "MaNV", nguoiDung.MaNV);
            return View(_mapper.Map<NguoiDungViewModel>(nguoiDung));
        }

        // POST: NguoiDung/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id,NguoiDungViewModel nguoiDung)
        {
            if (id != nguoiDung.MaNV)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(_mapper.Map<NguoiDung>(nguoiDung));
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NguoiDungExists(nguoiDung.MaNV))
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
            ViewData["MaNV"] = new SelectList(_context.NhanVien, "MaNV", "MaNV", nguoiDung.MaNV);
            return View(nguoiDung);
        }

        // GET: NguoiDung/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nguoiDung = await _context.NguoiDung
                .Include(n => n.NhanVien)
                .FirstOrDefaultAsync(m => m.MaNV == id);
            if (nguoiDung == null)
            {
                return NotFound();
            }

            return View(nguoiDung);
        }

        // POST: NguoiDung/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var nguoiDung = await _context.NguoiDung.FindAsync(id);
            if (nguoiDung != null)
            {
                _context.NguoiDung.Remove(nguoiDung);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NguoiDungExists(string id)
        {
            return _context.NguoiDung.Any(e => e.MaNV == id);
        }
    }
}
