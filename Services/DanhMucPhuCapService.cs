using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using BangLuong.Data;
using BangLuong.Data.Entities;
using static BangLuong.ViewModels.DanhMucPhuCapViewModels;

public class DanhMucPhuCapService : IDanhMucPhuCapService
{
    private readonly BangLuongDbContext _context;
    private readonly IMapper _mapper;

    public DanhMucPhuCapService(BangLuongDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<DanhMucPhuCapViewModel>> GetAllAsync()
    {
        var list = await _context.DanhMucPhuCap.ToListAsync();
        return _mapper.Map<IEnumerable<DanhMucPhuCapViewModel>>(list);
    }

    public async Task<DanhMucPhuCapViewModel?> GetByIdAsync(string id)
    {
        var entity = await _context.DanhMucPhuCap.FirstOrDefaultAsync(m => m.MaPC == id);
        return entity == null ? null : _mapper.Map<DanhMucPhuCapViewModel>(entity);
    }

    public async Task CreateAsync(DanhMucPhuCapRequest request)
    {
        var entity = _mapper.Map<DanhMucPhuCap>(request);
        _context.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(DanhMucPhuCapViewModel viewModel)
    {
        _context.Update(_mapper.Map<DanhMucPhuCap>(viewModel));
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(string id)
    {
        var entity = await _context.DanhMucPhuCap.FindAsync(id);
        if (entity != null)
        {
            _context.DanhMucPhuCap.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(string id)
    {
        return await _context.DanhMucPhuCap.AnyAsync(e => e.MaPC == id);
    }
}
