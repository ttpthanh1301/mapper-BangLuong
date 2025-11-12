using System;
using System.ComponentModel;
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
            [DisplayName("Mã Tham Số")]
            public string MaTS { get; set; } = string.Empty;

            [Required(ErrorMessage = "Tên tham số không được để trống.")]
            [StringLength(255, ErrorMessage = "Tên tham số không được vượt quá 255 ký tự.")]
            [DisplayName("Tên Tham Số")]
            public string TenThamSo { get; set; } = string.Empty;

            [Required(ErrorMessage = "Giá trị tham số không được để trống.")]
            [StringLength(255, ErrorMessage = "Giá trị tham số không được vượt quá 255 ký tự.")]
            [DisplayName("Giá Trị")]
            public string GiaTri { get; set; } = string.Empty;

            [DisplayName("Ngày Áp Dụng")]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            public DateTime? NgayApDung { get; set; }
        }

        public class ThamSoHeThongViewModel
        {
            [DisplayName("Mã Tham Số")]
            public string MaTS { get; set; } = string.Empty;

            [DisplayName("Tên Tham Số")]
            public string TenThamSo { get; set; } = string.Empty;

            [DisplayName("Giá Trị")]
            public string GiaTri { get; set; } = string.Empty;

            [DisplayName("Ngày Áp Dụng")]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            public DateTime? NgayApDung { get; set; }
        }
    }
}
