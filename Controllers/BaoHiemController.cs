using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BangLuong.Services;
using static BangLuong.ViewModels.BaoHiemViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using static BangLuong.ViewModels.NhanVienViewModels; // Cần dùng NhanVienViewModels để lấy HoTen

namespace BangLuong.Controllers
{
    [Authorize(Roles = "Admin,Manager")] // Chỉ Admin, Manager mới được truy cập
    public class BaoHiemController : Controller
    {
        private readonly IBaoHiemService _service;
        private readonly INhanVienService _nhanVienService; // Dùng để lấy danh sách nhân viên cho dropdown

        public BaoHiemController(
            IBaoHiemService service,
            INhanVienService nhanVienService) 
        {
            _service = service;
            _nhanVienService = nhanVienService;
        }

        // Helper để tải danh sách nhân viên cho dropdown list (MaNV - HoTen)
        private async Task LoadNhanVienSelectList(string? selectedMaNV = null)
        {
             // Giả định INhanVienService.GetAll() trả về List<NhanVienViewModel>
             var nhanVienList = (await _nhanVienService.GetAll())?.ToList() ?? new List<NhanVienViewModel>(); 
             
             // Định dạng: "MaNV - HoTen" cho hiển thị
             var nhanVienSelectItems = nhanVienList.Select(n => new
             {
                 Value = n.MaNV,
                 Text = $"{n.MaNV} - {n.HoTen}" 
             }).ToList();
             
             ViewData["MaNV"] = new SelectList(nhanVienSelectItems, "Value", "Text", selectedMaNV);
        }

        // ======================= INDEX =======================
        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? pageNumber)
        {
            try
            {
                int pageSize = 10;
                var list = await _service.GetAllFilter(sortOrder, currentFilter, searchString, pageNumber, pageSize);

                ViewData["CurrentSort"] = sortOrder;
                ViewData["CurrentFilter"] = searchString;
                
                // Thiết lập các tham số sort
                ViewData["SSBHSortParm"] = sortOrder == "ssbh" ? "ssbh_desc" : "ssbh";
                ViewData["MaNVSortParm"] = sortOrder == "manv" ? "manv_desc" : "manv";

                return View(list);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Lỗi khi tải danh sách: {ex.Message}";
                // Trả về một PaginatedList rỗng khi có lỗi
                return View(PaginatedList<BaoHiemViewModel>.Create(new List<BaoHiemViewModel>(), 1, 10));
            }
        }

        // ======================= DETAILS =======================
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return BadRequest("Mã bảo hiểm không được để trống");

            try
            {
                var baoHiem = await _service.GetById(id.Value);
                if (baoHiem == null)
                    return NotFound("Không tìm thấy thông tin bảo hiểm");

                return View(baoHiem);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // ======================= CREATE GET =======================
        public async Task<IActionResult> Create()
        {
            await LoadNhanVienSelectList();
            return View();
        }

        // ======================= CREATE POST =======================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BaoHiemRequest request)
        {
            if (!ModelState.IsValid)
            {
                await LoadNhanVienSelectList(request.MaNV);
                return View(request);
            }

            try
            {
                await _service.Create(request);
                TempData["SuccessMessage"] = "Tạo thông tin bảo hiểm thành công!";
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex) // Bắt lỗi MaNV bị trùng từ Service
            {
                ModelState.AddModelError("MaNV", ex.Message); 
                await LoadNhanVienSelectList(request.MaNV);
                return View(request);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Lỗi khi tạo: " + ex.Message);
                await LoadNhanVienSelectList(request.MaNV);
                return View(request);
            }
        }

        // ======================= EDIT GET =======================
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return BadRequest("Mã bảo hiểm không được để trống");

            try
            {
                var baoHiem = await _service.GetById(id.Value);
                if (baoHiem == null)
                    return NotFound("Không tìm thấy thông tin bảo hiểm");
                
                await LoadNhanVienSelectList(baoHiem.MaNV);
                return View(baoHiem); 
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // ======================= EDIT POST =======================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BaoHiemViewModel request)
        {
            if (id != request.MaBH)
                return NotFound();

            if (!ModelState.IsValid)
            {
                await LoadNhanVienSelectList(request.MaNV);
                return View(request);
            }

            try
            {
                await _service.Update(request);
                TempData["SuccessMessage"] = "Cập nhật thông tin bảo hiểm thành công!";
                return RedirectToAction(nameof(Index));
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Không tìm thấy thông tin bảo hiểm để cập nhật.");
            }
            catch (Exception ex)
            {
                // Xử lý lỗi trùng lặp MaNV nếu xảy ra trong DB Update
                ModelState.AddModelError("", "Lỗi khi cập nhật: " + ex.Message);
                await LoadNhanVienSelectList(request.MaNV);
                return View(request);
            }
        }

        // ======================= DELETE GET =======================
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return BadRequest("Mã bảo hiểm không được để trống");

            try
            {
                var baoHiem = await _service.GetById(id.Value);
                if (baoHiem == null)
                    return NotFound("Không tìm thấy thông tin bảo hiểm");

                return View(baoHiem);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // ======================= DELETE POST =======================
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _service.Delete(id);
                TempData["SuccessMessage"] = "Xóa thông tin bảo hiểm thành công!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Lỗi khi xóa: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }
    }
}