using Microsoft.EntityFrameworkCore;
using BangLuong.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using BCrypt.Net;

namespace BangLuong.Data
{
    public static class DbInitializer
    {
        public static void Seed(IServiceProvider serviceProvider)
        {
            using var context = new BangLuongDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<BangLuongDbContext>>());

            // 🏢 PHÒNG BAN
            if (!context.PhongBan.Any())
            {
                context.PhongBan.AddRange(
                    new PhongBan { MaPB = "PB01", TenPB = "Phòng Hệ thống", MoTa = "Quản lý hạ tầng và hệ thống" },
                    new PhongBan { MaPB = "PB02", TenPB = "Phòng PTSP và ĐBCL", MoTa = "Phát triển sản phẩm và đảm bảo chất lượng" },
                    new PhongBan { MaPB = "PB03", TenPB = "Phòng Phát triển ứng dụng", MoTa = "Phát triển các ứng dụng nội bộ" },
                    new PhongBan { MaPB = "PB04", TenPB = "Phòng Triển khai dự án phía Nam", MoTa = "Triển khai dự án tại khu vực phía Nam" },
                    new PhongBan { MaPB = "PB05", TenPB = "Phòng Hành chính tổng hợp", MoTa = "Hành chính và tổng hợp" },
                    new PhongBan { MaPB = "PB06", TenPB = "Phòng Kinh doanh", MoTa = "Phát triển thị trường và chăm sóc khách hàng" }
                );
                context.SaveChanges();
            }

            // 🎯 CHỨC VỤ
            if (!context.ChucVu.Any())
            {
                context.ChucVu.AddRange(
                    new ChucVu { MaCV = "CV01", TenCV = "Trưởng phòng" },
                    new ChucVu { MaCV = "CV02", TenCV = "Nhân viên kế toán" },
                    new ChucVu { MaCV = "CV03", TenCV = "Nhân viên kinh doanh" },
                    new ChucVu { MaCV = "CV04", TenCV = "Thực tập sinh" }
                );
                context.SaveChanges();
            }

            // 👩‍💼 NHÂN VIÊN
            if (!context.NhanVien.Any())
            {
                context.NhanVien.AddRange(
                    new NhanVien
                    {
                        MaNV = "NV001",
                        HoTen = "Nguyễn Văn An",
                        NgaySinh = new DateTime(1995, 3, 14),
                        GioiTinh = "Nam",
                        CCCD = "012345678901",
                        DiaChi = "123 Trần Phú, Hà Nội",
                        SoDienThoai = "0905123456",
                        Email = "an.nguyen@example.com",
                        MaSoThue = "1234567890123",
                        TaiKhoanNganHang = "1234567890123456",
                        TenNganHang = "Vietcombank",
                        NgayVaoLam = new DateTime(2020, 5, 1),
                        TrangThai = "Đang làm việc",
                        MaPB = "PB01",
                        MaCV = "CV01"
                    },
                    new NhanVien
                    {
                        MaNV = "NV002",
                        HoTen = "Trần Thị Bình",
                        NgaySinh = new DateTime(1997, 9, 22),
                        GioiTinh = "Nữ",
                        CCCD = "098765432109",
                        DiaChi = "45 Lê Lợi, TP. Hồ Chí Minh",
                        SoDienThoai = "0912345678",
                        Email = "binh.tran@example.com",
                        MaSoThue = "9876543210987",
                        TaiKhoanNganHang = "2345678901234567",
                        TenNganHang = "Techcombank",
                        NgayVaoLam = new DateTime(2021, 3, 15),
                        TrangThai = "Đang làm việc",
                        MaPB = "PB02",
                        MaCV = "CV02"
                    },
                    new NhanVien
                    {
                        MaNV = "NV003",
                        HoTen = "Lê Hoàng Minh",
                        NgaySinh = new DateTime(1992, 11, 10),
                        GioiTinh = "Nam",
                        CCCD = "045678901234",
                        DiaChi = "56 Nguyễn Văn Linh, Đà Nẵng",
                        SoDienThoai = "0923123123",
                        Email = "minh.le@example.com",
                        MaSoThue = "4567891234567",
                        TaiKhoanNganHang = "3456789012345678",
                        TenNganHang = "BIDV",
                        NgayVaoLam = new DateTime(2019, 8, 20),
                        TrangThai = "Đang làm việc",
                        MaPB = "PB01",
                        MaCV = "CV03"
                    },
                    new NhanVien
                    {
                        MaNV = "NV004",
                        HoTen = "Phạm Thu Hà",
                        NgaySinh = new DateTime(1998, 12, 5),
                        GioiTinh = "Nữ",
                        CCCD = "076543219876",
                        DiaChi = "78 Pasteur, Cần Thơ",
                        SoDienThoai = "0934123123",
                        Email = "ha.pham@example.com",
                        MaSoThue = "7654321987654",
                        TaiKhoanNganHang = "4567890123456789",
                        TenNganHang = "Agribank",
                        NgayVaoLam = new DateTime(2022, 6, 10),
                        TrangThai = "Đang thử việc",
                        MaPB = "PB03",
                        MaCV = "CV04"
                    }
                );
                context.SaveChanges();
            }

