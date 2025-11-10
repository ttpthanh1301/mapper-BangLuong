using Microsoft.EntityFrameworkCore;

namespace BangLuong.ViewModels
{
    public class BaoCaoNhanSuViewModel
    {
        // Các tên này hiện khớp với tên cột mới trong View SQL
        public string ?MaNV { get; set; }
        public string? HoTen { get; set; }
        public string ?GioiTinh { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string ?PhongBan { get; set; } 
        public string ?ChucVu { get; set; } // Khớp với cột ChucVu trong SQL
        public DateTime? NgayVaoLam { get; set; }
                [Precision(18, 2)]
        public decimal? ThamNienNam { get; set; }
        public string? LoaiHopDong { get; set; }
        public string? TrangThaiHopDong { get; set; }
        public string? Email { get; set; }
        public string ?SoDienThoai { get; set; }
    }
}