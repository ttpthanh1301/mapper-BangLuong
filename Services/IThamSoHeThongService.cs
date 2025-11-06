using System.Collections.Generic;
using System.Threading.Tasks;
using BangLuong.ViewModels;

namespace BangLuong.Services
{
    public interface IThamSoHeThongService
    {
        Task<IEnumerable<ThamSoHeThongViewModels.ThamSoHeThongViewModel>> GetAllAsync();
        Task<ThamSoHeThongViewModels.ThamSoHeThongViewModel?> GetByIdAsync(string id);
        Task<bool> CreateAsync(ThamSoHeThongViewModels.ThamSoHeThongRequest request);
        Task<bool> UpdateAsync(string id, ThamSoHeThongViewModels.ThamSoHeThongViewModel request);
        Task<bool> DeleteAsync(string id);
    }
}