            // 👨‍👩‍👧 NGƯỜI PHỤ THUỘC
            if (!context.NguoiPhuThuoc.Any())
            {
                context.NguoiPhuThuoc.AddRange(
                    new NguoiPhuThuoc
                    {
                        HoTen = "Nguyễn Thị Hoa",
                        NgaySinh = new DateTime(1970, 4, 12),
                        MoiQuanHe = "Mẹ ruột",
                        ThoiGianBatDauGiamTru = new DateTime(2020, 1, 1),
                        MaNV = "NV001"
                    },
                    new NguoiPhuThuoc
                    {
                        HoTen = "Nguyễn Văn Bình",
                        NgaySinh = new DateTime(2018, 8, 5),
                        MoiQuanHe = "Con trai",
                        ThoiGianBatDauGiamTru = new DateTime(2020, 1, 1),
                        MaNV = "NV001"
                    },
                    new NguoiPhuThuoc
                    {
                        HoTen = "Trần Văn Nam",
                        NgaySinh = new DateTime(2016, 2, 20),
                        MoiQuanHe = "Con trai",
                        ThoiGianBatDauGiamTru = new DateTime(2021, 3, 15),
                        MaNV = "NV002"
                    },
                    new NguoiPhuThuoc
                    {
                        HoTen = "Lê Thị Mai",
                        NgaySinh = new DateTime(2019, 7, 9),
                        MoiQuanHe = "Con gái",
                        ThoiGianBatDauGiamTru = new DateTime(2022, 1, 1),
                        MaNV = "NV003"
                    },
                    new NguoiPhuThuoc
                    {
                        HoTen = "Phạm Văn Long",
                        NgaySinh = new DateTime(1965, 10, 23),
                        MoiQuanHe = "Cha ruột",
                        ThoiGianBatDauGiamTru = new DateTime(2023, 5, 1),
                        MaNV = "NV004"
                    }
                );
                context.SaveChanges();
            }

            // 🔐 NGƯỜI DÙNG
            if (!context.NguoiDung.Any())
            {
                context.NguoiDung.AddRange(
                    new NguoiDung
                    {
                        MaNV = "NV001",
                        MatKhau = BCrypt.Net.BCrypt.HashPassword("123456", BCrypt.Net.BCrypt.GenerateSalt()),
                        PhanQuyen = "Admin",
                        TrangThai = "Hoạt động"
                    },
                    new NguoiDung
                    {
                        MaNV = "NV002",
                        MatKhau = BCrypt.Net.BCrypt.HashPassword("123456", BCrypt.Net.BCrypt.GenerateSalt()),
                        PhanQuyen = "Kế toán",
                        TrangThai = "Hoạt động"
                    },
                    new NguoiDung
                    {
                        MaNV = "NV003",
                        MatKhau = BCrypt.Net.BCrypt.HashPassword("123456", BCrypt.Net.BCrypt.GenerateSalt()),
                        PhanQuyen = "Nhân viên",
                        TrangThai = "Hoạt động"
                    },
                    new NguoiDung
                    {
                        MaNV = "NV004",
                        MatKhau = BCrypt.Net.BCrypt.HashPassword("123456", BCrypt.Net.BCrypt.GenerateSalt()),
                        PhanQuyen = "Thử việc",
                        TrangThai = "Đang thử việc"
                    }
                );
                context.SaveChanges();
            }

