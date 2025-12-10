using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BangLuong.Data.Entities
{
public class BaoHiem
{
[Key]
public int MaBH { get; set; }

    [Required]
    [MaxLength(20)]
    public string SoSoBHXH { get; set; } = null!;

    [MaxLength(20)]
    public string? MaTheBHYT { get; set; }

    [MaxLength(255)]
    public string? NoiDKKCB { get; set; }

    public DateTime? NgayCapSo { get; set; }

    [MaxLength(100)]
    public string? NoiCapSo { get; set; }

    [Required]       
     [StringLength(450)]
    public string MaNV { get; set; } = null!; // FK + Unique

    // Navigation
    public NhanVien? NhanVien { get; set; }
}

public class BaoHiemConfiguration : IEntityTypeConfiguration<BaoHiem>
{
    public void Configure(EntityTypeBuilder<BaoHiem> builder)
    {
        builder.HasKey(b => b.MaBH);

        builder.HasIndex(b => b.SoSoBHXH).IsUnique();
        builder.HasIndex(b => b.MaNV).IsUnique(); // 1-1

        builder.Property(b => b.SoSoBHXH)
               .IsRequired()
               .HasMaxLength(20);

        builder.Property(b => b.MaTheBHYT)
               .HasMaxLength(20);

        builder.Property(b => b.NoiDKKCB)
               .HasMaxLength(255);

        builder.Property(b => b.NoiCapSo)
               .HasMaxLength(100);

        builder.Property(b => b.MaNV)
               .IsRequired()
               .HasMaxLength(15);
    }
}
}
