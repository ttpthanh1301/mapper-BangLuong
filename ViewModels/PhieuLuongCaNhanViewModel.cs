using Microsoft.EntityFrameworkCore;

namespace BangLuong.ViewModels
{
    public class PhieuLuongCaNhanViewModel
    {
        public string? MaNV { get; set; }
        public string? HoTen { get; set; }
        public string? PhongBan { get; set; }
        public string? ChucVu { get; set; }
        public int KyLuongThang { get; set; }
        public int KyLuongNam { get; set; }

        [Precision(18, 2)]
        public decimal LuongCoBanHopDong { get; set; }
        [Precision(18, 2)]
        public decimal NgayCongThucTe { get; set; }
        [Precision(18, 2)]
        public decimal LuongThucTe { get; set; }
        [Precision(18, 2)]
        public decimal TongPhuCap { get; set; }
        [Precision(18, 2)]
        public decimal LuongTangCa { get; set; }
        [Precision(18, 2)]
        public decimal TongKhenThuong { get; set; }
        [Precision(18, 2)]
        public decimal TongThuNhap_GROSS { get; set; }

        // Các khoản khấu trừ
        [Precision(18, 2)]
        public decimal BHXH { get; set; }
        [Precision(18, 2)]
        public decimal BHYT { get; set; }
        [Precision(18, 2)]
        public decimal BHTN { get; set; }
        [Precision(18, 2)]
        public decimal KyLuat { get; set; }

        // Thu nhập chịu thuế & giảm trừ
        [Precision(18, 2)]
        public decimal ThuNhapChiuThue { get; set; }
        [Precision(18, 2)]
        public decimal GiamTruBanThan { get; set; }
        public int SoNguoiPhuThuoc { get; set; }
        [Precision(18, 2)]
        public decimal GiamTruNguoiPhuThuoc { get; set; }
        [Precision(18, 2)]
        public decimal ThuNhapTinhThue { get; set; }

        [Precision(18, 2)]
        public decimal ThueTNCNPhaiNop { get; set; }
        [Precision(18, 2)]
        public decimal TongKhauTru { get; set; }
        [Precision(18, 2)]
        public decimal ThucLanh { get; set; }
    }
}