            // 🏆 DANH MỤC KHEN THƯỞNG
            if (!context.DanhMucKhenThuong.Any())
            {
                context.DanhMucKhenThuong.AddRange(
                    new DanhMucKhenThuong
                    {
                        MaKT = "KT001",
                        TenKhenThuong = "Nhân viên xuất sắc tháng",
                        SoTien = 1000000
                    },
                    new DanhMucKhenThuong
                    {
                        MaKT = "KT002",
                        TenKhenThuong = "Nhân viên gương mẫu",
                        SoTien = 800000
                    },
                    new DanhMucKhenThuong
                    {
                        MaKT = "KT003",
                        TenKhenThuong = "Hoàn thành dự án đúng hạn",
                        SoTien = 1200000
                    },
                    new DanhMucKhenThuong
                    {
                        MaKT = "KT004",
                        TenKhenThuong = "Sáng kiến cải tiến hiệu quả",
                        SoTien = 1500000
                    },
                    new DanhMucKhenThuong
                    {
                        MaKT = "KT005",
                        TenKhenThuong = "Đóng góp tích cực trong công tác đoàn thể",
                        SoTien = 700000
                    }
                );
                context.SaveChanges();
            }

            // ⚠️ DANH MỤC KỶ LUẬT
            if (!context.DanhMucKyLuat.Any())
            {
                context.DanhMucKyLuat.AddRange(
                    new DanhMucKyLuat
                    {
                        MaKL = "KL001",
                        TenKyLuat = "Đi làm trễ không lý do",
                        SoTienPhat = 200000
                    },
                    new DanhMucKyLuat
                    {
                        MaKL = "KL002",
                        TenKyLuat = "Nghỉ không phép",
                        SoTienPhat = 500000
                    },
                    new DanhMucKyLuat
                    {
                        MaKL = "KL003",
                        TenKyLuat = "Không hoàn thành công việc đúng hạn",
                        SoTienPhat = 300000
                    },
                    new DanhMucKyLuat
                    {
                        MaKL = "KL004",
                        TenKyLuat = "Vi phạm nội quy công ty",
                        SoTienPhat = 400000
                    },
                    new DanhMucKyLuat
                    {
                        MaKL = "KL005",
                        TenKyLuat = "Gây mất đoàn kết nội bộ",
                        SoTienPhat = 600000
                    }
                );
                context.SaveChanges();
            }

