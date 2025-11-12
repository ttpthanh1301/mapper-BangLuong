using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BangLuong.ViewModels
{
    public class DanhMucKhenThuongViewModels
    {
        public class DanhMucKhenThuongRequest
        {
            [Key]
            [StringLength(10)]
            [DisplayName("Mã Khen Thưởng")]
            public string MaKT { get; set; } = null!;

            [Required]
            [StringLength(100)]
            [DisplayName("Tên Khen Thưởng")]
            public string TenKhenThuong { get; set; } = null!;

            [Required]
            [Range(0, double.MaxValue, ErrorMessage = "Số tiền phải lớn hơn hoặc bằng 0.")]
            [DisplayName("Số Tiền")]
            public decimal SoTien { get; set; }
        }

        public class DanhMucKhenThuongViewModel
        {
            [DisplayName("Mã Khen Thưởng")]
            public string MaKT { get; set; } = null!;

            [DisplayName("Tên Khen Thưởng")]
            public string TenKhenThuong { get; set; } = null!;

            [DisplayName("Số Tiền")]
            public decimal SoTien { get; set; }
        }
    }
}
