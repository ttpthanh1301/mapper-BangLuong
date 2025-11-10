using System.Collections.Generic;

namespace BangLuong.ViewModels
{
    public class DashboardViewModel
    {
        // --- A. NHÓM CHỈ SỐ TỔNG QUAN ---
        public double TongChiPhiLuong { get; set; }
        public decimal TongLuongThucLanh { get; set; }
        public int TongNhanSu { get; set; }
        public double TongGioTangCa { get; set; }
        public int SoLuongKhenThuong { get; set; }
        public int SoLuongKyLuat { get; set; }

        // --- B. NHÓM PHÂN TÍCH CHI PHÍ ---
        public double TongLuongTheoNgayCong { get; set; }
        public double TongLuongTangCa { get; set; }
        public double TongPhuCap { get; set; }
        public double TongKhenThuong { get; set; }

        public List<LuongTheoPhongBan> LuongTheoPhongBan { get; set; } = new();
        public List<LuongTheoThang> XuHuongLuongThang { get; set; } = new();

        // --- C. NHÓM 'TOP N' ---
        public List<TopNhanVienLuong> TopLuongCao { get; set; } = new();
        public List<TopNhanVienOT> TopGioTangCa { get; set; } = new();
    }

    public class LuongTheoPhongBan
    {
        public string? TenPhongBan { get; set; }
        public double TongChiPhi { get; set; }
    }

    public class LuongTheoThang
    {
        public string? Thang { get; set; }
        public double TongChiPhi { get; set; }
    }

    public class TopNhanVienLuong
    {
        public string? HoTen { get; set; }
        public double TongThuNhap { get; set; }
    }

    public class TopNhanVienOT
    {
        public string? HoTen { get; set; }
        public double TongGioTangCa { get; set; }
    }
}
