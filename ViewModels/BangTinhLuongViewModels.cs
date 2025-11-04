using System;
using System.ComponentModel.DataAnnotations;

namespace BangLuong.ViewModels
{
    public class BangTinhLuongViewModels
    {
        public class BangTinhLuongRequest
        {
            [Key]
            public int MaBL { get; set; }

            [Required(ErrorMessage = "Kỳ lương (tháng) không được để trống.")]
            public int KyLuongThang { get; set; }

            [Required(ErrorMessage = "Kỳ lương (năm) không được để trống.")]
            public int KyLuongNam { get; set; }

            [Required(ErrorMessage = "Lương cơ bản không được để trống.")]
            [Range(0, double.MaxValue, ErrorMessage = "Lương cơ bản phải là số dương.")]
            public decimal LuongCoBan { get; set; }

            [Range(0, double.MaxValue, ErrorMessage = "Tổng phụ cấp phải là số dương.")]
            public decimal? TongPhuCap { get; set; }

            [Range(0, double.MaxValue, ErrorMessage = "Tổng khen thưởng phải là số dương.")]
            public decimal? TongKhenThuong { get; set; }

            [Range(0, double.MaxValue, ErrorMessage = "Lương tăng ca phải là số dương.")]
            public decimal? LuongTangCa { get; set; }

            [Range(0, double.MaxValue, ErrorMessage = "Tổng thu nhập phải là số dương.")]
            public decimal? TongThuNhap { get; set; }

            [Range(0, double.MaxValue, ErrorMessage = "Giảm trừ BHXH phải là số dương.")]
            public decimal? GiamTruBHXH { get; set; }

            [Range(0, double.MaxValue, ErrorMessage = "Giảm trừ BHYT phải là số dương.")]
            public decimal? GiamTruBHYT { get; set; }

            [Range(0, double.MaxValue, ErrorMessage = "Giảm trừ BHTN phải là số dương.")]
            public decimal? GiamTruBHTN { get; set; }

            [Range(0, double.MaxValue, ErrorMessage = "Tổng giảm trừ kỷ luật phải là số dương.")]
            public decimal? TongGiamTruKyLuat { get; set; }

            [Range(0, double.MaxValue, ErrorMessage = "Giảm trừ thuế TNCN phải là số dương.")]
            public decimal? GiamTruThueTNCN { get; set; }

            [Required(ErrorMessage = "Thực lãnh không được để trống.")]
            [Range(0, double.MaxValue, ErrorMessage = "Thực lãnh phải là số dương.")]
            public decimal ThucLanh { get; set; }

            [Required(ErrorMessage = "Trạng thái không được để trống.")]
            [StringLength(50, ErrorMessage = "Trạng thái không được vượt quá 50 ký tự.")]
            public string TrangThai { get; set; } = null!;

            [Required(ErrorMessage = "Mã nhân viên không được để trống.")]
            [StringLength(15, ErrorMessage = "Mã nhân viên không được vượt quá 15 ký tự.")]
            public string MaNV { get; set; } = null!;
        }

        public class BangTinhLuongViewModel
        {
            public int MaBL { get; set; }
            public int KyLuongThang { get; set; }
            public int KyLuongNam { get; set; }
            public decimal LuongCoBan { get; set; }
            public decimal? TongPhuCap { get; set; }
            public decimal? TongKhenThuong { get; set; }
            public decimal? LuongTangCa { get; set; }
            public decimal? TongThuNhap { get; set; }
            public decimal? GiamTruBHXH { get; set; }
            public decimal? GiamTruBHYT { get; set; }
            public decimal? GiamTruBHTN { get; set; }
            public decimal? TongGiamTruKyLuat { get; set; }
            public decimal? GiamTruThueTNCN { get; set; }
            public decimal ThucLanh { get; set; }
            public string TrangThai { get; set; } = null!;
            public string MaNV { get; set; } = null!;
            public string? TenNhanVien { get; set; }
        }
    }
}
