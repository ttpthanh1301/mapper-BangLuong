using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BangLuong.Data;
using BangLuong.Data.Entities;
using AutoMapper;
using static BangLuong.ViewModels.ChucVuViewModels;

namespace BangLuong.Controllers
{
    public class ChucVuController : Controller
    {
        private readonly BangLuongDbContext _context;
        private readonly IMapper _mapper;

        public ChucVuController(BangLuongDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: ChucVu
        public async Task<IActionResult> Index()
        {
            var chucVu = await _context.ChucVu.ToListAsync();
            return View(_mapper.Map<IEnumerable<ChucVuViewModel>>(chucVu));
        }

        // GET: ChucVu/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chucVu = await _context.ChucVu
                .FirstOrDefaultAsync(m => m.MaCV == id);
            if (chucVu == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<ChucVuViewModel>(chucVu));
        }

        // GET: ChucVu/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ChucVu/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ChucVuRequest request)
        {
            if (ModelState.IsValid)
            {
                var chucVu = _mapper.Map<ChucVu>(request);
                _context.Add(chucVu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(request);
        }

        // GET: ChucVu/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chucVu = await _context.ChucVu.FindAsync(id);
            if (chucVu == null)
            {
                return NotFound();
            }
            return View(_mapper.Map<ChucVuViewModel>(chucVu));
        }

        // POST: ChucVu/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, ChucVuViewModel chucVu)
        {
            if (id != chucVu.MaCV)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(_mapper.Map<ChucVu>(chucVu));
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChucVuExists(chucVu.MaCV))
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
            return View(chucVu);
        }

        // GET: ChucVu/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chucVu = await _context.ChucVu
                .FirstOrDefaultAsync(m => m.MaCV == id);
            if (chucVu == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<ChucVuViewModel>(chucVu));
        }

        // POST: ChucVu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var chucVu = await _context.ChucVu.FindAsync(id);
            if (chucVu != null)
            {
                _context.ChucVu.Remove(chucVu);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChucVuExists(string id)
        {
            return _context.ChucVu.Any(e => e.MaCV == id);
        }
    }
}
