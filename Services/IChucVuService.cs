using System.Collections.Generic;
using System.Threading.Tasks;
using BangLuong.ViewModels;
using static BangLuong.ViewModels.ChucVuViewModels;

namespace BangLuong.Services
{
    public interface IChucVuService
    {
        Task<IEnumerable<ChucVuViewModels.ChucVuViewModel>> GetAllAsync();
        Task<ChucVuViewModels.ChucVuViewModel?> GetByIdAsync(string id);
        Task<bool> CreateAsync(ChucVuViewModels.ChucVuRequest request);
        Task<bool> UpdateAsync(string id, ChucVuViewModels.ChucVuViewModel viewModel);
        Task<bool> DeleteAsync(string id);
        Task<PaginatedList<ChucVuViewModel>> GetAllFilter(string sortOrder, string currentFilter, string searchString, int? pageNumber, int pageSize);
    }
}
