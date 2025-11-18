using AutoMapper;
using BangLuong.Data;
using BangLuong.Data.Entities;
using BangLuong.ViewModels;
using ClosedXML.Excel;
using Microsoft.EntityFrameworkCore;
using static BangLuong.ViewModels.NhanVienViewModels;

namespace BangLuong.Services;

public class NhanVienService : INhanVienService
{
    private readonly BangLuongDbContext _context;
    private readonly IMapper _mapper;
    
    public NhanVienService(BangLuongDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    // ✅ SỬA LẠI: Dùng IQueryable để phân trang hiệu quả
public async Task<PaginatedList<NhanVienViewModel>> GetAllFilter(
    string sortOrder,
    string currentFilter,
    string searchString,
    int? pageNumber,
    int pageSize)
{
    // Join với PhongBan và ChucVu để lấy tên
    var query = from nv in _context.NhanVien
                join pb in _context.PhongBan on nv.MaPB equals pb.MaPB into pbGroup
                from pb in pbGroup.DefaultIfEmpty()
                join cv in _context.ChucVu on nv.MaCV equals cv.MaCV into cvGroup
                from cv in cvGroup.DefaultIfEmpty()
                select new NhanVienViewModel
                {
                    MaNV = nv.MaNV,
                    HoTen = nv.HoTen,
                    NgaySinh = nv.NgaySinh,
                    GioiTinh = nv.GioiTinh,
                    DiaChi = nv.DiaChi,
                    SoDienThoai = nv.SoDienThoai,
                    Email = nv.Email,
                    CCCD = nv.CCCD,
                    MaSoThue = nv.MaSoThue,
                    TaiKhoanNganHang = nv.TaiKhoanNganHang,
                    TenNganHang = nv.TenNganHang,
                    NgayVaoLam = nv.NgayVaoLam,
                    TrangThai = nv.TrangThai,
                    MaPB = nv.MaPB,
                    TenPhongBan = pb != null ? pb.TenPB : null,
                    MaCV = nv.MaCV,
                    TenChucVu = cv != null ? cv.TenCV : null
                };

    // Filter
    if (!string.IsNullOrEmpty(searchString))
    {
        query = query.Where(s =>
            s.HoTen.Contains(searchString) ||
            s.MaNV.Contains(searchString) ||
            (s.CCCD != null && s.CCCD.Contains(searchString))
        );
    }

    // Sorting
    query = sortOrder switch
    {
        "name_desc" => query.OrderByDescending(s => s.HoTen),
        "gender" => query.OrderBy(s => s.GioiTinh),
        "gender_desc" => query.OrderByDescending(s => s.GioiTinh),
        "date" => query.OrderBy(s => s.NgayVaoLam),
        "date_desc" => query.OrderByDescending(s => s.NgayVaoLam),
        _ => query.OrderBy(s => s.HoTen),
    };

    // Phân trang
    return await PaginatedList<NhanVienViewModel>.CreateAsync(
        query.AsQueryable(),
        pageNumber ?? 1,
        pageSize
    );
}


    public async Task<int> Create(NhanVienViewModels.NhanVienRequest request)
    {
        var nhanVien = _mapper.Map<NhanVien>(request);
        _context.NhanVien.Add(nhanVien);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> Delete(string id)
    {
        var nhanVien = await _context.NhanVien.FindAsync(id);
        if (nhanVien == null)
        {
            throw new Exception("Nhân viên không tồn tại");
        }
        _context.NhanVien.Remove(nhanVien);
        return await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<NhanVienViewModels.NhanVienViewModel>> GetAll()
    {
        var nhanViens = await _context.NhanVien.ToListAsync();
        return _mapper.Map<IEnumerable<NhanVienViewModels.NhanVienViewModel>>(nhanViens);
    }

    public async Task<NhanVienViewModels.NhanVienViewModel> GetById(string id)
    {
        var nhanVien = await _context.NhanVien
            .FirstOrDefaultAsync(m => m.MaNV == id);
        if (nhanVien == null)
        {
            throw new Exception("Nhân viên không tồn tại");
        }
        return _mapper.Map<NhanVienViewModel>(nhanVien);
    }

    public async Task<int> Update(NhanVienViewModels.NhanVienViewModel request)
    {
        if (!NhanVienExists(request.MaNV))
        {
            throw new Exception("Nhân viên không tồn tại");
        }
        _context.NhanVien.Update(_mapper.Map<NhanVien>(request));
        return await _context.SaveChangesAsync();
    }

    private bool NhanVienExists(string id)
    {
        return _context.NhanVien.Any(e => e.MaNV == id);
    }

    public async Task<byte[]> ExportToExcel()
    {
        var nhanViens = await _context.NhanVien.ToListAsync();

        using (var workbook = new XLWorkbook())
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

            // Style header
            var headerRange = worksheet.Range(1, 1, 1, 9);
            headerRange.Style.Font.Bold = true;
            headerRange.Style.Fill.BackgroundColor = XLColor.LightBlue;
            headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            // Data
            int row = 2;
            foreach (var nv in nhanViens)
            {
                worksheet.Cell(row, 1).Value = nv.MaNV;
                worksheet.Cell(row, 2).Value = nv.HoTen;
                worksheet.Cell(row, 3).Value = nv.GioiTinh;
                worksheet.Cell(row, 4).Value = nv.NgaySinh?.ToString("dd/MM/yyyy");
                worksheet.Cell(row, 5).Value = nv.DiaChi;
                worksheet.Cell(row, 6).Value = nv.SoDienThoai;
                worksheet.Cell(row, 7).Value = nv.Email;
                worksheet.Cell(row, 8).Value = nv.CCCD;
                worksheet.Cell(row, 9).Value = nv.NgayVaoLam.ToString("dd/MM/yyyy");
                row++;
            }

            // Auto-fit columns
            worksheet.Columns().AdjustToContents();

            // Convert to byte array
            using (var stream = new MemoryStream())
            {
                workbook.SaveAs(stream);
                return stream.ToArray();
            }
        }
    }

    public async Task<(bool success, string message, int importedCount)> ImportFromExcel(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return (false, "File không hợp lệ", 0);
        }
        
        if (!file.FileName.EndsWith(".xlsx") && !file.FileName.EndsWith(".xls"))
        {
            return (false, "Chỉ chấp nhận file Excel (.xlsx, .xls)", 0);
        }
        
        try
        {
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                using (var workbook = new XLWorkbook(stream))
                {
                    var worksheet = workbook.Worksheet(1);
                    var rows = worksheet.RangeUsed().RowsUsed().Skip(1); // Skip header
                    
                    int importedCount = 0;
                    var errors = new List<string>();
                    
                    foreach (var row in rows)
                    {
                        try
                        {
                            var maNV = row.Cell(1).GetValue<string>();
                            
                            // Kiểm tra nếu mã NV đã tồn tại
                            var existingNV = await _context.NhanVien.FindAsync(maNV);
                            
                            var nhanVien = existingNV ?? new NhanVien { MaNV = maNV };
                            
                            nhanVien.HoTen = row.Cell(2).GetValue<string>();
                            nhanVien.GioiTinh = row.Cell(3).GetValue<string>();
                            
                            // Parse NgaySinh
                            var ngaySinhStr = row.Cell(4).GetValue<string>();
                            if (DateTime.TryParseExact(ngaySinhStr, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime ngaySinh))
                            {
                                nhanVien.NgaySinh = ngaySinh;
                            }
                            
                            nhanVien.DiaChi = row.Cell(5).GetValue<string>();
                            nhanVien.SoDienThoai = row.Cell(6).GetValue<string>();
                            nhanVien.Email = row.Cell(7).GetValue<string>();
                            nhanVien.CCCD = row.Cell(8).GetValue<string>();
                            
                            // Parse NgayVaoLam
                            var ngayVaoLamStr = row.Cell(9).GetValue<string>();
                            if (DateTime.TryParseExact(ngayVaoLamStr, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime ngayVaoLam))
                            {
                                nhanVien.NgayVaoLam = ngayVaoLam;
                            }
                            
                            if (existingNV == null)
                            {
                                _context.NhanVien.Add(nhanVien);
                            }
                            else
                            {
                                _context.NhanVien.Update(nhanVien);
                            }
                            
                            importedCount++;
                        }
                        catch (Exception ex)
                        {
                            errors.Add($"Lỗi dòng {row.RowNumber()}: {ex.Message}");
                        }
                    }
                    
                    await _context.SaveChangesAsync();
                    
                    if (errors.Any())
                    {
                        return (true, $"Import thành công {importedCount} nhân viên. Có {errors.Count} lỗi: {string.Join(", ", errors)}", importedCount);
                    }
                    
                    return (true, $"Import thành công {importedCount} nhân viên", importedCount);
                }
            }
        }
        catch (Exception ex)
        {
            return (false, $"Lỗi khi import: {ex.Message}", 0);
        }
    }
}