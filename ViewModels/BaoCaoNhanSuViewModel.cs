using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace BangLuong.ViewModels
{
    public class BaoCaoNhanSuViewModel
    {
        [DisplayName("Mã Nhân Viên")]
        public string? MaNV { get; set; }

        [DisplayName("Họ và Tên")]
        public string? HoTen { get; set; }

        [DisplayName("Giới Tính")]
        public string? GioiTinh { get; set; }

        [DisplayName("Ngày Sinh")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? NgaySinh { get; set; }

        [DisplayName("Phòng Ban")]
        public string? PhongBan { get; set; }

        [DisplayName("Chức Vụ")]
        public string? ChucVu { get; set; }

        [DisplayName("Ngày Vào Làm")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? NgayVaoLam { get; set; }

        [DisplayName("Thâm Niên (Năm)")]
        [Precision(18, 2)]
        public decimal? ThamNienNam { get; set; }

        [DisplayName("Loại Hợp Đồng")]
        public string? LoaiHopDong { get; set; }

        [DisplayName("Trạng Thái Hợp Đồng")]
        public string? TrangThaiHopDong { get; set; }

        [DisplayName("Email")]
        [EmailAddress]
        public string? Email { get; set; }

        [DisplayName("Số Điện Thoại")]
        [Phone]
        [StringLength(15)]
        public string? SoDienThoai { get; set; }
    }
}
