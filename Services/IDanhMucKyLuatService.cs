using System.Collections.Generic;
using System.Threading.Tasks;
using static BangLuong.ViewModels.DanhMucKyLuatViewModels;

namespace BangLuong.Services
{
    public interface IDanhMucKyLuatService
    {
        Task<IEnumerable<DanhMucKyLuatViewModel>> GetAllAsync();
        Task<DanhMucKyLuatViewModel?> GetByIdAsync(string id);
        Task<bool> CreateAsync(DanhMucKyLuatRequest request);
        Task<bool> UpdateAsync(DanhMucKyLuatViewModel request);
        Task<bool> DeleteAsync(string id);
        Task<PaginatedList<DanhMucKyLuatViewModel>> GetAllFilter(string sortOrder, string currentFilter, string searchString, int? pageNumber, int pageSize);
    }
}
