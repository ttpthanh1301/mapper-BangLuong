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
            [Display(Name = "Mã Bảng Lương")]
            public int MaBL { get; set; }

            [Display(Name = "Kỳ Lương (Tháng)")]
            public int KyLuongThang { get; set; }

            [Display(Name = "Kỳ Lương (Năm)")]
            public int KyLuongNam { get; set; }

            [Display(Name = "Lương Cơ Bản")]
            [DisplayFormat(DataFormatString = "{0:N0}")]
            public decimal LuongCoBan { get; set; }

            [Display(Name = "Tổng Phụ Cấp")]
            [DisplayFormat(DataFormatString = "{0:N0}")]
            public decimal? TongPhuCap { get; set; }

            [Display(Name = "Tổng Khen Thưởng")]
            [DisplayFormat(DataFormatString = "{0:N0}")]
            public decimal? TongKhenThuong { get; set; }

            [Display(Name = "Lương Tăng Ca")]
            [DisplayFormat(DataFormatString = "{0:N0}")]
            public decimal? LuongTangCa { get; set; }

            [Display(Name = "Tổng Thu Nhập")]
            [DisplayFormat(DataFormatString = "{0:N0}")]
            public decimal? TongThuNhap { get; set; }

            [Display(Name = "Giảm Trừ BHXH")]
            [DisplayFormat(DataFormatString = "{0:N0}")]
            public decimal? GiamTruBHXH { get; set; }

            [Display(Name = "Giảm Trừ BHYT")]
            [DisplayFormat(DataFormatString = "{0:N0}")]
            public decimal? GiamTruBHYT { get; set; }

            [Display(Name = "Giảm Trừ BHTN")]
            [DisplayFormat(DataFormatString = "{0:N0}")]
            public decimal? GiamTruBHTN { get; set; }

            [Display(Name = "Tổng Giảm Trừ Kỷ Luật")]
            [DisplayFormat(DataFormatString = "{0:N0}")]
            public decimal? TongGiamTruKyLuat { get; set; }

            [Display(Name = "Giảm Trừ Thuế TNCN")]
            [DisplayFormat(DataFormatString = "{0:N0}")]
            public decimal? GiamTruThueTNCN { get; set; }

            [Display(Name = "Thực Lãnh")]
            [DisplayFormat(DataFormatString = "{0:N0}")]
            public decimal ThucLanh { get; set; }

            [Display(Name = "Trạng Thái")]
            public string TrangThai { get; set; } = null!;

            [Display(Name = "Mã Nhân Viên")]
            public string MaNV { get; set; } = null!;

            [Display(Name = "Tên Nhân Viên")]
            public string? TenNhanVien { get; set; }
        }
    }
}
