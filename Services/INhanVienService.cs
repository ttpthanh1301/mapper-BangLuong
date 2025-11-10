using BangLuong.ViewModels;
using static BangLuong.ViewModels.NhanVienViewModels;
using Microsoft.AspNetCore.Http;

namespace BangLuong.Services
{
    public interface INhanVienService
    {
        Task<PaginatedList<NhanVienViewModel>> GetAllFilter(string sortOrder, string currentFilter, string searchString, int? pageNumber, int pageSize);
        Task<IEnumerable<NhanVienViewModel>> GetAll();
        Task<NhanVienViewModel> GetById(string id);
        Task<int> Create(NhanVienRequest request);
        Task<int> Update(NhanVienViewModel request);
        Task<int> Delete(string id);
        
        // Thêm methods cho import/export
        Task<byte[]> ExportToExcel();
        Task<(bool success, string message, int importedCount)> ImportFromExcel(IFormFile file);
    }
}