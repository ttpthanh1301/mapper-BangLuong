using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BangLuong.ViewModels
{
    public class PhongBanViewModels
    {
        public class PhongBanRequest
        {
            [Key]
            [StringLength(10)]
            [DisplayName("Mã Phòng Ban")]
            public string MaPB { get; set; } = null!;

            [Required]
            [StringLength(100)]
            [DisplayName("Tên Phòng Ban")]
            public string TenPB { get; set; } = null!;

            [StringLength(255)]
            [DisplayName("Mô Tả")]
            public string? MoTa { get; set; }
        }

        public class PhongBanViewModel
        {
            [DisplayName("Mã Phòng Ban")]
            public string MaPB { get; set; } = null!;

            [DisplayName("Tên Phòng Ban")]
            public string TenPB { get; set; } = null!;

            [DisplayName("Mô Tả")]
            public string? MoTa { get; set; }
        }
    }
}
