using System.Collections.Generic;
using System.Threading.Tasks;
using static BangLuong.ViewModels.DanhMucPhuCapViewModels;

public interface IDanhMucPhuCapService
{
    Task<IEnumerable<DanhMucPhuCapViewModel>> GetAllAsync();
    Task<DanhMucPhuCapViewModel?> GetByIdAsync(string id);
    Task CreateAsync(DanhMucPhuCapRequest request);
    Task UpdateAsync(DanhMucPhuCapViewModel viewModel);
    Task DeleteAsync(string id);
    Task<bool> ExistsAsync(string id);
}
