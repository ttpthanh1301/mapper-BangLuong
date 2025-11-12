using System;
using System.ComponentModel;
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
            [DisplayName("Ngày Khen Thưởng")]
            public DateTime NgayKhenThuong { get; set; }

            [StringLength(255, ErrorMessage = "Lý do không được vượt quá 255 ký tự.")]
            [DisplayName("Lý Do")]
            public string? LyDo { get; set; }

            [Required(ErrorMessage = "Mã khen thưởng không được để trống.")]
            [StringLength(10, ErrorMessage = "Mã khen thưởng không được vượt quá 10 ký tự.")]
            [DisplayName("Mã Khen Thưởng")]
            public string MaKT { get; set; } = null!;

            [Required(ErrorMessage = "Mã nhân viên không được để trống.")]
            [StringLength(15, ErrorMessage = "Mã nhân viên không được vượt quá 15 ký tự.")]
            [DisplayName("Mã Nhân Viên")]
            public string MaNV { get; set; } = null!;
        }

        // Dùng cho hiển thị dữ liệu ra view
        public class ChiTietKhenThuongViewModel
        {
            [DisplayName("Mã Chi Tiết Khen Thưởng")]
            public int MaCTKT { get; set; }

            [DisplayName("Ngày Khen Thưởng")]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            public DateTime NgayKhenThuong { get; set; }

            [DisplayName("Lý Do")]
            public string? LyDo { get; set; }

            [DisplayName("Mã Khen Thưởng")]
            public string MaKT { get; set; } = null!;

            [DisplayName("Mã Nhân Viên")]
            public string MaNV { get; set; } = null!;

            [DisplayName("Tên Khen Thưởng")]
            public string? TenKhenThuong { get; set; }

            [DisplayName("Tên Nhân Viên")]
            public string? TenNhanVien { get; set; }
        }
    }
}
