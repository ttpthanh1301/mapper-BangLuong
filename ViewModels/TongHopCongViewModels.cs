using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BangLuong.ViewModels
{
    public class TongHopCongViewModels
    {
        public class TongHopCongRequest
        {
            [Key]
            [DisplayName("Mã Tổng Hợp Công")]
            public int MaTHC { get; set; }

            [Required(ErrorMessage = "Kỳ lương (tháng) không được để trống.")]
            [DisplayName("Kỳ Lương (Tháng)")]
            public int KyLuongThang { get; set; }

            [Required(ErrorMessage = "Kỳ lương (năm) không được để trống.")]
            [DisplayName("Kỳ Lương (Năm)")]
            public int KyLuongNam { get; set; }

            [Required(ErrorMessage = "Số ngày công không được để trống.")]
            [Range(0, double.MaxValue, ErrorMessage = "Số ngày công phải là số dương.")]
            [DisplayName("Số Ngày Công")]
            public decimal SoNgayCong { get; set; }

            [Range(0, double.MaxValue, ErrorMessage = "Số giờ tăng ca ngày thường phải là số dương.")]
            [DisplayName("Số Giờ Tăng Ca Ngày Thường")]
            public decimal? SoGioTangCaNgayThuong { get; set; }

            [Range(0, double.MaxValue, ErrorMessage = "Số giờ tăng ca cuối tuần phải là số dương.")]
            [DisplayName("Số Giờ Tăng Ca Cuối Tuần")]
            public decimal? SoGioTangCaCuoiTuan { get; set; }

            [Range(0, double.MaxValue, ErrorMessage = "Số giờ tăng ca ngày lễ phải là số dương.")]
            [DisplayName("Số Giờ Tăng Ca Ngày Lễ")]
            public decimal? SoGioTangCaNgayLe { get; set; }

            [Range(0, int.MaxValue, ErrorMessage = "Số ngày nghỉ phép phải là số không âm.")]
            [DisplayName("Số Ngày Nghỉ Phép")]
            public int? SoNgayNghiPhep { get; set; }

            [Required(ErrorMessage = "Mã nhân viên không được để trống.")]
            [StringLength(15, ErrorMessage = "Mã nhân viên không được vượt quá 15 ký tự.")]
            [DisplayName("Mã Nhân Viên")]
            public string MaNV { get; set; } = null!;
        }

        public class TongHopCongViewModel
        {
            [DisplayName("Mã Tổng Hợp Công")]
            public int MaTHC { get; set; }

            [DisplayName("Kỳ Lương (Tháng)")]
            public int KyLuongThang { get; set; }

            [DisplayName("Kỳ Lương (Năm)")]
            public int KyLuongNam { get; set; }

            [DisplayName("Số Ngày Công")]
            public decimal SoNgayCong { get; set; }

            [DisplayName("Số Giờ Tăng Ca Ngày Thường")]
            public decimal? SoGioTangCaNgayThuong { get; set; }

            [DisplayName("Số Giờ Tăng Ca Cuối Tuần")]
            public decimal? SoGioTangCaCuoiTuan { get; set; }

            [DisplayName("Số Giờ Tăng Ca Ngày Lễ")]
            public decimal? SoGioTangCaNgayLe { get; set; }

            [DisplayName("Số Ngày Nghỉ Phép")]
            public int? SoNgayNghiPhep { get; set; }

            [DisplayName("Mã Nhân Viên")]
            public string MaNV { get; set; } = null!;

            [DisplayName("Tên Nhân Viên")]
            public string? TenNhanVien { get; set; }
        }
    }
}
