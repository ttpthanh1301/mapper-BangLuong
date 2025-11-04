using System.ComponentModel.DataAnnotations;
using BangLuong.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BangLuong.Data.Entities;

public class PhongBan
{
    [Key]
    public string MaPB { get; set; } = null!;
    public string TenPB { get; set; } = null!;

    public string? MoTa { get; set; }

    // Navigation
    public ICollection<NhanVien>? NhanVien { get; set; }


}
public class PhongBanConfiguration : IEntityTypeConfiguration<PhongBan>
{
        public void Configure(EntityTypeBuilder<PhongBan> builder)
    {
        builder.HasKey(e => e.MaPB);

        builder.HasMany(e => e.NhanVien)
               .WithOne(e => e.PhongBan)
               .HasForeignKey(e => e.MaPB)
               .HasPrincipalKey(e => e.MaPB);
    }
}

