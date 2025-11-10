using BangLuong.ViewModels;

namespace BangLuong.Services.Interfaces
{
    public interface IExcelExportService
    {
        /// <summary>
        /// Xuất báo cáo danh sách nhân viên ra Excel
        /// </summary>
        byte[] ExportBaoCaoNhanSu(List<BaoCaoNhanSuViewModel> data);

        /// <summary>
        /// Xuất báo cáo tổng hợp công ra Excel
        /// </summary>
        byte[] ExportBaoCaoTongHopCong(List<BaoCaoTongHopCongViewModel> data, int thang, int nam);

        /// <summary>
        /// Xuất báo cáo bảng lương chi tiết ra Excel
        /// </summary>
        byte[] ExportBaoCaoBangLuong(List<BaoCaoBangLuongChiTietViewModel> data, int thang, int nam);

        /// <summary>
        /// Xuất phiếu lương cá nhân ra Excel
        /// </summary>
        byte[] ExportPhieuLuongCaNhan(PhieuLuongCaNhanViewModel data);
    }
}