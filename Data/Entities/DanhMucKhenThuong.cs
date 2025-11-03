using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class DanhMucKhenThuong
{
    [Key, StringLength(10)]
    public string MaKT { get; set; } = null!;

    [Required, StringLength(100)]
    public string TenKhenThuong { get; set; } = null!;
    [Precision(18, 2)]

    [Required]
    public decimal SoTien { get; set; }

    public ICollection<ChiTietKhenThuong>? ChiTietKhenThuong { get; set; }
}
public class DanhMucKhenThuongConfiguration : IEntityTypeConfiguration<DanhMucKhenThuong>
{
        public void Configure(EntityTypeBuilder<DanhMucKhenThuong> builder)
    {
        builder.HasKey(e => e.MaKT);

        builder.HasMany(e => e.ChiTietKhenThuong)
               .WithOne(e => e.DanhMucKhenThuong)
               .HasForeignKey(e => e.MaKT)
               .HasPrincipalKey(e => e.MaKT);
    }
}
