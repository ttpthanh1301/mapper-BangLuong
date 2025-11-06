using System.Collections.Generic;
using System.Threading.Tasks;
using static BangLuong.ViewModels.NguoiDungViewModels;

namespace BangLuong.Services
{
    public interface INguoiDungService
    {
        Task<IEnumerable<NguoiDungViewModel>> GetAllAsync();
        Task<NguoiDungViewModel?> GetByIdAsync(string id);
        Task<bool> CreateAsync(NguoiDungRequest request);
        Task<bool> UpdateAsync(string id, NguoiDungViewModel viewModel);
        Task<bool> DeleteAsync(string id);
        Task<IEnumerable<string>> GetAllNhanVienIdsAsync(); // dùng cho dropdown chọn nhân viên
    }
}
