using System.Collections.Generic;
using System.Threading.Tasks;
using static BangLuong.ViewModels.ChiTietKhenThuongViewModels;

namespace BangLuong.Services
{
    public interface IChiTietKhenThuongService
    {
        Task<IEnumerable<ChiTietKhenThuongViewModel>> GetAllAsync();
        Task<ChiTietKhenThuongViewModel?> GetByIdAsync(int id);
        Task<bool> CreateAsync(ChiTietKhenThuongRequest request);
        Task<bool> UpdateAsync(int id, ChiTietKhenThuongViewModel request);
        Task<bool> DeleteAsync(int id);
        Task<PaginatedList<ChiTietKhenThuongViewModel>> GetAllFilter(string sortOrder, string currentFilter, string searchString, int? pageNumber, int pageSize);
        
    }
}