            // 📄 HỢP ĐỒNG
            if (!context.HopDong.Any())
            {
                context.HopDong.AddRange(
                    new HopDong
                    {
                        SoHopDong = "HD001",
                        LoaiHD = "Hợp đồng không xác định thời hạn",
                        NgayBatDau = new DateTime(2020, 5, 1),
                        NgayKetThuc = null,
                        LuongCoBan = 15000000,
                        PhuCapAnTrua = 1000000,
                        PhuCapXangXe = 800000,
                        PhuCapDienThoai = 500000,
                        PhuCapTrachNhiem = 1500000,
                        PhuCapKhac = 300000,
                        TrangThai = "Còn hiệu lực",
                        MaNV = "NV001"
                    },
                    new HopDong
                    {
                        SoHopDong = "HD002",
                        LoaiHD = "Hợp đồng xác định thời hạn 2 năm",
                        NgayBatDau = new DateTime(2021, 3, 15),
                        NgayKetThuc = new DateTime(2023, 3, 15),
                        LuongCoBan = 12000000,
                        PhuCapAnTrua = 800000,
                        PhuCapXangXe = 600000,
                        PhuCapDienThoai = 400000,
                        PhuCapTrachNhiem = 1000000,
                        PhuCapKhac = 200000,
                        TrangThai = "Còn hiệu lực",
                        MaNV = "NV002"
                    },
                    new HopDong
                    {
                        SoHopDong = "HD003",
                        LoaiHD = "Hợp đồng xác định thời hạn 3 năm",
                        NgayBatDau = new DateTime(2019, 8, 20),
                        NgayKetThuc = new DateTime(2022, 8, 20),
                        LuongCoBan = 13000000,
                        PhuCapAnTrua = 900000,
                        PhuCapXangXe = 700000,
                        PhuCapDienThoai = 450000,
                        PhuCapTrachNhiem = 1200000,
                        PhuCapKhac = 250000,
                        TrangThai = "Còn hiệu lực",
                        MaNV = "NV003"
                    },
                    new HopDong
                    {
                        SoHopDong = "HD004",
                        LoaiHD = "Hợp đồng thử việc 2 tháng",
                        NgayBatDau = new DateTime(2022, 6, 10),
                        NgayKetThuc = new DateTime(2022, 8, 10),
                        LuongCoBan = 8000000,
                        PhuCapAnTrua = 500000,
                        PhuCapXangXe = 300000,
                        PhuCapDienThoai = 200000,
                        PhuCapTrachNhiem = 0,
                        PhuCapKhac = 0,
                        TrangThai = "Còn hiệu lực",
                        MaNV = "NV004"
                    }
                );
                context.SaveChanges();
            }

            // 🎖️ CHI TIẾT KHEN THƯỞNG
            if (!context.ChiTietKhenThuong.Any())
            {
                context.ChiTietKhenThuong.AddRange(
                    // NV001
                    new ChiTietKhenThuong
                    {
                        MaNV = "NV001",
                        MaKT = "KT001",
                        NgayKhenThuong = new DateTime(2024, 1, 15),
                        LyDo = "Nhân viên xuất sắc tháng 1"
                    },
                    new ChiTietKhenThuong
                    {
                        MaNV = "NV001",
                        MaKT = "KT004",
                        NgayKhenThuong = new DateTime(2024, 3, 10),
                        LyDo = "Sáng kiến cải tiến hiệu quả"
                    },
                    // NV002
                    new ChiTietKhenThuong
                    {
                        MaNV = "NV002",
                        MaKT = "KT002",
                        NgayKhenThuong = new DateTime(2024, 2, 20),
                        LyDo = "Nhân viên gương mẫu quý 1"
                    },
                    new ChiTietKhenThuong
                    {
                        MaNV = "NV002",
                        MaKT = "KT005",
                        NgayKhenThuong = new DateTime(2024, 4, 5),
                        LyDo = "Đóng góp tích cực trong công tác đoàn thể"
                    },
                    // NV003
                    new ChiTietKhenThuong
                    {
                        MaNV = "NV003",
                        MaKT = "KT003",
                        NgayKhenThuong = new DateTime(2024, 3, 25),
                        LyDo = "Hoàn thành dự án đúng hạn"
                    },
                    new ChiTietKhenThuong
                    {
                        MaNV = "NV003",
                        MaKT = "KT001",
                        NgayKhenThuong = new DateTime(2024, 5, 10),
                        LyDo = "Nhân viên xuất sắc tháng 5"
                    },
                    // NV004
                    new ChiTietKhenThuong
                    {
                        MaNV = "NV004",
                        MaKT = "KT002",
                        NgayKhenThuong = new DateTime(2024, 4, 15),
                        LyDo = "Nhân viên gương mẫu quý 2"
                    },
                    new ChiTietKhenThuong
                    {
                        MaNV = "NV004",
                        MaKT = "KT004",
                        NgayKhenThuong = new DateTime(2024, 6, 1),
                        LyDo = "Sáng kiến cải tiến hiệu quả"
                    }
                );
                context.SaveChanges();
            }

