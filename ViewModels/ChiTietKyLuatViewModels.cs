using System;
using System.ComponentModel.DataAnnotations;

namespace BangLuong.ViewModels
{
    public class ChiTietKyLuatViewModels
    {
        public class ChiTietKyLuatRequest
        {
            [Key]
            public int MaCTKL { get; set; }

            [Required(ErrorMessage = "Ngày vi phạm không được để trống.")]
            [DataType(DataType.Date)]
            public DateTime NgayViPham { get; set; }

            [StringLength(255, ErrorMessage = "Lý do không được vượt quá 255 ký tự.")]
            public string? LyDo { get; set; }

            [Required(ErrorMessage = "Mã kỷ luật không được để trống.")]
            [StringLength(10, ErrorMessage = "Mã kỷ luật không được vượt quá 10 ký tự.")]
            public string MaKL { get; set; } = null!;

            [Required(ErrorMessage = "Mã nhân viên không được để trống.")]
            [StringLength(15, ErrorMessage = "Mã nhân viên không được vượt quá 15 ký tự.")]
            public string MaNV { get; set; } = null!;
        }

        public class ChiTietKyLuatViewModel
        {
            public int MaCTKL { get; set; }
            [DataType(DataType.Date)]
            public DateTime NgayViPham { get; set; }
            public string? LyDo { get; set; }
            public string MaKL { get; set; } = null!;
            public string MaNV { get; set; } = null!;
            public string? TenKyLuat { get; set; }
            public string? TenNhanVien { get; set; }
        }
    }
}
