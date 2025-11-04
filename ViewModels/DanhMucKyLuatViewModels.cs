using System;
using System.ComponentModel.DataAnnotations;

namespace BangLuong.ViewModels
{
    public class DanhMucKyLuatViewModels
    {
        public class DanhMucKyLuatRequest
        {
            [Key]
            [StringLength(10)]
            public string MaKL { get; set; } = null!;

            [Required]
            [StringLength(100)]
            public string TenKyLuat { get; set; } = null!;

            [Required]
            [Range(0, double.MaxValue, ErrorMessage = "Số tiền phạt phải lớn hơn hoặc bằng 0.")]
            public decimal SoTienPhat { get; set; }
        }

        public class DanhMucKyLuatViewModel
        {
            public string MaKL { get; set; } = null!;
            public string TenKyLuat { get; set; } = null!;
            public decimal SoTienPhat { get; set; }
        }
    }
}
