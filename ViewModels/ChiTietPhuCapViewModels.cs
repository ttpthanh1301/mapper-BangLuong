using System;
using System.ComponentModel.DataAnnotations;

namespace BangLuong.ViewModels
{
    public class ChiTietPhuCapViewModels
    {
        public class ChiTietPhuCapRequest
        {
            [Key]
            public int MaCTPC { get; set; }

            [Required]
            [DataType(DataType.Date)]
            public DateTime NgayApDung { get; set; }

            [StringLength(255)]
            public string? GhiChu { get; set; }

            [Required]
            [StringLength(10)]
            public string MaPC { get; set; } = null!;

            [Required]
            [StringLength(15)]
            public string MaNV { get; set; } = null!;
        }

        public class ChiTietPhuCapViewModel
        {
            public int MaCTPC { get; set; }
            [DataType(DataType.Date)]
            public DateTime NgayApDung { get; set; }
            public string? GhiChu { get; set; }
            public string MaPC { get; set; } = null!;
            public string MaNV { get; set; } = null!;
        }
    }
}
