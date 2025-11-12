using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BangLuong.ViewModels
{
    public class ChucVuViewModels
    {
        public class ChucVuRequest
        {
            [Key]
            [StringLength(10)]
            [DisplayName("Mã Chức Vụ")]
            public string MaCV { get; set; } = null!;

            [Required]
            [StringLength(100)]
            [DisplayName("Tên Chức Vụ")]
            public string TenCV { get; set; } = null!;

            [StringLength(255)]
            [DisplayName("Mô Tả")]
            public string? MoTa { get; set; }
        }

        public class ChucVuViewModel
        {
            [DisplayName("Mã Chức Vụ")]
            public string MaCV { get; set; } = null!;

            [DisplayName("Tên Chức Vụ")]
            public string TenCV { get; set; } = null!;

            [DisplayName("Mô Tả")]
            public string? MoTa { get; set; }
        }
    }
}