            // 📛 CHI TIẾT KỶ LUẬT
            if (!context.ChiTietKyLuat.Any())
            {
                context.ChiTietKyLuat.AddRange(
                    // NV001
                    new ChiTietKyLuat
                    {
                        MaNV = "NV001",
                        MaKL = "KL001",
                        NgayViPham = new DateTime(2024, 1, 10),
                        LyDo = "Đi làm trễ không lý do tháng 1"
                    },
                    new ChiTietKyLuat
                    {
                        MaNV = "NV001",
                        MaKL = "KL003",
                        NgayViPham = new DateTime(2024, 2, 5),
                        LyDo = "Không hoàn thành công việc đúng hạn"
                    },
                    // NV002
                    new ChiTietKyLuat
                    {
                        MaNV = "NV002",
                        MaKL = "KL002",
                        NgayViPham = new DateTime(2024, 3, 12),
                        LyDo = "Nghỉ không phép 1 ngày"
                    },
                    new ChiTietKyLuat
                    {
                        MaNV = "NV002",
                        MaKL = "KL005",
                        NgayViPham = new DateTime(2024, 4, 1),
                        LyDo = "Gây mất đoàn kết nội bộ"
                    },
                    // NV003
                    new ChiTietKyLuat
                    {
                        MaNV = "NV003",
                        MaKL = "KL001",
                        NgayViPham = new DateTime(2024, 2, 20),
                        LyDo = "Đi làm trễ không lý do tháng 2"
                    },
                    new ChiTietKyLuat
                    {
                        MaNV = "NV003",
                        MaKL = "KL004",
                        NgayViPham = new DateTime(2024, 3, 15),
                        LyDo = "Vi phạm nội quy công ty"
                    },
                    // NV004
                    new ChiTietKyLuat
                    {
                        MaNV = "NV004",
                        MaKL = "KL002",
                        NgayViPham = new DateTime(2024, 4, 18),
                        LyDo = "Nghỉ không phép 2 ngày"
                    },
                    new ChiTietKyLuat
                    {
                        MaNV = "NV004",
                        MaKL = "KL003",
                        NgayViPham = new DateTime(2024, 5, 10),
                        LyDo = "Không hoàn thành công việc đúng hạn"
                    }
                );
                context.SaveChanges();
            }

            // ⚙️ THAM SỐ HỆ THỐNG
            if (!context.ThamSoHeThong.Any())
            {
                context.ThamSoHeThong.AddRange(
                    // Biểu thuế TNCN
                    new ThamSoHeThong
                    {
                        MaTS = "TS001",
                        TenThamSo = "Biểu thuế Bậc 1 (Đến 5tr)",
                        GiaTri = "5.0",
                        NgayApDung = new DateTime(2014, 1, 1)
                    },
                    new ThamSoHeThong
                    {
                        MaTS = "TS002",
                        TenThamSo = "Biểu thuế Bậc 2 (Trên 5tr - 10tr)",
                        GiaTri = "10.0",
                        NgayApDung = new DateTime(2014, 1, 1)
                    },
                    new ThamSoHeThong
                    {
                        MaTS = "TS003",
                        TenThamSo = "Biểu thuế Bậc 3 (Trên 10tr - 18tr)",
                        GiaTri = "15.0",
                        NgayApDung = new DateTime(2014, 1, 1)
                    },
                    new ThamSoHeThong
                    {
                        MaTS = "TS004",
                        TenThamSo = "Biểu thuế Bậc 4 (Trên 18tr - 32tr)",
                        GiaTri = "20.0",
                        NgayApDung = new DateTime(2014, 1, 1)
                    },
                    new ThamSoHeThong
                    {
                        MaTS = "TS005",
                        TenThamSo = "Biểu thuế Bậc 5 (Trên 32tr - 52tr)",
                        GiaTri = "25.0",
                        NgayApDung = new DateTime(2014, 1, 1)
                    },
                    new ThamSoHeThong
                    {
                        MaTS = "TS006",
                        TenThamSo = "Biểu thuế Bậc 6 (Trên 52tr - 80tr)",
                        GiaTri = "30.0",
                        NgayApDung = new DateTime(2014, 1, 1)
                    },
                    new ThamSoHeThong
                    {
                        MaTS = "TS007",
                        TenThamSo = "Biểu thuế Bậc 7 (Trên 80tr)",
                        GiaTri = "35.0",
                        NgayApDung = new DateTime(2014, 1, 1)
                    },
                    // Hệ số tăng ca
                    new ThamSoHeThong
                    {
                        MaTS = "TS008",
                        TenThamSo = "Hệ số tăng ca ngày thường",
                        GiaTri = "1.5",
                        NgayApDung = new DateTime(2021, 1, 1)
                    },
                    new ThamSoHeThong
                    {
                        MaTS = "TS009",
                        TenThamSo = "Hệ số tăng ca cuối tuần",
                        GiaTri = "2.0",
                        NgayApDung = new DateTime(2021, 1, 1)
                    },
                    new ThamSoHeThong
                    {
                        MaTS = "TS010",
                        TenThamSo = "Hệ số tăng ca ngày lễ",
                        GiaTri = "3.0",
                        NgayApDung = new DateTime(2021, 1, 1)
                    }
                );
                context.SaveChanges();
            }

