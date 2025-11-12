using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BangLuong.ViewModels
{
    public class NguoiDungViewModels
    {
        // Dùng cho create/update user
        public class NguoiDungRequest
        {
            [Key, StringLength(50)]
            [DisplayName("Mã Nhân Viên")]
            public string MaNV { get; set; } = null!; // tương đương Id trong IdentityUser

            [Required, StringLength(255)]
            [DataType(DataType.Password)]
            [DisplayName("Mật Khẩu")]
            public string Password { get; set; } = null!;

            [Required, StringLength(50)]
            [DisplayName("Phân Quyền")]
            public string PhanQuyen { get; set; } = null!;

            [Required, StringLength(50)]
            [DisplayName("Trạng Thái")]
            public string TrangThai { get; set; } = null!;

            [Required, EmailAddress]
            [DisplayName("Email")]
            public string Email { get; set; } = null!;

            [Phone]
            [DisplayName("Số Điện Thoại")]
            public string? PhoneNumber { get; set; }
        }

        // Dùng để hiển thị thông tin người dùng
        public class NguoiDungViewModel
        {
            [DisplayName("Mã Nhân Viên")]
            public string MaNV { get; set; } = null!;

            [DisplayName("Phân Quyền")]
            public string PhanQuyen { get; set; } = null!;

            [DisplayName("Trạng Thái")]
            public string TrangThai { get; set; } = null!;

            [DisplayName("Email")]
            public string Email { get; set; } = null!;

            [DisplayName("Số Điện Thoại")]
            public string? PhoneNumber { get; set; }
        }

        // Dùng cho đăng nhập
        public class LoginRequest
        {
            [Required, EmailAddress]
            [DisplayName("Email")]
            public string Email { get; set; } = null!;

            [Required, DataType(DataType.Password)]
            [DisplayName("Mật Khẩu")]
            public string Password { get; set; } = null!;

            [DisplayName("Ghi nhớ đăng nhập?")]
            public bool RememberMe { get; set; } = false;
        }
    }
}
