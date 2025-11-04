using System;
using System.ComponentModel.DataAnnotations;

namespace BangLuong.ViewModels
{
    public class ThamSoHeThongViewModels
    {
        public class ThamSoHeThongRequest
        {
            [Key]
            [StringLength(50, ErrorMessage = "Mã tham số không được vượt quá 50 ký tự.")]
            [Required(ErrorMessage = "Mã tham số không được để trống.")]
            public string MaTS { get; set; } = string.Empty;

            [Required(ErrorMessage = "Tên tham số không được để trống.")]
            [StringLength(255, ErrorMessage = "Tên tham số không được vượt quá 255 ký tự.")]
            public string TenThamSo { get; set; } = string.Empty;

            [Required(ErrorMessage = "Giá trị tham số không được để trống.")]
            [StringLength(255, ErrorMessage = "Giá trị tham số không được vượt quá 255 ký tự.")]
            public string GiaTri { get; set; } = string.Empty;

            public DateTime? NgayApDung { get; set; }
        }

        public class ThamSoHeThongViewModel
        {
            public string MaTS { get; set; } = string.Empty;
            public string TenThamSo { get; set; } = string.Empty;
            public string GiaTri { get; set; } = string.Empty;
            public DateTime? NgayApDung { get; set; }
        }
    }
}
