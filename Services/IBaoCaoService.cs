using BangLuong.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BangLuong.Services.Interfaces
{
    public interface IBaoCaoService
    {
        Task<List<BaoCaoNhanSuViewModel>> GetBaoCaoNhanSuTongHopAsync();
        Task<List<BaoCaoTongHopCongViewModel>> GetBaoCaoTongHopCongAsync(int thang, int nam);
        Task<List<BaoCaoBangLuongChiTietViewModel>> GetBaoCaoBangLuongChiTietAsync(int thang, int nam);
        Task<PhieuLuongCaNhanViewModel?> GetPhieuLuongCaNhanAsync(string maNV, int thang, int nam);
    }
}
