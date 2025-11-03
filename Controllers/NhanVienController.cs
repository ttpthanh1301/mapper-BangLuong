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
using static BangLuong.ViewModels.NhanVienViewModels;
using Microsoft.Build.Experimental.ProjectCache;
using BangLuong.Services;

namespace BangLuong.Controllers
{

    public class NhanVienController : Controller
    {
        private readonly INhanVienService _NhanVienservice;
        public NhanVienController(INhanVienService NhanVienservice)
        {

            _NhanVienservice = NhanVienservice;
        }

        // GET: NhanVien
public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber, int pageSize = 10)
{
    ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) || sortOrder.Equals("name") ? "name_desc" : "name";
    ViewData["GenderSortParm"] = String.IsNullOrEmpty(sortOrder) || sortOrder.Equals("gender") ? "gender_desc" : "gender";
    ViewData["DateSortParm"] = String.IsNullOrEmpty(sortOrder) || sortOrder.Equals("date") ? "date_desc" : "date";
    ViewData["CurrentFilter"] = searchString;
    ViewData["CurrentSort"] = sortOrder;

    var result = await _NhanVienservice.GetAllFilter(sortOrder, currentFilter, searchString, pageNumber, pageSize);
    return View(result);
}

        // GET: NhanVien/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var nhanVien = await _NhanVienservice.GetById(id);
            if (nhanVien == null)
            {
                return NotFound();
            }

            return View(nhanVien);
        }

        // GET: NhanVien/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NhanVien/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NhanVienRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            await _NhanVienservice.Create(request);
            return RedirectToAction(nameof(Index));
        }

        // GET: NhanVien/Edit/5
        public async Task<IActionResult> Edit(string id)
        {

            var nhanVien = await _NhanVienservice.GetById(id);
            if (nhanVien == null)
            {
                return NotFound();
            }
            return View(nhanVien);
        }

        // POST: NhanVien/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, NhanVienViewModel nhanVien)
        {
            if (id != nhanVien.MaNV)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
               await _NhanVienservice.Update(nhanVien);
                return RedirectToAction(nameof(Index));
            }
            return View(nhanVien);
        }

        // GET: NhanVien/Delete/5
        public async Task<IActionResult> Delete(string id)
        {

            var nhanVien = await _NhanVienservice.GetById(id);
            if (nhanVien == null)
            {
                return NotFound();
            }
            return View();
        }

        // POST: NhanVien/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _NhanVienservice.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

