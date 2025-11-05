using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BangLuong.Data;
using AutoMapper;
using static BangLuong.ViewModels.DanhMucKyLuatViewModels; 
namespace BangLuong.Controllers
{
    public class DanhMucKyLuatController : Controller
    {
        private readonly BangLuongDbContext _context;
        private readonly IMapper _mapper;

        public DanhMucKyLuatController(BangLuongDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: DanhMucKyLuat
        public async Task<IActionResult> Index()
        {
            var danhMucKyLuat = await _context.DanhMucKyLuat.ToListAsync();
            return View(_mapper.Map<IEnumerable<DanhMucKyLuatViewModel>>(danhMucKyLuat));
        }

        // GET: DanhMucKyLuat/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var danhMucKyLuat = await _context.DanhMucKyLuat
                .FirstOrDefaultAsync(m => m.MaKL == id);
            if (danhMucKyLuat == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<DanhMucKyLuatViewModel>(danhMucKyLuat));
        }

        // GET: DanhMucKyLuat/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DanhMucKyLuat/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DanhMucKyLuatRequest request)
        {
            if (ModelState.IsValid)
            { 
                var danhMucKyLuat = _mapper.Map<DanhMucKyLuat>(request);
                _context.Add(danhMucKyLuat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(request);
        }

        // GET: DanhMucKyLuat/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var danhMucKyLuat = await _context.DanhMucKyLuat.FindAsync(id);
            if (danhMucKyLuat == null)
            {
                return NotFound();
            }
            return View(_mapper.Map<DanhMucKyLuatViewModel>(danhMucKyLuat));
        }

        // POST: DanhMucKyLuat/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, DanhMucKyLuatViewModel danhMucKyLuat)
        {
            if (id != danhMucKyLuat.MaKL)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(_mapper.Map<DanhMucKyLuat>(danhMucKyLuat));
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DanhMucKyLuatExists(danhMucKyLuat.MaKL))
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
            return View(danhMucKyLuat);
        }

        // GET: DanhMucKyLuat/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var danhMucKyLuat = await _context.DanhMucKyLuat
                .FirstOrDefaultAsync(m => m.MaKL == id);
            if (danhMucKyLuat == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<DanhMucKyLuatViewModel>(danhMucKyLuat));
        }

        // POST: DanhMucKyLuat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var danhMucKyLuat = await _context.DanhMucKyLuat.FindAsync(id);
            if (danhMucKyLuat != null)
            {
                _context.DanhMucKyLuat.Remove(danhMucKyLuat);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DanhMucKyLuatExists(string id)
        {
            return _context.DanhMucKyLuat.Any(e => e.MaKL == id);
        }
    }
}
