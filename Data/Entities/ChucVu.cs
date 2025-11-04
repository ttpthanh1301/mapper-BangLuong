using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static BangLuong.Data.Entities.NhanVien;

namespace BangLuong.Data.Entities;

public class ChucVu
{
    [Key]
    [StringLength(10)]
    public string MaCV { get; set; } = null!;

    public string TenCV { get; set; } = null!;
    public string? MoTa { get; set; }

    // Navigation
    public ICollection<NhanVien>? NhanVien { get; set; }
}
public class ChucVuConfiguration : IEntityTypeConfiguration<ChucVu>
{
        public void Configure(EntityTypeBuilder<ChucVu> builder)
    {
        builder.HasKey(e => e.MaCV);

        builder.HasMany(e => e.NhanVien)
               .WithOne(e => e.ChucVu)
               .HasForeignKey(e => e.MaCV)
               .HasPrincipalKey(e => e.MaCV);
    }
}

