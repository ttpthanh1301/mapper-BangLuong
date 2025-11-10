using BangLuong.Data;
using BangLuong.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangLuong.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly BangLuongDbContext _context;

        public DashboardService(BangLuongDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardViewModel> GetDashboardDataAsync()
        {
            var model = new DashboardViewModel();

            // --- A. NHÓM CHỈ SỐ TỔNG QUAN ---
            model.TongChiPhiLuong = (double)await _context.BangTinhLuong
     .SumAsync(x => x.LuongCoBan + x.TongPhuCap + x.TongKhenThuong + x.LuongTangCa);


            model.TongLuongThucLanh = await _context.BangTinhLuong
        .SumAsync(x => x.ThucLanh);


            model.TongNhanSu = await _context.NhanVien
                .CountAsync(x => x.TrangThai == "Đang làm việc");

            model.TongGioTangCa = (double)await _context.TongHopCong
                .SumAsync(x =>
                    (x.SoGioTangCaNgayThuong ?? 0) +
                    (x.SoGioTangCaCuoiTuan ?? 0) +
                    (x.SoGioTangCaNgayLe ?? 0));

            model.SoLuongKhenThuong = await _context.ChiTietKhenThuong.CountAsync();
            model.SoLuongKyLuat = await _context.ChiTietKyLuat.CountAsync();

            // --- B. NHÓM PHÂN TÍCH CHI PHÍ ---

            model.TongLuongTheoNgayCong = (double)await _context.BangTinhLuong.SumAsync(x => x.LuongCoBan);
            model.TongLuongTangCa = (double)await _context.BangTinhLuong.SumAsync(x => x.LuongTangCa);
            model.TongPhuCap = (double)await _context.BangTinhLuong.SumAsync(x => x.TongPhuCap);
            model.TongKhenThuong = (double)await _context.BangTinhLuong.SumAsync(x => x.TongKhenThuong);


            // Biểu đồ: Chi phí lương theo phòng ban
            model.LuongTheoPhongBan = await _context.BangTinhLuong
                .Join(_context.NhanVien,
                    bl => bl.MaNV,
                    nv => nv.MaNV,
                    (bl, nv) => new { bl, nv })
                .Join(_context.PhongBan,
                    temp => temp.nv.MaPB,
                    pb => pb.MaPB,
                    (temp, pb) => new
                    {
                        TenPhongBan = pb.TenPB,
                        ChiPhi = temp.bl.ThucLanh 
                    })
                .GroupBy(x => x.TenPhongBan)
                .Select(g => new LuongTheoPhongBan
                {
                    TenPhongBan = g.Key,
                    TongChiPhi = (double)g.Sum(x => x.ChiPhi)
                })
                .ToListAsync();

            // Biểu đồ: Xu hướng chi phí lương 12 tháng gần nhất
            model.XuHuongLuongThang = await _context.BangTinhLuong
                .GroupBy(x => x.KyLuongThang) // cọt Tháng (1..12)
                .Select(g => new LuongTheoThang
                {
                    Thang = g.Key.ToString(),
                    TongChiPhi = (double)g.Sum(x => x.LuongCoBan  + (x.TongPhuCap ?? 0) + (x.TongKhenThuong ?? 0) + (x.LuongTangCa ?? 0))
                })
                .OrderBy(g => g.Thang)
                .ToListAsync();

            // --- C. NHÓM 'TOP N' ---
            model.TopLuongCao = await _context.BangTinhLuong
                .Join(_context.NhanVien,
                    bl => bl.MaNV,
                    nv => nv.MaNV,
                    (bl, nv) => new TopNhanVienLuong
                    {
                        HoTen = nv.HoTen,
                        TongThuNhap = (double)(bl.ThucLanh )
                    })
                .OrderByDescending(x => x.TongThuNhap)
                .Take(5)
                .ToListAsync();

            model.TopGioTangCa = await _context.TongHopCong
                .Join(_context.NhanVien,
                    th => th.MaNV,
                    nv => nv.MaNV,
                    (th, nv) => new TopNhanVienOT
                    {
                        HoTen = nv.HoTen,
                        TongGioTangCa = (double)((th.SoGioTangCaNgayThuong ?? 0)
                            + (th.SoGioTangCaCuoiTuan ?? 0)
                            + (th.SoGioTangCaNgayLe ?? 0))
                    })
                .OrderByDescending(x => x.TongGioTangCa)
                .Take(5)
                .ToListAsync();

            return model;
        }
    }
}
