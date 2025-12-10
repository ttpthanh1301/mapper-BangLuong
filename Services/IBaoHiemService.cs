using System.Collections.Generic;
using System.Threading.Tasks;
using static BangLuong.ViewModels.BaoHiemViewModels;

namespace BangLuong.Services
{
    public interface IBaoHiemService
    {
        Task<IEnumerable<BaoHiemViewModel>> GetAll();
        
        // ĐÃ SỬA: Thêm "?" để cho phép kiểu trả về là null, giải quyết cảnh báo CS8613.
        Task<BaoHiemViewModel?> GetById(int id);
        
        Task<int> Create(BaoHiemRequest request);
        Task<int> Update(BaoHiemViewModel request);
        Task<int> Delete(int id);
        Task<PaginatedList<BaoHiemViewModel>> GetAllFilter(string sortOrder, string currentFilter, string searchString, int? pageNumber, int pageSize);
    }
}