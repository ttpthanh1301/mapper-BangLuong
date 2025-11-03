namespace BangLuong.ViewModels.AutoMapper;
using AutoMapper;
using BangLuong.Data.Entities;
using global::AutoMapper;
using static BangLuong.ViewModels.NhanVienViewModels;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<NhanVien, NhanVienViewModel>();
        CreateMap<NhanVienViewModel, NhanVien>();
        CreateMap<NhanVienRequest, NhanVien>();
    }
}

