using BangLuong.Data;
using BangLuong.Services.Interfaces;
using BangLuong.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        // ======================= 0. Lấy danh sách nhân viên =======================
        public async Task<List<NhanVienViewModel>> GetDanhSachNhanVienAsync()
        {
            return await _context.NhanVien
                .Where(nv => nv.TrangThai == "Đang làm việc")
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

        // ======================= 1. Báo cáo nhân sự tổng hợp =======================
        public async Task<List<BaoCaoNhanSuViewModel>> GetBaoCaoNhanSuTongHopAsync()
        {
            return await _context.Set<BaoCaoNhanSuViewModel>()
                .FromSqlRaw("SELECT * FROM v_BaoCao_DanhSachNhanVien")
                .OrderBy(x => x.PhongBan)
                .ThenBy(x => x.HoTen)
                .ToListAsync();
        }

        public async Task<List<BaoCaoNhanSuViewModel>> GetBaoCaoNhanSuTongHopAsync(string phongBan)
        {
            var query = _context.Set<BaoCaoNhanSuViewModel>()
                                .FromSqlRaw("SELECT * FROM v_BaoCao_DanhSachNhanVien")
                                .AsQueryable();

            if (!string.IsNullOrEmpty(phongBan))
                query = query.Where(x => x.PhongBan == phongBan);

            return await query.OrderBy(x => x.PhongBan)
                              .ThenBy(x => x.HoTen)
                              .ToListAsync();
        }

        // ======================= 2. Báo cáo tổng hợp chấm công =======================
        public async Task<List<BaoCaoTongHopCongViewModel>> GetBaoCaoTongHopCongAsync(int thang, int nam)
        {
            return await _context.Set<BaoCaoTongHopCongViewModel>()
                .FromSqlRaw("EXEC sp_BaoCao_TongHopCong @Thang = {0}, @Nam = {1}", thang, nam)
                .ToListAsync();
        }

        // ======================= 3. Báo cáo bảng lương chi tiết =======================
        public async Task<List<BaoCaoBangLuongChiTietViewModel>> GetBaoCaoBangLuongChiTietAsync(int thang, int nam)
        {
            return await _context.Set<BaoCaoBangLuongChiTietViewModel>()
                .FromSqlRaw("EXEC sp_BaoCao_BangTinhLuongChiTiet @Thang = {0}, @Nam = {1}", thang, nam)
                .ToListAsync();
        }

        // ======================= 4. Phiếu lương cá nhân =======================
        public async Task<PhieuLuongCaNhanViewModel?> GetPhieuLuongCaNhanAsync(string maNV, int thang, int nam)
        {
            var list = await _context.Set<PhieuLuongCaNhanViewModel>()
                .FromSqlRaw(
                    "EXEC sp_BaoCao_PhieuLuongCaNhan @MaNV = {0}, @Thang = {1}, @Nam = {2}",
                    maNV, thang, nam)
                .ToListAsync();

            return list.SingleOrDefault(); // Lấy 1 phiếu hoặc null nếu không có
        }

        // ======================= 5. Danh sách phòng ban =======================
        public async Task<List<string>> GetDanhSachPhongBanAsync()
        {
            return await _context.NhanVien
                .Include(nv => nv.PhongBan)
                .Where(nv => nv.PhongBan != null)
                .Select(nv => nv.PhongBan!.TenPB)
                .Distinct()
                .OrderBy(pb => pb)
                .ToListAsync();

        }

    }
}
