using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace BangLuong.ViewModels
{
    public class BaoCaoTongHopCongViewModel
    {
        public string? PhongBan { get; set; }
        public string? MaNV { get; set; }
        public string? HoTen { get; set; }
        public string? ChucVu { get; set; }

        [Precision(18, 2)]
        public decimal NgayCongChuan { get; set; }

        [Precision(18, 2)]
        public decimal? NgayCongThucTe { get; set; }

        [Precision(18, 2)]
        public decimal? SoNgayNghiPhep { get; set; }

        [Precision(18, 2)]
        public decimal? SoGioTangCaNgayThuong { get; set; }

        [Precision(18, 2)]
        public decimal? SoGioTangCaCuoiTuan { get; set; }

        [Precision(18, 2)]
        public decimal? SoGioTangCaNgayLe { get; set; }
    }
}
