using System.Collections.Generic;
using System.Threading.Tasks;
using static BangLuong.ViewModels.ChiTietPhuCapViewModels;

namespace BangLuong.Services
{
    public interface IChiTietPhuCapService
    {
        Task<IEnumerable<ChiTietPhuCapViewModel>> GetAll();
        Task<ChiTietPhuCapViewModel> GetById(int id);
        Task<int> Create(ChiTietPhuCapRequest request);
        Task<int> Update(ChiTietPhuCapViewModel request);
        Task<int> Delete(int id);
    }
}
