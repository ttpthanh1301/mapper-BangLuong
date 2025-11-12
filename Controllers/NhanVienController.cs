using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BangLuong.Data;
using BangLuong.Data.Entities;
using BangLuong.Services;
using AutoMapper;
using static BangLuong.ViewModels.NhanVienViewModels;


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
          public async Task<IActionResult> ExportToExcel()
        {
            try
            {
                var fileBytes = await _NhanVienservice.ExportToExcel();
                var fileName = $"DanhSachNhanVien_{DateTime.Now:yyyyMMddHHmmss}.xlsx";
                return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Lỗi khi export: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        // Import from Excel - GET
        public IActionResult Import()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Import(IFormFile file)
        {
            if (file == null)
            {
                TempData["Error"] = "Vui lòng chọn file";
                return View();
            }

            var result = await _NhanVienservice.ImportFromExcel(file);

            if (result.success)
            {
                TempData["Success"] = result.message;
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["Error"] = result.message;
                return View();
            }
        }
           // Download Template
        public IActionResult DownloadTemplate()
        {
            using (var workbook = new ClosedXML.Excel.XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("NhanVien");

                // Header
                worksheet.Cell(1, 1).Value = "Mã NV";
                worksheet.Cell(1, 2).Value = "Họ Tên";
                worksheet.Cell(1, 3).Value = "Giới Tính";
                worksheet.Cell(1, 4).Value = "Ngày Sinh";
                worksheet.Cell(1, 5).Value = "Địa Chỉ";
                worksheet.Cell(1, 6).Value = "SĐT";
                worksheet.Cell(1, 7).Value = "Email";
                worksheet.Cell(1, 8).Value = "CCCD";
                worksheet.Cell(1, 9).Value = "Ngày Vào Làm";

                // Example data
                worksheet.Cell(2, 1).Value = "NV001";
                worksheet.Cell(2, 2).Value = "Nguyễn Văn A";
                worksheet.Cell(2, 3).Value = "Nam";
                worksheet.Cell(2, 4).Value = "01/01/1990";
                worksheet.Cell(2, 5).Value = "Hà Nội";
                worksheet.Cell(2, 6).Value = "0123456789";
                worksheet.Cell(2, 7).Value = "nva@example.com";
                worksheet.Cell(2, 8).Value = "001234567890";
                worksheet.Cell(2, 9).Value = "01/01/2020";

                // Style
                var headerRange = worksheet.Range(1, 1, 1, 9);
                headerRange.Style.Font.Bold = true;
                headerRange.Style.Fill.BackgroundColor = ClosedXML.Excel.XLColor.LightBlue;
                worksheet.Columns().AdjustToContents();

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var fileName = "Template_NhanVien.xlsx";
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                }
            }
        }
    }
}

