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
            [DataType(DataType.DateTime)] // ✅ Hiển thị cả ngày và giờ
            [Display(Name = "Ngày Chấm Công")]
            public DateTime NgayChamCong { get; set; }

            [DataType(DataType.Time)]
            [Display(Name = "Giờ Vào")]
            public TimeSpan? GioVao { get; set; }

            [DataType(DataType.Time)]
            [Display(Name = "Giờ Ra")]
            public TimeSpan? GioRa { get; set; }

            [Range(0, double.MaxValue, ErrorMessage = "Số giờ tăng ca phải lớn hơn hoặc bằng 0.")]
            [Display(Name = "Số Giờ Tăng Ca")]
            public decimal? SoGioTangCa { get; set; }

            [Required]
            [StringLength(15)]
            [Display(Name = "Mã Nhân Viên")]
            public string MaNV { get; set; } = null!;
        }

        public class ChamCongViewModel
        {
            [Display(Name = "Mã Chấm Công")]
            public int MaCC { get; set; }

            [DataType(DataType.DateTime)] // ✅ Hiển thị cả ngày + giờ
            [Display(Name = "Ngày Chấm Công")]
            public DateTime NgayChamCong { get; set; }

            [DataType(DataType.Time)]
            [Display(Name = "Giờ Vào")]
            public TimeSpan? GioVao { get; set; }

            [DataType(DataType.Time)]
            [Display(Name = "Giờ Ra")]
            public TimeSpan? GioRa { get; set; }

            [Display(Name = "Số Giờ Tăng Ca")]
            public decimal? SoGioTangCa { get; set; }

            [Display(Name = "Mã Nhân Viên")]
            public string MaNV { get; set; } = null!;
        }
    }
}
