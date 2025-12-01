using BangLuong.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static BangLuong.ViewModels.NhanVienViewModels;
using System;
using System.Threading.Tasks;

namespace BangLuong.Controllers
{
    [Authorize] // Tất cả actions yêu cầu đăng nhập
    public class BaoCaoController : Controller
    {
        private readonly IBaoCaoService _service;
        private readonly IExcelExportService _excelService;
        private readonly UserManager<NguoiDung> _userManager;

        public BaoCaoController(
            IBaoCaoService service,
            IExcelExportService excelService,
            UserManager<NguoiDung> userManager)
        {
            _service = service;
            _excelService = excelService;
            _userManager = userManager;
        }

        // ======================= INDEX =======================
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult Index()
        {
            return View();
        }

        // ======================= Báo cáo nhân sự tổng hợp =======================
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> NhanSuTongHop(string phongBan = "")
        {
            ViewBag.PhongBan = phongBan;

            // Lấy danh sách phòng ban để dropdown
            ViewBag.DanhSachPhongBan = await _service.GetDanhSachPhongBanAsync();

            // Lấy dữ liệu báo cáo, filter phòng ban nếu có
            var data = await _service.GetBaoCaoNhanSuTongHopAsync(phongBan);

            return View(data);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> ExportNhanSu(string phongBan = "")
        {
            var data = await _service.GetBaoCaoNhanSuTongHopAsync(phongBan);
            var fileBytes = _excelService.ExportBaoCaoNhanSu(data);
            
            var fileName = string.IsNullOrEmpty(phongBan) 
                ? $"BaoCao_DanhSachNhanVien_{DateTime.Now:yyyyMMdd}.xlsx"
                : $"BaoCao_DanhSachNhanVien_{phongBan}_{DateTime.Now:yyyyMMdd}.xlsx";
            
            return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }

        // ======================= 2. Báo cáo tổng hợp chấm công =======================
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> BaoCaoTongCong(int thang = 0, int nam = 0)
        {
            if (thang == 0) thang = DateTime.Now.Month;
            if (nam == 0) nam = DateTime.Now.Year;

            ViewBag.Thang = thang;
            ViewBag.Nam = nam;

            var model = await _service.GetBaoCaoTongHopCongAsync(thang, nam);
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> ExportBaoCaoTongCong(int thang, int nam)
        {
            var data = await _service.GetBaoCaoTongHopCongAsync(thang, nam);
            var fileBytes = _excelService.ExportBaoCaoTongHopCong(data, thang, nam);
            return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                       $"BaoCao_TongHopCong_{thang}_{nam}_{DateTime.Now:yyyyMMdd}.xlsx");
        }

        // ======================= 3. Báo cáo bảng lương chi tiết =======================
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> BangLuongChiTiet(int thang = 0, int nam = 0)
        {
            if (thang == 0) thang = DateTime.Now.Month;
            if (nam == 0) nam = DateTime.Now.Year;

            ViewBag.Thang = thang;
            ViewBag.Nam = nam;

            var model = await _service.GetBaoCaoBangLuongChiTietAsync(thang, nam);
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> ExportBangLuong(int thang, int nam)
        {
            var data = await _service.GetBaoCaoBangLuongChiTietAsync(thang, nam);
            var fileBytes = _excelService.ExportBaoCaoBangLuong(data, thang, nam);
            return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                       $"BaoCao_BangLuong_{thang}_{nam}_{DateTime.Now:yyyyMMdd}.xlsx");
        }

        // ======================= 4. Phiếu lương cá nhân =======================
        // ✅ FIX: Employee chỉ xem được phiếu lương của mình
        [Authorize(Roles = "Admin,Manager,Employee")]
        public async Task<IActionResult> PhieuLuongCaNhan(string maNV = "", int thang = 0, int nam = 0)
        {
            if (thang == 0) thang = DateTime.Now.Month;
            if (nam == 0) nam = DateTime.Now.Year;

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
                return RedirectToAction("Login", "NguoiDung");

            var userRoles = await _userManager.GetRolesAsync(currentUser);

            // ================= Employee chỉ xem phiếu của chính mình =================
            if (userRoles.Contains("Employee") && !userRoles.Contains("Admin") && !userRoles.Contains("Manager"))
            {
                maNV = currentUser.MaNV; // dùng MaNV thực sự của nhân viên
            }

            ViewBag.MaNV = maNV;
            ViewBag.Thang = thang;
            ViewBag.Nam = nam;

            // ================= Admin/Manager =================
            if (userRoles.Contains("Admin") || userRoles.Contains("Manager"))
            {
                var danhSachNhanVien = await _service.GetDanhSachNhanVienAsync();
                ViewBag.DanhSachNhanVien = danhSachNhanVien;

                // Nếu chưa chọn nhân viên thì chỉ hiển thị dropdown, không gọi dữ liệu
                if (string.IsNullOrEmpty(maNV))
                    return View();

                var adminModel = await _service.GetPhieuLuongCaNhanAsync(maNV, thang, nam);
                if (adminModel == null)
                {
                    TempData["ErrorMessage"] = "Không tìm thấy phiếu lương của nhân viên này!";
                    return View();
                }
                return View(adminModel);
            }

            // ================= Employee =================
            // Employee sẽ không hiển thị dropdown, chỉ xem phiếu của mình
            var employeeModel = await _service.GetPhieuLuongCaNhanAsync(maNV, thang, nam);
            if (employeeModel == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy phiếu lương của bạn!";
                return View();
            }

            return View(employeeModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Manager,Employee")]
        public async Task<IActionResult> ExportPhieuLuong(string maNV, int thang, int nam)
        {
            // ✅ FIX: Kiểm tra quyền - Employee chỉ export được phiếu của mình
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            if (currentUser == null)
            {
                return RedirectToAction("Login", "NguoiDung");
            }

            var userRoles = await _userManager.GetRolesAsync(currentUser);

            if (userRoles.Contains("Employee") && !userRoles.Contains("Admin") && !userRoles.Contains("Manager"))
            {
                // Employee chỉ được export phiếu của chính mình
                if (maNV != currentUser.Id)
                {
                    return Forbid(); // 403 Forbidden
                }
            }

            var data = await _service.GetPhieuLuongCaNhanAsync(maNV, thang, nam);
            if (data == null)
            {
                return NotFound();
            }

            var fileBytes = _excelService.ExportPhieuLuongCaNhan(data);
            return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                       $"PhieuLuong_{maNV}_{thang}_{nam}_{DateTime.Now:yyyyMMdd}.xlsx");
        }
    }
}