using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class DanhMucKhenThuong
{
    [Key]
    public string MaKT { get; set; } = null!;
    public string TenKhenThuong { get; set; } = null!;
    [Precision(18, 2)]
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
