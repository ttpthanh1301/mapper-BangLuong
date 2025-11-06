using System.Collections.Generic;
using System.Threading.Tasks;
using static BangLuong.ViewModels.ChiTietKyLuatViewModels;

namespace BangLuong.Services
{
    public interface IChiTietKyLuatService
    {
        Task<IEnumerable<ChiTietKyLuatViewModel>> GetAllAsync();
        Task<ChiTietKyLuatViewModel?> GetByIdAsync(int id);
        Task<bool> CreateAsync(ChiTietKyLuatRequest request);
        Task<bool> UpdateAsync(int id, ChiTietKyLuatViewModel request);
        Task<bool> DeleteAsync(int id);
    }
}
