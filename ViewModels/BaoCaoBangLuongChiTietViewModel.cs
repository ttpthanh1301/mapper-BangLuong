using Microsoft.EntityFrameworkCore;

namespace BangLuong.ViewModels
{
    public class BaoCaoBangLuongChiTietViewModel
    {
        public string? PhongBan { get; set; }
        public string? MaNV { get; set; }
        public string? HoTen { get; set; }
        public string? ChucVu { get; set; }
        [Precision(18, 2)]
        public decimal LuongCoBan { get; set; }
        [Precision(18, 2)]
        public decimal NgayCongThucTe { get; set; }
        [Precision(18, 2)]
        public decimal LuongThucTe { get; set; }
        [Precision(18, 2)]
        public decimal TongPhuCap { get; set; }
        [Precision(18, 2)]
        public decimal LuongTangCa { get; set; }
        [Precision(18, 2)]
        public decimal TongThuNhap { get; set; }
        [Precision(18, 2)]
        public decimal TongKhauTru { get; set; }
        [Precision(18, 2)]
        public decimal ThucLanh { get; set; }
    }
}
