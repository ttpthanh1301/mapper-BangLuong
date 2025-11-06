using System.Collections.Generic;
using System.Threading.Tasks;
using static BangLuong.ViewModels.ChamCongViewModels;

namespace BangLuong.Services
{
    public interface IChamCongService
    {
        Task<IEnumerable<ChamCongViewModel>> GetAll();
        Task<ChamCongViewModel> GetById(int id);
        Task<int> Create(ChamCongRequest request);
        Task<int> Update(ChamCongViewModel request);
        Task<int> Delete(int id);
    }
}
