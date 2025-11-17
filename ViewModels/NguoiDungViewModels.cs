using System.ComponentModel.DataAnnotations;

namespace BangLuong.ViewModels
{
    public class NguoiDungViewModels
    {
        public class NguoiDungRequest
        {
            [Required(ErrorMessage = "Mã nhân viên không được để trống")]
            [Display(Name = "Mã Nhân Viên")]
            public string MaNV { get; set; } = null!;

            [EmailAddress(ErrorMessage = "Email không hợp lệ")]
            [Display(Name = "Email")]
            public string? Email { get; set; }

            [StringLength(100, MinimumLength = 8, ErrorMessage = "Mật khẩu phải có ít nhất 8 ký tự")]
            [DataType(DataType.Password)]
            [Display(Name = "Mật Khẩu")]
            public string? Password { get; set; }

            [Display(Name = "Phân Quyền")]
            public string? PhanQuyen { get; set; }

            [Display(Name = "Trạng Thái")]
            public string? TrangThai { get; set; }
        }

        public class NguoiDungViewModel
        {
            public string MaNV { get; set; } = null!;
            public string? Email { get; set; }
            public string? UserName { get; set; }
            public string? PhanQuyen { get; set; }
            public string? TrangThai { get; set; }
        }

        // ✅ LoginRequest dùng MaNV (Mã Nhân Viên)
        public class LoginRequest
        {
            [Required(ErrorMessage = "Mã nhân viên không được để trống")]
            [Display(Name = "Mã Nhân Viên")]
            public string MaNV { get; set; } = null!;

            [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
            [DataType(DataType.Password)]
            [Display(Name = "Mật Khẩu")]
            public string Password { get; set; } = null!;

            [Display(Name = "Ghi nhớ đăng nhập")]
            public bool RememberMe { get; set; }

            // ReturnUrl - không bắt buộc
            public string? ReturnUrl { get; set; }
        }
    }
}