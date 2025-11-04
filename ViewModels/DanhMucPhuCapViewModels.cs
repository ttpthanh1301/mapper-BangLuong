using System;
using System.ComponentModel.DataAnnotations;

namespace BangLuong.ViewModels
{
    public class DanhMucPhuCapViewModels
    {
        public class DanhMucPhuCapRequest
        {
            [Key]
            [StringLength(10)]
            public string MaPC { get; set; } = null!;

            [Required]
            [StringLength(100)]
            public string TenPhuCap { get; set; } = null!;

            [Required]
            [Range(0, double.MaxValue, ErrorMessage = "Số tiền phải lớn hơn hoặc bằng 0.")]
            public decimal SoTien { get; set; }
        }

        public class DanhMucPhuCapViewModel
        {
            public string MaPC { get; set; } = null!;
            public string TenPhuCap { get; set; } = null!;
            public decimal SoTien { get; set; }
        }
    }
}
