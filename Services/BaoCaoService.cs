using BangLuong.Data;
using BangLuong.Services.Interfaces;
using BangLuong.ViewModels;
using Microsoft.EntityFrameworkCore;
using static BangLuong.ViewModels.NhanVienViewModels;

namespace BangLuong.Services.Implementations
{
    public class BaoCaoService : IBaoCaoService
    {
        private readonly BangLuongDbContext _context;

        public BaoCaoService(BangLuongDbContext context)
        {
            _context = context;
        }


 public async Task<List<NhanVienViewModel>> GetDanhSachNhanVienAsync()
{
    return await _context.NhanVien
        .Where(nv => nv.TrangThai == "Đang làm việc") // Hoặc điều kiện phù hợp
        .OrderBy(nv => nv.MaNV)
        .Select(nv => new NhanVienViewModel
        {
            MaNV = nv.MaNV,
            HoTen = nv.HoTen,
            MaPB = nv.MaPB,
            MaCV = nv.MaCV,
            TrangThai = nv.TrangThai
        })
        .ToListAsync();
}
        // 1. Báo cáo nhân sự tổng hợp (VIEW)
        public async Task<List<BaoCaoNhanSuViewModel>> GetBaoCaoNhanSuTongHopAsync()
        {
            var result = await _context
                .Set<BaoCaoNhanSuViewModel>()
                .FromSqlRaw("SELECT * FROM v_BaoCao_DanhSachNhanVien")
                .ToListAsync();

            return result;
        }

        // 2. Báo cáo tổng hợp chấm công
        public async Task<List<BaoCaoTongHopCongViewModel>> GetBaoCaoTongHopCongAsync(int thang, int nam)
        {
            // Map trực tiếp vào ViewModel
            var result = await _context.Set<BaoCaoTongHopCongViewModel>()
                .FromSqlRaw("EXEC sp_BaoCao_TongHopCong @Thang = {0}, @Nam = {1}", thang, nam)
                .ToListAsync();

            return result;
        }


        // 3. Báo cáo bảng lương chi tiết (Keyless entity)
        public async Task<List<BaoCaoBangLuongChiTietViewModel>> GetBaoCaoBangLuongChiTietAsync(int thang, int nam)
        {
            return await _context.Set<BaoCaoBangLuongChiTietViewModel>()
                .FromSqlRaw("EXEC sp_BaoCao_BangTinhLuongChiTiet @Thang = {0}, @Nam = {1}", thang, nam)
                .ToListAsync();
        }

        // 4. Phiếu lương cá nhân
        public async Task<PhieuLuongCaNhanViewModel?> GetPhieuLuongCaNhanAsync(string maNV, int thang, int nam)
        {
            var list = await _context.Set<PhieuLuongCaNhanViewModel>()
                .FromSqlRaw(
                    "EXEC sp_BaoCao_PhieuLuongCaNhan @MaNV = {0}, @Thang = {1}, @Nam = {2}",
                    maNV, thang, nam)
                .ToListAsync();  // Lấy toàn bộ dữ liệu async về client

            return list.SingleOrDefault();  // xử lý SingleOrDefault trên client
        }

    }
}