            // 📋 CHẤM CÔNG - Tháng 10/2024
            if (!context.ChamCong.Any())
            {
                var chamCongList = new List<ChamCong>();
                var random = new Random();

                // Danh sách nhân viên
                var danhSachNV = new[] { "NV001", "NV002", "NV003", "NV004" };

                // Tạo dữ liệu chấm công cho tháng 10/2024 (31 ngày)
                for (int ngay = 1; ngay <= 31; ngay++)
                {
                    var ngayChamCong = new DateTime(2024, 10, ngay);

                    // Bỏ qua thứ 7 và chủ nhật
                    if (ngayChamCong.DayOfWeek == DayOfWeek.Saturday ||
                        ngayChamCong.DayOfWeek == DayOfWeek.Sunday)
                        continue;

                    foreach (var maNV in danhSachNV)
                    {
                        // Random một số trường hợp đặc biệt
                        int tinhHuong = random.Next(100);

                        if (tinhHuong < 5) // 5% nghỉ không chấm công
                        {
                            continue;
                        }
                        else if (tinhHuong < 10) // 5% đi trễ
                        {
                            chamCongList.Add(new ChamCong
                            {
                                NgayChamCong = ngayChamCong,
                                GioVao = new TimeSpan(8, 30 + random.Next(15, 60), 0), // Trễ 15-60 phút
                                GioRa = new TimeSpan(17, 30, 0),
                                SoGioTangCa = 0,
                                MaNV = maNV
                            });
                        }
                        else if (tinhHuong < 15) // 5% về sớm
                        {
                            chamCongList.Add(new ChamCong
                            {
                                NgayChamCong = ngayChamCong,
                                GioVao = new TimeSpan(8, 30, 0),
                                GioRa = new TimeSpan(16, 30 - random.Next(10, 30), 0), // Về sớm 10-30 phút
                                SoGioTangCa = 0,
                                MaNV = maNV
                            });
                        }
                        else if (tinhHuong < 30) // 15% có tăng ca
                        {
                            decimal soGioTangCa = random.Next(1, 4); // 1-3 giờ tăng ca
                            chamCongList.Add(new ChamCong
                            {
                                NgayChamCong = ngayChamCong,
                                GioVao = new TimeSpan(8, 30, 0),
                                GioRa = new TimeSpan(17, 30 + (int)soGioTangCa, 0),
                                SoGioTangCa = soGioTangCa,
                                MaNV = maNV
                            });
                        }
                        else // 70% chấm công bình thường
                        {
                            chamCongList.Add(new ChamCong
                            {
                                NgayChamCong = ngayChamCong,
                                GioVao = new TimeSpan(8, 30, 0),
                                GioRa = new TimeSpan(17, 30, 0),
                                SoGioTangCa = 0,
                                MaNV = maNV
                            });
                        }
                    }
                }

                context.ChamCong.AddRange(chamCongList);
                context.SaveChanges();
            }
        }
    }
}