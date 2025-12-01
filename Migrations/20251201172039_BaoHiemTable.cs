using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BangLuong.Migrations
{
    /// <inheritdoc />
    public partial class BaoHiemTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NhanVien_NguoiDung_NguoiDungId",
                table: "NhanVien");

            migrationBuilder.DropIndex(
                name: "IX_NhanVien_NguoiDungId",
                table: "NhanVien");

            migrationBuilder.DropColumn(
                name: "NguoiDungId",
                table: "NhanVien");

            migrationBuilder.AlterColumn<string>(
                name: "MaNV",
                table: "TongHopCong",
                type: "nvarchar(15)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "MaNV",
                table: "NhanVien",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450);

            migrationBuilder.AlterColumn<string>(
                name: "MaNV",
                table: "NguoiPhuThuoc",
                type: "nvarchar(15)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "MaNV",
                table: "NguoiDung",
                type: "nvarchar(15)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "MaNV",
                table: "HopDong",
                type: "nvarchar(15)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "MaNV",
                table: "ChiTietPhuCap",
                type: "nvarchar(15)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "MaNV",
                table: "ChiTietKyLuat",
                type: "nvarchar(15)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "MaNV",
                table: "ChiTietKhenThuong",
                type: "nvarchar(15)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "MaNV",
                table: "ChamCong",
                type: "nvarchar(15)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "MaNV",
                table: "BangTinhLuong",
                type: "nvarchar(15)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateTable(
                name: "BaoHiem",
                columns: table => new
                {
                    MaBH = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SoSoBHXH = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    MaTheBHYT = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    NoiDKKCB = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    NgayCapSo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NoiCapSo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    MaNV = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaoHiem", x => x.MaBH);
                    table.ForeignKey(
                        name: "FK_BaoHiem_NhanVien_MaNV",
                        column: x => x.MaNV,
                        principalTable: "NhanVien",
                        principalColumn: "MaNV",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NguoiDung_MaNV",
                table: "NguoiDung",
                column: "MaNV",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BaoHiem_MaNV",
                table: "BaoHiem",
                column: "MaNV",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_NguoiDung_NhanVien_MaNV",
                table: "NguoiDung",
                column: "MaNV",
                principalTable: "NhanVien",
                principalColumn: "MaNV",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NguoiDung_NhanVien_MaNV",
                table: "NguoiDung");

            migrationBuilder.DropTable(
                name: "BaoHiem");

            migrationBuilder.DropIndex(
                name: "IX_NguoiDung_MaNV",
                table: "NguoiDung");

            migrationBuilder.DropColumn(
                name: "MaNV",
                table: "NguoiDung");

            migrationBuilder.AlterColumn<string>(
                name: "MaNV",
                table: "TongHopCong",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)");

            migrationBuilder.AlterColumn<string>(
                name: "MaNV",
                table: "NhanVien",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);

            migrationBuilder.AddColumn<string>(
                name: "NguoiDungId",
                table: "NhanVien",
                type: "varchar(50)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MaNV",
                table: "NguoiPhuThuoc",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)");

            migrationBuilder.AlterColumn<string>(
                name: "MaNV",
                table: "HopDong",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)");

            migrationBuilder.AlterColumn<string>(
                name: "MaNV",
                table: "ChiTietPhuCap",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)");

            migrationBuilder.AlterColumn<string>(
                name: "MaNV",
                table: "ChiTietKyLuat",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)");

            migrationBuilder.AlterColumn<string>(
                name: "MaNV",
                table: "ChiTietKhenThuong",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)");

            migrationBuilder.AlterColumn<string>(
                name: "MaNV",
                table: "ChamCong",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)");

            migrationBuilder.AlterColumn<string>(
                name: "MaNV",
                table: "BangTinhLuong",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)");

            migrationBuilder.CreateIndex(
                name: "IX_NhanVien_NguoiDungId",
                table: "NhanVien",
                column: "NguoiDungId");

            migrationBuilder.AddForeignKey(
                name: "FK_NhanVien_NguoiDung_NguoiDungId",
                table: "NhanVien",
                column: "NguoiDungId",
                principalTable: "NguoiDung",
                principalColumn: "Id");
        }
    }
}
