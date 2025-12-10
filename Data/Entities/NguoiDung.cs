using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BangLuong.Data.Entities;
using Microsoft.AspNetCore.Identity;

public class NguoiDung : IdentityUser
{
    [Required]
    [MaxLength(50)]
    public string PhanQuyen { get; set; } = "Employee";

    [Required]
    [MaxLength(50)]
    public string TrangThai { get; set; } = "Active";

    // Override Id để dùng làm MaNV
    [Key]
    [StringLength(15)]
    [Column(TypeName = "nvarchar(15)")]
    public override string Id
    {
        get => base.Id;
        set => base.Id = value;
    }

    // Giữ MaNV để code dễ đọc, EF sẽ không tạo cột mới
    [NotMapped]
    public string MaNV => Id;

    // Navigation property 1-1 với NhanVien
    [ForeignKey(nameof(Id))]
    public NhanVien NhanVien { get; set; } = null!;
}
