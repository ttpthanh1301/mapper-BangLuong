using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BangLuong.Data;
using AutoMapper;
using static BangLuong.ViewModels.DanhMucPhuCapViewModels;

namespace BangLuong.Controllers
{
    public class DanhMucPhuCapController : Controller
    {
        private readonly BangLuongDbContext _context;
        private readonly IMapper _mapper;

        public DanhMucPhuCapController(BangLuongDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: DanhMucPhuCap
        public async Task<IActionResult> Index()
        {
            var danhMucPhuCap = await _context.DanhMucPhuCap.ToListAsync();
            return View(_mapper.Map<IEnumerable<DanhMucPhuCapViewModel>>(danhMucPhuCap));
        }

        // GET: DanhMucPhuCap/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var danhMucPhuCap = await _context.DanhMucPhuCap
                .FirstOrDefaultAsync(m => m.MaPC == id);
            if (danhMucPhuCap == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<DanhMucPhuCapViewModel>(danhMucPhuCap));
        }

        // GET: DanhMucPhuCap/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DanhMucPhuCap/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DanhMucPhuCapRequest request)
        {
            if (ModelState.IsValid)
            {
                var danhMucPhuCap = _mapper.Map<DanhMucPhuCap>(request);
                _context.Add(danhMucPhuCap);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(request);
        }

        // GET: DanhMucPhuCap/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var danhMucPhuCap = await _context.DanhMucPhuCap.FindAsync(id);
            if (danhMucPhuCap == null)
            {
                return NotFound();
            }
            return View(_mapper.Map<DanhMucPhuCapViewModel>(danhMucPhuCap));
        }

        // POST: DanhMucPhuCap/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, DanhMucPhuCapViewModel danhMucPhuCap)
        {
            if (id != danhMucPhuCap.MaPC)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(_mapper.Map<DanhMucPhuCap>(danhMucPhuCap));
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DanhMucPhuCapExists(danhMucPhuCap.MaPC))
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
            return View(danhMucPhuCap);
        }

        // GET: DanhMucPhuCap/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var danhMucPhuCap = await _context.DanhMucPhuCap
                .FirstOrDefaultAsync(m => m.MaPC == id);
            if (danhMucPhuCap == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<DanhMucPhuCapViewModel>(danhMucPhuCap));
        }

        // POST: DanhMucPhuCap/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var danhMucPhuCap = await _context.DanhMucPhuCap.FindAsync(id);
            if (danhMucPhuCap != null)
            {
                _context.DanhMucPhuCap.Remove(danhMucPhuCap);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DanhMucPhuCapExists(string id)
        {
            return _context.DanhMucPhuCap.Any(e => e.MaPC == id);
        }
    }
}
