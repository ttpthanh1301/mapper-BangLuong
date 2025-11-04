using System;
using System.ComponentModel.DataAnnotations;

namespace BangLuong.ViewModels
{
    public class ChamCongViewModels
    {
        public class ChamCongRequest
        {
            [Key]
            public int MaCC { get; set; }

            [Required]
            [DataType(DataType.Date)]
            public DateTime NgayChamCong { get; set; }

            [DataType(DataType.Time)]
            public TimeSpan? GioVao { get; set; }

            [DataType(DataType.Time)]
            public TimeSpan? GioRa { get; set; }

            [Range(0, double.MaxValue, ErrorMessage = "Số giờ tăng ca phải lớn hơn hoặc bằng 0.")]
            public decimal? SoGioTangCa { get; set; }

            [Required]
            [StringLength(15)]
            public string MaNV { get; set; } = null!;
        }

        public class ChamCongViewModel
        {
            public int MaCC { get; set; }
            [DataType(DataType.Time)]

            public DateTime NgayChamCong { get; set; }
            [DataType(DataType.Time)]
            public TimeSpan? GioVao { get; set; }
            [DataType(DataType.Time)]
            public TimeSpan? GioRa { get; set; }
            public decimal? SoGioTangCa { get; set; }
            public string MaNV { get; set; } = null!;
        }
    }
}
