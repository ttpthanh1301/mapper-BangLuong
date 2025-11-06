using System.Collections.Generic;
using System.Threading.Tasks;
using BangLuong;
using static BangLuong.ViewModels.DanhMucPhuCapViewModels;

public interface IDanhMucPhuCapService
{
    Task<IEnumerable<DanhMucPhuCapViewModel>> GetAllAsync();
    Task<DanhMucPhuCapViewModel?> GetByIdAsync(string id);
    Task CreateAsync(DanhMucPhuCapRequest request);
    Task UpdateAsync(DanhMucPhuCapViewModel viewModel);
    Task DeleteAsync(string id);
    Task<bool> ExistsAsync(string id);
    Task<PaginatedList<DanhMucPhuCapViewModel>> GetAllFilter(string sortOrder, string currentFilter, string searchString, int? pageNumber, int pageSize);
}
