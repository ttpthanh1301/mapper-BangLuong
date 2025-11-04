using System;
using System.ComponentModel.DataAnnotations;

namespace BangLuong.ViewModels
{
    public class HopDongViewModels
    {
        public class HopDongRequest
        {
            [Key]
            public int MaHD { get; set; }

            [StringLength(50)]
            public string? SoHopDong { get; set; }

            [Required]
            [StringLength(100)]
            public string LoaiHD { get; set; } = null!;

            [Required]
            [DataType(DataType.Date)]
            public DateTime NgayBatDau { get; set; }

            [DataType(DataType.Date)]
            public DateTime? NgayKetThuc { get; set; }

            [Required]
            [Range(0, double.MaxValue, ErrorMessage = "Lương cơ bản phải lớn hơn hoặc bằng 0.")]
            public decimal LuongCoBan { get; set; }

            [Range(0, double.MaxValue, ErrorMessage = "Phụ cấp ăn trưa phải lớn hơn hoặc bằng 0.")]
            public decimal? PhuCapAnTrua { get; set; }

            [Range(0, double.MaxValue, ErrorMessage = "Phụ cấp xăng xe phải lớn hơn hoặc bằng 0.")]
            public decimal? PhuCapXangXe { get; set; }

            [Range(0, double.MaxValue, ErrorMessage = "Phụ cấp điện thoại phải lớn hơn hoặc bằng 0.")]
            public decimal? PhuCapDienThoai { get; set; }

            [Range(0, double.MaxValue, ErrorMessage = "Phụ cấp trách nhiệm phải lớn hơn hoặc bằng 0.")]
            public decimal? PhuCapTrachNhiem { get; set; }

            [Range(0, double.MaxValue, ErrorMessage = "Phụ cấp khác phải lớn hơn hoặc bằng 0.")]
            public decimal? PhuCapKhac { get; set; }

            [Required]
            [StringLength(50)]
            public string TrangThai { get; set; } = null!;

            [Required]
            public string MaNV { get; set; } = null!;
        }

        public class HopDongViewModel
        {
            public int MaHD { get; set; }
            public string? SoHopDong { get; set; }
            public string LoaiHD { get; set; } = null!;
            [DataType(DataType.Date)]
            public DateTime NgayBatDau { get; set; }
            [DataType(DataType.Date)]
            public DateTime? NgayKetThuc { get; set; }
            public decimal LuongCoBan { get; set; }
            public decimal? PhuCapAnTrua { get; set; }
            public decimal? PhuCapXangXe { get; set; }
            public decimal? PhuCapDienThoai { get; set; }
            public decimal? PhuCapTrachNhiem { get; set; }
            public decimal? PhuCapKhac { get; set; }
            public string TrangThai { get; set; } = null!;
            public string MaNV { get; set; } = null!;
        }
    }
}
