using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BangLuong.ViewModels
{
    public class ChiTietKyLuatViewModels
    {
        public class ChiTietKyLuatRequest
        {
            [Key]
            [DisplayName("Mã Chi Tiết Kỷ Luật")]
            public int MaCTKL { get; set; }

            [Required(ErrorMessage = "Ngày vi phạm không được để trống.")]
            [DataType(DataType.Date)]
            [DisplayName("Ngày Vi Phạm")]
            public DateTime NgayViPham { get; set; }

            [StringLength(255, ErrorMessage = "Lý do không được vượt quá 255 ký tự.")]
            [DisplayName("Lý Do")]
            public string? LyDo { get; set; }

            [Required(ErrorMessage = "Mã kỷ luật không được để trống.")]
            [StringLength(10, ErrorMessage = "Mã kỷ luật không được vượt quá 10 ký tự.")]
            [DisplayName("Mã Kỷ Luật")]
            public string MaKL { get; set; } = null!;

            [Required(ErrorMessage = "Mã nhân viên không được để trống.")]
            [StringLength(15, ErrorMessage = "Mã nhân viên không được vượt quá 15 ký tự.")]
            [DisplayName("Mã Nhân Viên")]
            public string MaNV { get; set; } = null!;
        }

        public class ChiTietKyLuatViewModel
        {
            [DisplayName("Mã Chi Tiết Kỷ Luật")]
            public int MaCTKL { get; set; }

            [DisplayName("Ngày Vi Phạm")]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            public DateTime NgayViPham { get; set; }

            [DisplayName("Lý Do")]
            public string? LyDo { get; set; }

            [DisplayName("Mã Kỷ Luật")]
            public string MaKL { get; set; } = null!;

            [DisplayName("Mã Nhân Viên")]
            public string MaNV { get; set; } = null!;

            [DisplayName("Tên Kỷ Luật")]
            public string? TenKyLuat { get; set; }

            [DisplayName("Tên Nhân Viên")]
            public string? TenNhanVien { get; set; }
        }
    }
}
