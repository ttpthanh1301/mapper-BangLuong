using BangLuong.ViewModels;

namespace BangLuong.Services
{
    public interface INguoiDungService
    {
        // Đăng nhập và trả về JWT token
        Task<string> Authenticate(NguoiDungViewModels.LoginRequest request);

        // Đăng ký người dùng mới
        Task<bool> Register(NguoiDungViewModels.NguoiDungRequest request);

        // Lấy danh sách tất cả người dùng
        Task<IEnumerable<NguoiDungViewModels.NguoiDungViewModel>> GetAll();

        // Lấy thông tin người dùng theo MaNV
        Task<NguoiDungViewModels.NguoiDungViewModel?> GetById(string maNV);

        // Cập nhật thông tin người dùng
        Task<bool> Update(NguoiDungViewModels.NguoiDungRequest request);

        // Xóa người dùng
        Task<bool> Delete(string maNV);
    }
}
