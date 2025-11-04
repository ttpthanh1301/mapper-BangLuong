using System;
using System.ComponentModel.DataAnnotations;

namespace BangLuong.ViewModels
{
    public class TongHopCongViewModels
    {
        public class TongHopCongRequest
        {
            [Key]
            public int MaTHC { get; set; }

            [Required(ErrorMessage = "Kỳ lương (tháng) không được để trống.")]
            public int KyLuongThang { get; set; }

            [Required(ErrorMessage = "Kỳ lương (năm) không được để trống.")]
            public int KyLuongNam { get; set; }

            [Required(ErrorMessage = "Số ngày công không được để trống.")]
            [Range(0, double.MaxValue, ErrorMessage = "Số ngày công phải là số dương.")]
            public decimal SoNgayCong { get; set; }

            [Range(0, double.MaxValue, ErrorMessage = "Số giờ tăng ca ngày thường phải là số dương.")]
            public decimal? SoGioTangCaNgayThuong { get; set; }

            [Range(0, double.MaxValue, ErrorMessage = "Số giờ tăng ca cuối tuần phải là số dương.")]
            public decimal? SoGioTangCaCuoiTuan { get; set; }

            [Range(0, double.MaxValue, ErrorMessage = "Số giờ tăng ca ngày lễ phải là số dương.")]
            public decimal? SoGioTangCaNgayLe { get; set; }

            [Range(0, int.MaxValue, ErrorMessage = "Số ngày nghỉ phép phải là số không âm.")]
            public int? SoNgayNghiPhep { get; set; }

            [Required(ErrorMessage = "Mã nhân viên không được để trống.")]
            [StringLength(15, ErrorMessage = "Mã nhân viên không được vượt quá 15 ký tự.")]
            public string MaNV { get; set; } = null!;
        }

        public class TongHopCongViewModel
        {
            public int MaTHC { get; set; }
            public int KyLuongThang { get; set; }
            public int KyLuongNam { get; set; }
            public decimal SoNgayCong { get; set; }
            public decimal? SoGioTangCaNgayThuong { get; set; }
            public decimal? SoGioTangCaCuoiTuan { get; set; }
            public decimal? SoGioTangCaNgayLe { get; set; }
            public int? SoNgayNghiPhep { get; set; }
            public string MaNV { get; set; } = null!;
            public string? TenNhanVien { get; set; }
        }
    }
}
