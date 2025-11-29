using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using static BangLuong.ViewModels.NguoiPhuThuocViewModels;

namespace BangLuong.Services
{
    public interface INguoiPhuThuocService
    {
        Task<PaginatedList<NguoiPhuThuocViewModel>> GetAllFilter(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? pageNumber,
            int pageSize);

        Task<IEnumerable<NguoiPhuThuocViewModel>> GetAllAsync();
        
        Task<NguoiPhuThuocViewModel?> GetByIdAsync(int id);
        
        Task<bool> CreateAsync(NguoiPhuThuocRequest request);
        
        Task<bool> UpdateAsync(int id, NguoiPhuThuocViewModel viewModel);
        
        Task<bool> DeleteAsync(int id);
        
        Task<IEnumerable<string>> GetAllNhanVienIdsAsync();
        
        // Thêm phương thức mới
        Task<IEnumerable<SelectListItem>> GetAllNhanVienSelectListAsync();
    }
}