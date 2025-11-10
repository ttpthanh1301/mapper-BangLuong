using System.Threading.Tasks;
using BangLuong.ViewModels;

namespace BangLuong.Services
{
    public interface IDashboardService
    {
        Task<DashboardViewModel> GetDashboardDataAsync();
    }
}
