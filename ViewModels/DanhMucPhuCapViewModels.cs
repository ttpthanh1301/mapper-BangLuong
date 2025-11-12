using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BangLuong.ViewModels
{
    public class DanhMucPhuCapViewModels
    {
        public class DanhMucPhuCapRequest
        {
            [Key]
            [StringLength(10)]
            [DisplayName("Mã Phụ Cấp")]
            public string MaPC { get; set; } = null!;

            [Required]
            [StringLength(100)]
            [DisplayName("Tên Phụ Cấp")]
            public string TenPhuCap { get; set; } = null!;

            [Required]
            [Range(0, double.MaxValue, ErrorMessage = "Số tiền phải lớn hơn hoặc bằng 0.")]
            [DisplayName("Số Tiền")]
            public decimal SoTien { get; set; }
        }

        public class DanhMucPhuCapViewModel
        {
            [DisplayName("Mã Phụ Cấp")]
            public string MaPC { get; set; } = null!;

            [DisplayName("Tên Phụ Cấp")]
            public string TenPhuCap { get; set; } = null!;

            [DisplayName("Số Tiền")]
            public decimal SoTien { get; set; }
        }
    }
}
