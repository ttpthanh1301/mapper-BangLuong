using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BangLuong.Data;
using AutoMapper;
using static BangLuong.ViewModels.NguoiPhuThuocViewModels;
namespace BangLuong.Controllers
{
    public class NguoiPhuThuocController : Controller
    {
        private readonly BangLuongDbContext _context;
        private readonly IMapper _mapper;

        public NguoiPhuThuocController(BangLuongDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: NguoiPhuThuoc
        public async Task<IActionResult> Index()
        {
            var bangLuongDbContext = _context.NguoiPhuThuoc.Include(n => n.NhanVien);
            var nguoiPhuThuoc = await bangLuongDbContext.ToListAsync();
            return View(_mapper.Map<IEnumerable<NguoiPhuThuocViewModel>>(nguoiPhuThuoc));
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

            return View(_mapper.Map<NguoiPhuThuocViewModel>(nguoiPhuThuoc));
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
        public async Task<IActionResult> Create(NguoiPhuThuocRequest request)
        {
            if (ModelState.IsValid)
            {
                var nguoiPhuThuoc = _mapper.Map<NguoiPhuThuoc>(request);
                _context.Add(nguoiPhuThuoc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaNV"] = new SelectList(_context.NhanVien, "MaNV", "MaNV", request.MaNV);
            return View(request);
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
            return View(_mapper.Map<NguoiPhuThuocViewModel>(nguoiPhuThuoc));
        }

        // POST: NguoiPhuThuoc/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, NguoiPhuThuocViewModel nguoiPhuThuoc)
        {
            if (id != nguoiPhuThuoc.MaNPT)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(_mapper.Map<NguoiPhuThuoc>(nguoiPhuThuoc));
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

            return View(_mapper.Map<NguoiPhuThuocViewModel>(nguoiPhuThuoc));
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
