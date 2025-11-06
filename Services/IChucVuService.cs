using System.Collections.Generic;
using System.Threading.Tasks;
using BangLuong.ViewModels;

namespace BangLuong.Services
{
    public interface IChucVuService
    {
        Task<IEnumerable<ChucVuViewModels.ChucVuViewModel>> GetAllAsync();
        Task<ChucVuViewModels.ChucVuViewModel?> GetByIdAsync(string id);
        Task<bool> CreateAsync(ChucVuViewModels.ChucVuRequest request);
        Task<bool> UpdateAsync(string id, ChucVuViewModels.ChucVuViewModel viewModel);
        Task<bool> DeleteAsync(string id);
    }
}
