using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace BangLuong.ViewModels
{
    public class BaoCaoTongHopCongViewModel
    {
        [DisplayName("Phòng Ban")]
        public string? PhongBan { get; set; }

        [DisplayName("Mã Nhân Viên")]
        public string? MaNV { get; set; }

        [DisplayName("Họ và Tên")]
        public string? HoTen { get; set; }

        [DisplayName("Chức Vụ")]
        public string? ChucVu { get; set; }

        [DisplayName("Ngày Công Chuẩn")]
        [Precision(18, 2)]
        public decimal NgayCongChuan { get; set; }

        [DisplayName("Ngày Công Thực Tế")]
        [Precision(18, 2)]
        public decimal? NgayCongThucTe { get; set; }

        [DisplayName("Số Ngày Nghỉ Phép")]
        [Precision(18, 2)]
        public decimal? SoNgayNghiPhep { get; set; }

        [DisplayName("Số Giờ Tăng Ca Ngày Thường")]
        [Precision(18, 2)]
        public decimal? SoGioTangCaNgayThuong { get; set; }

        [DisplayName("Số Giờ Tăng Ca Cuối Tuần")]
        [Precision(18, 2)]
        public decimal? SoGioTangCaCuoiTuan { get; set; }

        [DisplayName("Số Giờ Tăng Ca Ngày Lễ")]
        [Precision(18, 2)]
        public decimal? SoGioTangCaNgayLe { get; set; }
    }
}
