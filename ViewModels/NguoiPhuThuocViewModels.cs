using System.ComponentModel.DataAnnotations;

namespace BangLuong.ViewModels;

public class NguoiPhuThuocViewModels
{
    public class NguoiPhuThuocRequest
    {
        [Key]
        public int MaNPT { get; set; }

        [Required, StringLength(100)]
        public string HoTen { get; set; } = null!;

        [Required, DataType(DataType.Date)]
        public DateTime NgaySinh { get; set; }

        [Required, StringLength(50)]
        public string MoiQuanHe { get; set; } = null!;

        [Required, DataType(DataType.Date)]
        public DateTime ThoiGianBatDauGiamTru { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ThoiGianKetThucGiamTru { get; set; }

        [Required, StringLength(15)]
        public string MaNV { get; set; } = null!;
    }
    public class NguoiPhuThuocViewModel
    {
        public int MaNPT { get; set; }

        public string HoTen { get; set; } = null!;
        [DataType(DataType.Date)]
        public DateTime NgaySinh { get; set; }

        public string MoiQuanHe { get; set; } = null!;

        [DataType(DataType.Date)]
        public DateTime ThoiGianBatDauGiamTru { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ThoiGianKetThucGiamTru { get; set; }
        public string MaNV { get; set; } = null!;
    }
}
