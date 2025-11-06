using System.Collections.Generic;
using System.Threading.Tasks;
using static BangLuong.ViewModels.PhongBanViewModels;

namespace BangLuong.Services
{
    public interface IPhongBanService
    {
        Task<IEnumerable<PhongBanViewModel>> GetAll();
        Task<PhongBanViewModel> GetById(string id);
        Task<int> Create(PhongBanRequest request);
        Task<int> Update(PhongBanViewModel request);
        Task<int> Delete(string id);
        Task<PaginatedList<PhongBanViewModel>> GetAllFilter(string sortOrder, string currentFilter, string searchString, int? pageNumber, int pageSize);
    }
}
