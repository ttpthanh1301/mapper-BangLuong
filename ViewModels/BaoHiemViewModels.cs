using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BangLuong.ViewModels
{
    // Lớp chứa các ViewModels liên quan đến Bảo Hiểm
    public class BaoHiemViewModels
    {
        // ===============================================================
        // 1. Dùng cho việc tạo mới hoặc cập nhật (Create/Edit Request)
        // ===============================================================
        public class BaoHiemRequest
        {
            [DisplayName("Mã Bảo Hiểm")]
            public int MaBH { get; set; } // Chỉ dùng cho Edit/Update

            [Required(ErrorMessage = "Số sổ BHXH không được để trống.")]
            [StringLength(20, ErrorMessage = "Số sổ BHXH không được vượt quá 20 ký tự.")]
            [DisplayName("Số Sổ BHXH")]
            public string SoSoBHXH { get; set; } = null!;

            [StringLength(20, ErrorMessage = "Mã thẻ BHYT không được vượt quá 20 ký tự.")]
            [DisplayName("Mã Thẻ BHYT")]
            public string? MaTheBHYT { get; set; }

            [StringLength(255, ErrorMessage = "Nơi đăng ký KCB không được vượt quá 255 ký tự.")]
            [DisplayName("Nơi ĐK KCB")]
            public string? NoiDKKCB { get; set; }

            [DisplayName("Ngày Cấp Sổ")]
            [DataType(DataType.Date)]
            public DateTime? NgayCapSo { get; set; }

            [StringLength(100, ErrorMessage = "Nơi cấp sổ không được vượt quá 100 ký tự.")]
            [DisplayName("Nơi Cấp Sổ")]
            public string? NoiCapSo { get; set; }

            [Required(ErrorMessage = "Mã nhân viên không được để trống.")]
            // Giả định MaxLength 15 theo Configuration trước đó
            [StringLength(15, ErrorMessage = "Mã nhân viên không được vượt quá 15 ký tự.")]
            [DisplayName("Mã Nhân Viên")]
            public string MaNV { get; set; } = null!;
        }

        // ===============================================================
        // 2. Dùng để hiển thị dữ liệu ra View (Index/Details)
        // ===============================================================
        public class BaoHiemViewModel
        {
            [DisplayName("Mã BH")]
            public int MaBH { get; set; }

            [DisplayName("Số Sổ BHXH")]
            public string SoSoBHXH { get; set; } = null!;

            [DisplayName("Mã Thẻ BHYT")]
            public string? MaTheBHYT { get; set; }

            [DisplayName("Nơi ĐK KCB")]
            public string? NoiDKKCB { get; set; }

            [DisplayName("Ngày Cấp Sổ")]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            public DateTime? NgayCapSo { get; set; }

            [DisplayName("Nơi Cấp Sổ")]
            public string? NoiCapSo { get; set; }

            [DisplayName("Mã NV")]
            public string MaNV { get; set; } = null!;

            // Thuộc tính mở rộng để hiển thị tên nhân viên
            [DisplayName("Họ và Tên")]
            public string? TenNhanVien { get; set; }
        }
    }
}