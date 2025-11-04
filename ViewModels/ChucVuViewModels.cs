using System.ComponentModel.DataAnnotations;

namespace BangLuong.ViewModels;

public class ChucVuViewModels
{
    public class ChucVuRequest
    {

        [Key]
        [StringLength(10)]
        public string MaCV { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string TenCV { get; set; } = null!;

        [StringLength(255)]
        public string? MoTa { get; set; }
    }
    public class ChucVuViewModel
    {
        public string MaCV { get; set; } = null!;
        public string TenCV { get; set; } = null!;
        public string? MoTa { get; set; }
    }
    
}
