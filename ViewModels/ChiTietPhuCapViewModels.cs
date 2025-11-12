using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BangLuong.ViewModels
{
    public class ChiTietPhuCapViewModels
    {
        public class ChiTietPhuCapRequest
        {
            [Key]
            [DisplayName("Mã Chi Tiết Phụ Cấp")]
            public int MaCTPC { get; set; }

            [Required]
            [DataType(DataType.Date)]
            [DisplayName("Ngày Áp Dụng")]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            public DateTime NgayApDung { get; set; }

            [StringLength(255)]
            [DisplayName("Ghi Chú")]
            public string? GhiChu { get; set; }

            [Required]
            [StringLength(10)]
            [DisplayName("Mã Phụ Cấp")]
            public string MaPC { get; set; } = null!;

            [Required]
            [StringLength(15)]
            [DisplayName("Mã Nhân Viên")]
            public string MaNV { get; set; } = null!;
        }

        public class ChiTietPhuCapViewModel
        {
            [DisplayName("Mã Chi Tiết Phụ Cấp")]
            public int MaCTPC { get; set; }

            [DisplayName("Ngày Áp Dụng")]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            public DateTime NgayApDung { get; set; }

            [DisplayName("Ghi Chú")]
            public string? GhiChu { get; set; }

            [DisplayName("Mã Phụ Cấp")]
            public string MaPC { get; set; } = null!;

            [DisplayName("Mã Nhân Viên")]
            public string MaNV { get; set; } = null!;
        }
    }
}
