using static BangLuong.ViewModels.BangTinhLuongViewModels;

namespace BangLuong.Services
{
    public interface IBangTinhLuongService
    {
        Task<PaginatedList<BangTinhLuongViewModel>> GetAllFilter(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? pageNumber,
            int pageSize);

        Task<IEnumerable<BangTinhLuongViewModel>> GetAllAsync();
        Task<BangTinhLuongViewModel?> GetByIdAsync(int id);
        Task<bool> CreateAsync(BangTinhLuongRequest request);
        Task<bool> UpdateAsync(int id, BangTinhLuongViewModel request);
        Task<bool> DeleteAsync(int id);

        // ✅ Đổi tên hàm đúng theo Controller
        Task<bool> TinhLuongTheoKyAsync(int kyLuongThang, int kyLuongNam);
    }
}
