using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BangLuong.Data.Entities;

namespace BangLuong.ViewModels;

public class NhanVienViewModels
{
    public class NhanVienRequest
    {

        [Key]
        [StringLength(15)]
        public string MaNV { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string HoTen { get; set; } = null!;

        [DataType(DataType.Date)]
        public DateTime? NgaySinh { get; set; }

        [StringLength(10)]
        public string? GioiTinh { get; set; }

        [StringLength(12)]
        public string? CCCD { get; set; }

        [StringLength(255)]
        public string? DiaChi { get; set; }

        [StringLength(15)]
        public string? SoDienThoai { get; set; }
        [StringLength(100)]
        public string? Email { get; set; }

        [StringLength(13)]
        public string? MaSoThue { get; set; }

        [StringLength(20)]
        public string? TaiKhoanNganHang { get; set; }

        [StringLength(100)]
        public string? TenNganHang { get; set; }

        [Required, DataType(DataType.Date)]
        public DateTime NgayVaoLam { get; set; }

        [Required, StringLength(50)]
        public string TrangThai { get; set; } = null!;
        public string? MaPB { get; set; }
        public string? MaCV { get; set; }
    }
    public class NhanVienViewModel
    {

        public string MaNV { get; set; } = null!;
        public string HoTen { get; set; } = null!;
        [DataType(DataType.Date)]
        public DateTime? NgaySinh { get; set; }
        public string? GioiTinh { get; set; }
        public string? CCCD { get; set; }
        public string? DiaChi { get; set; }
        public string? SoDienThoai { get; set; }
        public string? Email { get; set; }
        public string? MaSoThue { get; set; }
        public string? TaiKhoanNganHang { get; set; }
        public string? TenNganHang { get; set; }

        [DataType(DataType.Date)]
        public DateTime NgayVaoLam { get; set; }
        public string TrangThai { get; set; } = null!;
        public string? MaPB { get; set; }
        public string? MaCV { get; set; }
    }
}
