using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BangLuong.ViewModels
{
    public class NguoiPhuThuocViewModels
    {
        public class NguoiPhuThuocRequest
        {
            [Key]
            [DisplayName("Mã Người Phụ Thuộc")]
            public int MaNPT { get; set; }

            [Required, StringLength(100)]
            [DisplayName("Họ Tên")]
            public string HoTen { get; set; } = null!;

            [Required, DataType(DataType.Date)]
            [DisplayName("Ngày Sinh")]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            public DateTime NgaySinh { get; set; }

            [Required, StringLength(50)]
            [DisplayName("Mối Quan Hệ")]
            public string MoiQuanHe { get; set; } = null!;

            [Required, DataType(DataType.Date)]
            [DisplayName("Thời Gian Bắt Đầu Giảm Trừ")]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            public DateTime ThoiGianBatDauGiamTru { get; set; }

            [DataType(DataType.Date)]
            [DisplayName("Thời Gian Kết Thúc Giảm Trừ")]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            public DateTime? ThoiGianKetThucGiamTru { get; set; }

            [Required, StringLength(15)]
            [DisplayName("Mã Nhân Viên")]
            public string MaNV { get; set; } = null!;
        }

        public class NguoiPhuThuocViewModel
        {
            [DisplayName("Mã Người Phụ Thuộc")]
            public int MaNPT { get; set; }

            [DisplayName("Họ Tên")]
            public string HoTen { get; set; } = null!;

            [DisplayName("Ngày Sinh")]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            public DateTime NgaySinh { get; set; }

            [DisplayName("Mối Quan Hệ")]
            public string MoiQuanHe { get; set; } = null!;

            [DisplayName("Thời Gian Bắt Đầu Giảm Trừ")]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            public DateTime ThoiGianBatDauGiamTru { get; set; }

            [DisplayName("Thời Gian Kết Thúc Giảm Trừ")]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            public DateTime? ThoiGianKetThucGiamTru { get; set; }

            [DisplayName("Mã Nhân Viên")]
            public string MaNV { get; set; } = null!;
        }
    }
}
