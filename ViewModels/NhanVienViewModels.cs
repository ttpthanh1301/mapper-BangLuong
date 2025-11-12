using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BangLuong.Data.Entities;

namespace BangLuong.ViewModels
{
    public class NhanVienViewModels
    {
        public class NhanVienRequest
        {
            [Key]
            [StringLength(15)]
            [DisplayName("Mã Nhân Viên")]
            public string MaNV { get; set; } = null!;

            [Required, StringLength(100)]
            [DisplayName("Họ Tên")]
            public string HoTen { get; set; } = null!;

            [DataType(DataType.Date)]
            [DisplayName("Ngày Sinh")]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            public DateTime? NgaySinh { get; set; }

            [StringLength(10)]
            [DisplayName("Giới Tính")]
            public string? GioiTinh { get; set; }

            [StringLength(12)]
            [DisplayName("CCCD")]
            public string? CCCD { get; set; }

            [StringLength(255)]
            [DisplayName("Địa Chỉ")]
            public string? DiaChi { get; set; }

            [StringLength(15)]
            [DisplayName("Số Điện Thoại")]
            public string? SoDienThoai { get; set; }

            [StringLength(100)]
            [DisplayName("Email")]
            public string? Email { get; set; }

            [StringLength(13)]
            [DisplayName("Mã Số Thuế")]
            public string? MaSoThue { get; set; }

            [StringLength(20)]
            [DisplayName("Tài Khoản Ngân Hàng")]
            public string? TaiKhoanNganHang { get; set; }

            [StringLength(100)]
            [DisplayName("Tên Ngân Hàng")]
            public string? TenNganHang { get; set; }

            [Required, DataType(DataType.Date)]
            [DisplayName("Ngày Vào Làm")]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            public DateTime NgayVaoLam { get; set; }

            [Required, StringLength(50)]
            [DisplayName("Trạng Thái")]
            public string TrangThai { get; set; } = null!;

            [DisplayName("Mã Phòng Ban")]
            public string? MaPB { get; set; }

            [DisplayName("Mã Chức Vụ")]
            public string? MaCV { get; set; }
        }

        public class NhanVienViewModel
        {
            [DisplayName("Mã Nhân Viên")]
            public string MaNV { get; set; } = null!;

            [DisplayName("Họ Tên")]
            public string HoTen { get; set; } = null!;

            [DisplayName("Ngày Sinh")]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            public DateTime? NgaySinh { get; set; }

            [DisplayName("Giới Tính")]
            public string? GioiTinh { get; set; }

            [DisplayName("CCCD")]
            public string? CCCD { get; set; }

            [DisplayName("Địa Chỉ")]
            public string? DiaChi { get; set; }

            [DisplayName("Số Điện Thoại")]
            public string? SoDienThoai { get; set; }

            [DisplayName("Email")]
            public string? Email { get; set; }

            [DisplayName("Mã Số Thuế")]
            public string? MaSoThue { get; set; }

            [DisplayName("Tài Khoản Ngân Hàng")]
            public string? TaiKhoanNganHang { get; set; }

            [DisplayName("Tên Ngân Hàng")]
            public string? TenNganHang { get; set; }

            [DisplayName("Ngày Vào Làm")]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            public DateTime NgayVaoLam { get; set; }

            [DisplayName("Trạng Thái")]
            public string TrangThai { get; set; } = null!;

            [DisplayName("Mã Phòng Ban")]
            public string? MaPB { get; set; }

            [DisplayName("Mã Chức Vụ")]
            public string? MaCV { get; set; }
        }
        public class ImportNhanVienRequest
        {
            public IFormFile? File { get; set; }
        }
    }
}
