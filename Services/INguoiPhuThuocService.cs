using System.Collections.Generic;
using System.Threading.Tasks;
using static BangLuong.ViewModels.NguoiPhuThuocViewModels;

namespace BangLuong.Services
{
    public interface INguoiPhuThuocService
    {
        Task<IEnumerable<NguoiPhuThuocViewModel>> GetAllAsync();
        Task<NguoiPhuThuocViewModel?> GetByIdAsync(int id);
        Task<bool> CreateAsync(NguoiPhuThuocRequest request);
        Task<bool> UpdateAsync(int id, NguoiPhuThuocViewModel viewModel);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<string>> GetAllNhanVienIdsAsync(); // phục vụ dropdown MaNV
        Task<PaginatedList<NguoiPhuThuocViewModel>> GetAllFilter(string sortOrder, string currentFilter, string searchString, int? pageNumber, int pageSize);
    }
}
