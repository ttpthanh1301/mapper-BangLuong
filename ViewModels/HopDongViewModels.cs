using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BangLuong.ViewModels
{
    public class HopDongViewModels
    {
        public class HopDongRequest
        {
            [Key]
            [DisplayName("Mã Hợp Đồng")]
            public int MaHD { get; set; }

            [StringLength(50)]
            [DisplayName("Số Hợp Đồng")]
            public string? SoHopDong { get; set; }

            [Required]
            [StringLength(100)]
            [DisplayName("Loại Hợp Đồng")]
            public string LoaiHD { get; set; } = null!;

            [Required]
            [DataType(DataType.Date)]
            [DisplayName("Ngày Bắt Đầu")]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            public DateTime NgayBatDau { get; set; }

            [DataType(DataType.Date)]
            [DisplayName("Ngày Kết Thúc")]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            public DateTime? NgayKetThuc { get; set; }

            [Required]
            [Range(0, double.MaxValue, ErrorMessage = "Lương cơ bản phải lớn hơn hoặc bằng 0.")]
            [DisplayName("Lương Cơ Bản")]
            public decimal LuongCoBan { get; set; }

            [Range(0, double.MaxValue, ErrorMessage = "Phụ cấp ăn trưa phải lớn hơn hoặc bằng 0.")]
            [DisplayName("Phụ Cấp Ăn Trưa")]
            public decimal? PhuCapAnTrua { get; set; }

            [Range(0, double.MaxValue, ErrorMessage = "Phụ cấp xăng xe phải lớn hơn hoặc bằng 0.")]
            [DisplayName("Phụ Cấp Xăng Xe")]
            public decimal? PhuCapXangXe { get; set; }

            [Range(0, double.MaxValue, ErrorMessage = "Phụ cấp điện thoại phải lớn hơn hoặc bằng 0.")]
            [DisplayName("Phụ Cấp Điện Thoại")]
            public decimal? PhuCapDienThoai { get; set; }

            [Range(0, double.MaxValue, ErrorMessage = "Phụ cấp trách nhiệm phải lớn hơn hoặc bằng 0.")]
            [DisplayName("Phụ Cấp Trách Nhiệm")]
            public decimal? PhuCapTrachNhiem { get; set; }

            [Range(0, double.MaxValue, ErrorMessage = "Phụ cấp khác phải lớn hơn hoặc bằng 0.")]
            [DisplayName("Phụ Cấp Khác")]
            public decimal? PhuCapKhac { get; set; }

            [Required]
            [StringLength(50)]
            [DisplayName("Trạng Thái")]
            public string TrangThai { get; set; } = null!;

            [Required]
            [DisplayName("Mã Nhân Viên")]
            public string MaNV { get; set; } = null!;
        }

        public class HopDongViewModel
        {
            [DisplayName("Mã Hợp Đồng")]
            public int MaHD { get; set; }

            [DisplayName("Số Hợp Đồng")]
            public string? SoHopDong { get; set; }

            [DisplayName("Loại Hợp Đồng")]
            public string LoaiHD { get; set; } = null!;

            [DisplayName("Ngày Bắt Đầu")]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            public DateTime NgayBatDau { get; set; }

            [DisplayName("Ngày Kết Thúc")]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            public DateTime? NgayKetThuc { get; set; }

            [DisplayName("Lương Cơ Bản")]
            public decimal LuongCoBan { get; set; }

            [DisplayName("Phụ Cấp Ăn Trưa")]
            public decimal? PhuCapAnTrua { get; set; }

            [DisplayName("Phụ Cấp Xăng Xe")]
            public decimal? PhuCapXangXe { get; set; }

            [DisplayName("Phụ Cấp Điện Thoại")]
            public decimal? PhuCapDienThoai { get; set; }

            [DisplayName("Phụ Cấp Trách Nhiệm")]
            public decimal? PhuCapTrachNhiem { get; set; }

            [DisplayName("Phụ Cấp Khác")]
            public decimal? PhuCapKhac { get; set; }

            [DisplayName("Trạng Thái")]
            public string TrangThai { get; set; } = null!;

            [DisplayName("Mã Nhân Viên")]
            public string MaNV { get; set; } = null!;
        }
    }
}
