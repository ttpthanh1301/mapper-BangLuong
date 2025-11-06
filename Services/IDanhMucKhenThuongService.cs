using System.Collections.Generic;
using System.Threading.Tasks;
using static BangLuong.ViewModels.DanhMucKhenThuongViewModels;

public interface IDanhMucKhenThuongService
{
    Task<IEnumerable<DanhMucKhenThuongViewModel>> GetAllAsync();
    Task<DanhMucKhenThuongViewModel?> GetByIdAsync(string id);
    Task CreateAsync(DanhMucKhenThuongRequest request);
    Task UpdateAsync(DanhMucKhenThuongViewModel viewModel);
    Task DeleteAsync(string id);
    Task<bool> ExistsAsync(string id);
}
