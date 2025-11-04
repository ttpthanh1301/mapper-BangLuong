using System.ComponentModel.DataAnnotations;

namespace BangLuong.ViewModels;

public class NguoiDungViewModels
{
    public class NguoiDungRequest
    {
        [Key, StringLength(15)]
        public string MaNV { get; set; } = null!;

        [Required, StringLength(255)]
        public string MatKhau { get; set; } = null!;

        [Required, StringLength(50)]
        public string PhanQuyen { get; set; } = null!;

        [Required, StringLength(50)]
        public string TrangThai { get; set; } = null!;

    }
    public class NguoiDungViewModel
    {
        public string MaNV { get; set; } = null!;

        public string MatKhau { get; set; } = null!;

        public string PhanQuyen { get; set; } = null!;

        public string TrangThai { get; set; } = null!;
    }
}
