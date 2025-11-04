using System.ComponentModel.DataAnnotations;

namespace BangLuong.ViewModels;

public class PhongBanViewModels
{
    public class PhongBanRequest
    {
        [Key]
        [StringLength(10)]
        public string MaPB { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string TenPB { get; set; } = null!;

        [StringLength(255)]
        public string? MoTa { get; set; }
    }
    public class PhongBanViewModel
    {
        public string MaPB { get; set; } = null!;
        public string TenPB { get; set; } = null!;
        public string? MoTa { get; set; }
    }
}
