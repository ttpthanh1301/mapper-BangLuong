using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using BangLuong.Data;
using BangLuong.Data.Entities;
using static BangLuong.ViewModels.DanhMucKhenThuongViewModels;

public class DanhMucKhenThuongService : IDanhMucKhenThuongService
{
    private readonly BangLuongDbContext _context;
    private readonly IMapper _mapper;

    public DanhMucKhenThuongService(BangLuongDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<DanhMucKhenThuongViewModel>> GetAllAsync()
    {
        var list = await _context.DanhMucKhenThuong.ToListAsync();
        return _mapper.Map<IEnumerable<DanhMucKhenThuongViewModel>>(list);
    }

    public async Task<DanhMucKhenThuongViewModel?> GetByIdAsync(string id)
    {
        var entity = await _context.DanhMucKhenThuong
            .FirstOrDefaultAsync(x => x.MaKT == id);
        return entity == null ? null : _mapper.Map<DanhMucKhenThuongViewModel>(entity);
    }

    public async Task CreateAsync(DanhMucKhenThuongRequest request)
    {
        var entity = _mapper.Map<DanhMucKhenThuong>(request);
        _context.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(DanhMucKhenThuongViewModel viewModel)
    {
        _context.Update(_mapper.Map<DanhMucKhenThuong>(viewModel));
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(string id)
    {
        var entity = await _context.DanhMucKhenThuong.FindAsync(id);
        if (entity != null)
        {
            _context.DanhMucKhenThuong.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(string id)
    {
        return await _context.DanhMucKhenThuong.AnyAsync(e => e.MaKT == id);
    }
}
