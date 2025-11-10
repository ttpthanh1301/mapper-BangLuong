using System.Collections.Generic;
using System.Threading.Tasks;
using static BangLuong.ViewModels.TongHopCongViewModels;

namespace BangLuong.Services
{
    public interface ITongHopCongService
    {
        Task<IEnumerable<TongHopCongViewModel>> GetAllAsync();
        Task<TongHopCongViewModel?> GetByIdAsync(int id);
        Task<bool> CreateAsync(TongHopCongRequest request);
        Task<bool> UpdateAsync(int id, TongHopCongViewModel request);
        Task<bool> DeleteAsync(int id);
        Task<PaginatedList<TongHopCongViewModel>> GetAllFilter(string sortOrder, string currentFilter, string searchString, int? pageNumber, int pageSize);
        Task RunTongHopCongThangAsync(int kyLuongThang, int kyLuongNam);
    }
}
