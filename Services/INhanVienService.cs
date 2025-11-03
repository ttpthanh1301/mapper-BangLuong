using static BangLuong.ViewModels.NhanVienViewModels;

namespace BangLuong.Services;

public interface INhanVienService
{
   
        Task<IEnumerable<NhanVienViewModel>> GetAll();
        Task<NhanVienViewModel> GetById(string id);
        Task<int> Create(NhanVienRequest request);
        Task<int> Update(NhanVienViewModel request);
        Task<int> Delete(string id);
        Task<PaginatedList<NhanVienViewModel>> GetAllFilter(string sortOrder, string currentFilter, string searchString, int? pageNumber, int pageSize);

}
