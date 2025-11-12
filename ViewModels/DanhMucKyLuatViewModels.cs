using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BangLuong.ViewModels
{
    public class DanhMucKyLuatViewModels
    {
        public class DanhMucKyLuatRequest
        {
            [Key]
            [StringLength(10)]
            [DisplayName("Mã Kỷ Luật")]
            public string MaKL { get; set; } = null!;

            [Required]
            [StringLength(100)]
            [DisplayName("Tên Kỷ Luật")]
            public string TenKyLuat { get; set; } = null!;

            [Required]
            [Range(0, double.MaxValue, ErrorMessage = "Số tiền phạt phải lớn hơn hoặc bằng 0.")]
            [DisplayName("Số Tiền Phạt")]
            public decimal SoTienPhat { get; set; }
        }

        public class DanhMucKyLuatViewModel
        {
            [DisplayName("Mã Kỷ Luật")]
            public string MaKL { get; set; } = null!;

            [DisplayName("Tên Kỷ Luật")]
            public string TenKyLuat { get; set; } = null!;

            [DisplayName("Số Tiền Phạt")]
            public decimal SoTienPhat { get; set; }
        }
    }
}
