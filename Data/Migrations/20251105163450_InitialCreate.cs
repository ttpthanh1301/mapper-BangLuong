using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BangLuong.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChucVu",
                columns: table => new
                {
                    MaCV = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    TenCV = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChucVu", x => x.MaCV);
                });

            migrationBuilder.CreateTable(
                name: "DanhMucKhenThuong",
                columns: table => new
                {
                    MaKT = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenKhenThuong = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoTien = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhMucKhenThuong", x => x.MaKT);
                });

            migrationBuilder.CreateTable(
                name: "DanhMucKyLuat",
                columns: table => new
                {
                    MaKL = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenKyLuat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoTienPhat = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhMucKyLuat", x => x.MaKL);
                });

            migrationBuilder.CreateTable(
                name: "DanhMucPhuCap",
                columns: table => new
                {
                    MaPC = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenPhuCap = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoTien = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhMucPhuCap", x => x.MaPC);
                });

            migrationBuilder.CreateTable(
                name: "PhongBan",
                columns: table => new
                {
                    MaPB = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenPB = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhongBan", x => x.MaPB);
                });

            migrationBuilder.CreateTable(
                name: "ThamSoHeThong",
                columns: table => new
                {
                    MaTS = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenThamSo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GiaTri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayApDung = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThamSoHeThong", x => x.MaTS);
                });

            migrationBuilder.CreateTable(
                name: "NhanVien",
                columns: table => new
                {
                    MaNV = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GioiTinh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CCCD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoDienThoai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaSoThue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaiKhoanNganHang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenNganHang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayVaoLam = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaPB = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    MaCV = table.Column<string>(type: "nvarchar(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhanVien", x => x.MaNV);
                    table.ForeignKey(
                        name: "FK_NhanVien_ChucVu_MaCV",
                        column: x => x.MaCV,
                        principalTable: "ChucVu",
                        principalColumn: "MaCV");
                    table.ForeignKey(
                        name: "FK_NhanVien_PhongBan_MaPB",
                        column: x => x.MaPB,
                        principalTable: "PhongBan",
                        principalColumn: "MaPB");
                });

            migrationBuilder.CreateTable(
                name: "BangTinhLuong",
                columns: table => new
                {
                    MaBL = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KyLuongThang = table.Column<int>(type: "int", nullable: false),
                    KyLuongNam = table.Column<int>(type: "int", nullable: false),
                    LuongCoBan = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TongPhuCap = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    TongKhenThuong = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    LuongTangCa = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    TongThuNhap = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    GiamTruBHXH = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    GiamTruBHYT = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    GiamTruBHTN = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    TongGiamTruKyLuat = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    GiamTruThueTNCN = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    ThucLanh = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaNV = table.Column<string>(type: "nvarchar(15)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BangTinhLuong", x => x.MaBL);
                    table.ForeignKey(
                        name: "FK_BangTinhLuong_NhanVien_MaNV",
                        column: x => x.MaNV,
                        principalTable: "NhanVien",
                        principalColumn: "MaNV",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChamCong",
                columns: table => new
                {
                    MaCC = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NgayChamCong = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GioVao = table.Column<TimeSpan>(type: "time", nullable: true),
                    GioRa = table.Column<TimeSpan>(type: "time", nullable: true),
                    SoGioTangCa = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    MaNV = table.Column<string>(type: "nvarchar(15)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChamCong", x => x.MaCC);
                    table.ForeignKey(
                        name: "FK_ChamCong_NhanVien_MaNV",
                        column: x => x.MaNV,
                        principalTable: "NhanVien",
                        principalColumn: "MaNV",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietKhenThuong",
                columns: table => new
                {
                    MaCTKT = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NgayKhenThuong = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LyDo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaKT = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaNV = table.Column<string>(type: "nvarchar(15)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietKhenThuong", x => x.MaCTKT);
                    table.ForeignKey(
                        name: "FK_ChiTietKhenThuong_DanhMucKhenThuong_MaKT",
                        column: x => x.MaKT,
                        principalTable: "DanhMucKhenThuong",
                        principalColumn: "MaKT",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietKhenThuong_NhanVien_MaNV",
                        column: x => x.MaNV,
                        principalTable: "NhanVien",
                        principalColumn: "MaNV",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietKyLuat",
                columns: table => new
                {
                    MaCTKL = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NgayViPham = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LyDo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaKL = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaNV = table.Column<string>(type: "nvarchar(15)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietKyLuat", x => x.MaCTKL);
                    table.ForeignKey(
                        name: "FK_ChiTietKyLuat_DanhMucKyLuat_MaKL",
                        column: x => x.MaKL,
                        principalTable: "DanhMucKyLuat",
                        principalColumn: "MaKL",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietKyLuat_NhanVien_MaNV",
                        column: x => x.MaNV,
                        principalTable: "NhanVien",
                        principalColumn: "MaNV",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietPhuCap",
                columns: table => new
                {
                    MaCTPC = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NgayApDung = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaPC = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaNV = table.Column<string>(type: "nvarchar(15)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietPhuCap", x => x.MaCTPC);
                    table.ForeignKey(
                        name: "FK_ChiTietPhuCap_DanhMucPhuCap_MaPC",
                        column: x => x.MaPC,
                        principalTable: "DanhMucPhuCap",
                        principalColumn: "MaPC",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietPhuCap_NhanVien_MaNV",
                        column: x => x.MaNV,
                        principalTable: "NhanVien",
                        principalColumn: "MaNV",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HopDong",
                columns: table => new
                {
                    MaHD = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SoHopDong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoaiHD = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayBatDau = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayKetThuc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LuongCoBan = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PhuCapAnTrua = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    PhuCapXangXe = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    PhuCapDienThoai = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    PhuCapTrachNhiem = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    PhuCapKhac = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    TrangThai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaNV = table.Column<string>(type: "nvarchar(15)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HopDong", x => x.MaHD);
                    table.ForeignKey(
                        name: "FK_HopDong_NhanVien_MaNV",
                        column: x => x.MaNV,
                        principalTable: "NhanVien",
                        principalColumn: "MaNV",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NguoiDung",
                columns: table => new
                {
                    MaNV = table.Column<string>(type: "nvarchar(15)", nullable: false),
                    MatKhau = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhanQuyen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NguoiDung", x => x.MaNV);
                    table.ForeignKey(
                        name: "FK_NguoiDung_NhanVien_MaNV",
                        column: x => x.MaNV,
                        principalTable: "NhanVien",
                        principalColumn: "MaNV",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NguoiPhuThuoc",
                columns: table => new
                {
                    MaNPT = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoTen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MoiQuanHe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThoiGianBatDauGiamTru = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ThoiGianKetThucGiamTru = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaNV = table.Column<string>(type: "nvarchar(15)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NguoiPhuThuoc", x => x.MaNPT);
                    table.ForeignKey(
                        name: "FK_NguoiPhuThuoc_NhanVien_MaNV",
                        column: x => x.MaNV,
                        principalTable: "NhanVien",
                        principalColumn: "MaNV",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TongHopCong",
                columns: table => new
                {
                    MaTHC = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KyLuongThang = table.Column<int>(type: "int", nullable: false),
                    KyLuongNam = table.Column<int>(type: "int", nullable: false),
                    SoNgayCong = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    SoGioTangCaNgayThuong = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    SoGioTangCaCuoiTuan = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    SoGioTangCaNgayLe = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    SoNgayNghiPhep = table.Column<int>(type: "int", nullable: true),
                    MaNV = table.Column<string>(type: "nvarchar(15)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TongHopCong", x => x.MaTHC);
                    table.ForeignKey(
                        name: "FK_TongHopCong_NhanVien_MaNV",
                        column: x => x.MaNV,
                        principalTable: "NhanVien",
                        principalColumn: "MaNV",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BangTinhLuong_MaNV",
                table: "BangTinhLuong",
                column: "MaNV");

            migrationBuilder.CreateIndex(
                name: "IX_ChamCong_MaNV",
                table: "ChamCong",
                column: "MaNV");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietKhenThuong_MaKT",
                table: "ChiTietKhenThuong",
                column: "MaKT");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietKhenThuong_MaNV",
                table: "ChiTietKhenThuong",
                column: "MaNV");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietKyLuat_MaKL",
                table: "ChiTietKyLuat",
                column: "MaKL");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietKyLuat_MaNV",
                table: "ChiTietKyLuat",
                column: "MaNV");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietPhuCap_MaNV",
                table: "ChiTietPhuCap",
                column: "MaNV");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietPhuCap_MaPC",
                table: "ChiTietPhuCap",
                column: "MaPC");

            migrationBuilder.CreateIndex(
                name: "IX_HopDong_MaNV",
                table: "HopDong",
                column: "MaNV");

            migrationBuilder.CreateIndex(
                name: "IX_NguoiPhuThuoc_MaNV",
                table: "NguoiPhuThuoc",
                column: "MaNV");

            migrationBuilder.CreateIndex(
                name: "IX_NhanVien_MaCV",
                table: "NhanVien",
                column: "MaCV");

            migrationBuilder.CreateIndex(
                name: "IX_NhanVien_MaPB",
                table: "NhanVien",
                column: "MaPB");

            migrationBuilder.CreateIndex(
                name: "IX_TongHopCong_MaNV",
                table: "TongHopCong",
                column: "MaNV");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BangTinhLuong");

            migrationBuilder.DropTable(
                name: "ChamCong");

            migrationBuilder.DropTable(
                name: "ChiTietKhenThuong");

            migrationBuilder.DropTable(
                name: "ChiTietKyLuat");

            migrationBuilder.DropTable(
                name: "ChiTietPhuCap");

            migrationBuilder.DropTable(
                name: "HopDong");

            migrationBuilder.DropTable(
                name: "NguoiDung");

            migrationBuilder.DropTable(
                name: "NguoiPhuThuoc");

            migrationBuilder.DropTable(
                name: "ThamSoHeThong");

            migrationBuilder.DropTable(
                name: "TongHopCong");

            migrationBuilder.DropTable(
                name: "DanhMucKhenThuong");

            migrationBuilder.DropTable(
                name: "DanhMucKyLuat");

            migrationBuilder.DropTable(
                name: "DanhMucPhuCap");

            migrationBuilder.DropTable(
                name: "NhanVien");

            migrationBuilder.DropTable(
                name: "ChucVu");

            migrationBuilder.DropTable(
                name: "PhongBan");
        }
    }
}
