using BangLuong.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BangLuong.Controllers
{
    public class BaoCaoController : Controller
    {
        private readonly IBaoCaoService _service;
        private readonly IExcelExportService _excelService;

        public BaoCaoController(IBaoCaoService service, IExcelExportService excelService)
        {
            _service = service;
            _excelService = excelService;
        }

        // Trang chính quản lý báo cáo
        public IActionResult Index()
        {
            return View();
        }

        // 1. Báo cáo nhân sự tổng hợp
        public async Task<IActionResult> NhanSuTongHop()
        {
            var model = await _service.GetBaoCaoNhanSuTongHopAsync();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ExportNhanSu()
        {
            var data = await _service.GetBaoCaoNhanSuTongHopAsync();
            var fileBytes = _excelService.ExportBaoCaoNhanSu(data);
            return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                       $"BaoCao_DanhSachNhanVien_{DateTime.Now:yyyyMMdd}.xlsx");
        }

        // 2. Báo cáo tổng hợp chấm công (ĐÃ ĐỔI TÊN ACTION)
        public async Task<IActionResult> BaoCaoTongCong(int thang = 0, int nam = 0)
        {
            // Set default values to current month/year if not provided
            if (thang == 0) thang = DateTime.Now.Month;
            if (nam == 0) nam = DateTime.Now.Year;

            ViewBag.Thang = thang;
            ViewBag.Nam = nam;

            var model = await _service.GetBaoCaoTongHopCongAsync(thang, nam);
            
            // Explicitly trả về View có tên "BaocaoTongcong"
          return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ExportBaoCaoTongCong(int thang, int nam) // ĐÃ ĐỔI TÊN ACTION
        {
            var data = await _service.GetBaoCaoTongHopCongAsync(thang, nam);
            var fileBytes = _excelService.ExportBaoCaoTongHopCong(data, thang, nam);
            return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                       $"BaoCao_TongHopCong_{thang}_{nam}_{DateTime.Now:yyyyMMdd}.xlsx");
        }

        // 3. Báo cáo bảng lương chi tiết
        public async Task<IActionResult> BangLuongChiTiet(int thang = 0, int nam = 0)
        {
            // Set default values to current month/year if not provided
            if (thang == 0) thang = DateTime.Now.Month;
            if (nam == 0) nam = DateTime.Now.Year;

            ViewBag.Thang = thang;
            ViewBag.Nam = nam;

            var model = await _service.GetBaoCaoBangLuongChiTietAsync(thang, nam);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ExportBangLuong(int thang, int nam)
        {
            var data = await _service.GetBaoCaoBangLuongChiTietAsync(thang, nam);
            var fileBytes = _excelService.ExportBaoCaoBangLuong(data, thang, nam);
            return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                       $"BaoCao_BangLuong_{thang}_{nam}_{DateTime.Now:yyyyMMdd}.xlsx");
        }

        // 4. Phiếu lương cá nhân
        public async Task<IActionResult> PhieuLuongCaNhan(string maNV = "", int thang = 0, int nam = 0)
        {
            // Set default values
            if (thang == 0) thang = DateTime.Now.Month;
            if (nam == 0) nam = DateTime.Now.Year;

            ViewBag.MaNV = maNV;
            ViewBag.Thang = thang;
            ViewBag.Nam = nam;

            if (string.IsNullOrEmpty(maNV))
            {
                return View();
            }

            var model = await _service.GetPhieuLuongCaNhanAsync(maNV, thang, nam);
            if (model == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy phiếu lương của nhân viên này!";
                return View();
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ExportPhieuLuong(string maNV, int thang, int nam)
        {
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