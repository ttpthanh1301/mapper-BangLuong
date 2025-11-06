using System.Collections.Generic;
using System.Threading.Tasks;
using static BangLuong.ViewModels.HopDongViewModels;

namespace BangLuong.Services
{
    public interface IHopDongService
    {
        Task<IEnumerable<HopDongViewModel>> GetAll();
        Task<HopDongViewModel> GetById(int id);
        Task<int> Create(HopDongRequest request);
        Task<int> Update(HopDongViewModel request);
        Task<int> Delete(int id);
        Task<PaginatedList<HopDongViewModel>> GetAllFilter(string sortOrder, string currentFilter, string searchString, int? pageNumber, int pageSize);
    }
}
