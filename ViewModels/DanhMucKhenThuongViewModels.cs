using System;
using System.ComponentModel.DataAnnotations;

namespace BangLuong.ViewModels
{
    public class DanhMucKhenThuongViewModels
    {
        public class DanhMucKhenThuongRequest
        {
            [Key]
            [StringLength(10)]
            public string MaKT { get; set; } = null!;

            [Required]
            [StringLength(100)]
            public string TenKhenThuong { get; set; } = null!;

            [Required]
            [Range(0, double.MaxValue, ErrorMessage = "Số tiền phải lớn hơn hoặc bằng 0.")]
            public decimal SoTien { get; set; }
        }

        public class DanhMucKhenThuongViewModel
        {
            public string MaKT { get; set; } = null!;
            public string TenKhenThuong { get; set; } = null!;
            public decimal SoTien { get; set; }
        }
    }
}
