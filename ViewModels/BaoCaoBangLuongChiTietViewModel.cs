using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace BangLuong.ViewModels
{
    public class BaoCaoBangLuongChiTietViewModel
    {
        [Display(Name = "Phòng Ban")]
        public string? PhongBan { get; set; }

        [Display(Name = "Mã Nhân Viên")]
        public string? MaNV { get; set; }

        [Display(Name = "Họ và Tên")]
        public string? HoTen { get; set; }

        [Display(Name = "Chức Vụ")]
        public string? ChucVu { get; set; }

        [Display(Name = "Lương Cơ Bản")]
        [Precision(18, 2)]
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public decimal LuongCoBan { get; set; }

        [Display(Name = "Ngày Công Thực Tế")]
        [Precision(18, 2)]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public decimal NgayCongThucTe { get; set; }

        [Display(Name = "Lương Thực Tế")]
        [Precision(18, 2)]
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public decimal LuongThucTe { get; set; }

        [Display(Name = "Tổng Phụ Cấp")]
        [Precision(18, 2)]
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public decimal TongPhuCap { get; set; }

        [Display(Name = "Lương Tăng Ca")]
        [Precision(18, 2)]
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public decimal LuongTangCa { get; set; }

        [Display(Name = "Tổng Thu Nhập")]
        [Precision(18, 2)]
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public decimal TongThuNhap { get; set; }

        [Display(Name = "Tổng Khấu Trừ")]
        [Precision(18, 2)]
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public decimal TongKhauTru { get; set; }

        [Display(Name = "Thực Lãnh")]
        [Precision(18, 2)]
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public decimal ThucLanh { get; set; }
    }
}
