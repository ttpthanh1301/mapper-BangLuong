using System;
using System.ComponentModel.DataAnnotations;

namespace BangLuong.ViewModels
{
    public class ChiTietKhenThuongViewModels
    {
        // Dùng cho tạo mới hoặc cập nhật
        public class ChiTietKhenThuongRequest
        {
            [Key]
            public int MaCTKT { get; set; }

            [Required(ErrorMessage = "Ngày khen thưởng không được để trống.")]
            [DataType(DataType.Date)]
            public DateTime NgayKhenThuong { get; set; }

            [StringLength(255, ErrorMessage = "Lý do không được vượt quá 255 ký tự.")]
            public string? LyDo { get; set; }

            [Required(ErrorMessage = "Mã khen thưởng không được để trống.")]
            [StringLength(10, ErrorMessage = "Mã khen thưởng không được vượt quá 10 ký tự.")]
            public string MaKT { get; set; } = null!;

            [Required(ErrorMessage = "Mã nhân viên không được để trống.")]
            [StringLength(15, ErrorMessage = "Mã nhân viên không được vượt quá 15 ký tự.")]
            public string MaNV { get; set; } = null!;
        }

        // Dùng cho hiển thị dữ liệu ra view
        public class ChiTietKhenThuongViewModel
        {
            public int MaCTKT { get; set; }
            [DataType(DataType.Date)]
            public DateTime NgayKhenThuong { get; set; }
            public string? LyDo { get; set; }
            public string MaKT { get; set; } = null!;
            public string MaNV { get; set; } = null!;
            public string? TenKhenThuong { get; set; }
            public string? TenNhanVien { get; set; }
        }
    }
}
