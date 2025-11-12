using BangLuong.Services;
using BangLuong.ViewModels;
using Microsoft.AspNetCore.Mvc;
using static BangLuong.ViewModels.NhanVienViewModels;

namespace BangLuong.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class NhanVienApiController : ControllerBase
    {
        private readonly INhanVienService _nhanVienService;

        public NhanVienApiController(INhanVienService nhanVienService)
        {
            _nhanVienService = nhanVienService;
        }

        // GET: api/NhanVien
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _nhanVienService.GetAllFilter("", "", "", null, 1000); // Lấy tất cả
            return Ok(list);
        }

        // GET: api/NhanVien/filter
        [HttpGet("filter")]
        public async Task<IActionResult> GetAllFilter(
            [FromQuery] string? sortOrder,
            [FromQuery] string? currentFilter,
            [FromQuery] string? searchString,
            [FromQuery] int? pageNumber,
            [FromQuery] int pageSize = 10)
        {
            var filtered = await _nhanVienService.GetAllFilter(
                sortOrder ?? "",
                currentFilter ?? "",
                searchString ?? "",
                pageNumber,
                pageSize);

            return Ok(filtered);
        }

        // GET: api/NhanVien/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            if (string.IsNullOrEmpty(id)) return BadRequest("Id không được trống.");

            var nhanVien = await _nhanVienService.GetById(id);
            if (nhanVien == null) return NotFound();

            return Ok(nhanVien);
        }

        // POST: api/NhanVien
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] NhanVienRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var created = await _nhanVienService.Create(request);
            return Ok(created);
        }

        // PUT: api/NhanVien
        [HttpPut]
        public async Task<IActionResult> Update([FromForm] NhanVienViewModel nhanVien)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updated = await _nhanVienService.Update(nhanVien);
            if (updated == 0) return NotFound();

            return Ok(updated > 0);
        }

        // DELETE: api/NhanVien/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id)) return BadRequest("Id không được trống.");

            var deleted = await _nhanVienService.Delete(id);
            return Ok(deleted);
        }

        // Export to Excel
        [HttpGet("export")]
        public async Task<IActionResult> ExportToExcel()
        {
            try
            {
                var fileBytes = await _nhanVienService.ExportToExcel();
                var fileName = $"DanhSachNhanVien_{DateTime.Now:yyyyMMddHHmmss}.xlsx";
                return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Lỗi khi export: {ex.Message}" });
            }
        }

        [HttpPost("import-file")]
        public async Task<IActionResult> ImportFromExcel([FromForm] ImportNhanVienRequest request)
        {
            if (request.File == null || request.File.Length == 0)
                return BadRequest("File is required");

            await _nhanVienService.ImportFromExcel(request.File);
            return Ok(new { message = "Import successful" });
        }


        // Download Template
        [HttpGet("template")]
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
