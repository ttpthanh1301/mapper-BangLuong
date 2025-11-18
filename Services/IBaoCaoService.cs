using BangLuong.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using static BangLuong.ViewModels.NhanVienViewModels;

namespace BangLuong.Services.Interfaces
{
    public interface IBaoCaoService
    {
        // 1. Báo cáo nhân sự tổng hợp
        Task<List<BaoCaoNhanSuViewModel>> GetBaoCaoNhanSuTongHopAsync(); // tất cả
        Task<List<BaoCaoNhanSuViewModel>> GetBaoCaoNhanSuTongHopAsync(string phongBan); // lọc theo phòng ban

        // 2. Báo cáo tổng hợp chấm công
        Task<List<BaoCaoTongHopCongViewModel>> GetBaoCaoTongHopCongAsync(int thang, int nam);

        // 3. Báo cáo bảng lương chi tiết
        Task<List<BaoCaoBangLuongChiTietViewModel>> GetBaoCaoBangLuongChiTietAsync(int thang, int nam);

        // 4. Phiếu lương cá nhân
        Task<PhieuLuongCaNhanViewModel?> GetPhieuLuongCaNhanAsync(string maNV, int thang, int nam);

        // 5. Danh sách nhân viên
        Task<List<NhanVienViewModel>> GetDanhSachNhanVienAsync();

        // 6. Danh sách phòng ban
        Task<List<string>> GetDanhSachPhongBanAsync();
    }
}
