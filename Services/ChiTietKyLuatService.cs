using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BangLuong.Data;
using Microsoft.EntityFrameworkCore;
using static BangLuong.ViewModels.ChiTietKyLuatViewModels;

namespace BangLuong.Services
{
    public class ChiTietKyLuatService : IChiTietKyLuatService
    {
        private readonly BangLuongDbContext _context;
        private readonly IMapper _mapper;

        public ChiTietKyLuatService(BangLuongDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ChiTietKyLuatViewModel>> GetAllAsync()
        {
            var list = await _context.ChiTietKyLuat
                .Include(x => x.DanhMucKyLuat)
                .Include(x => x.NhanVien)
                .ToListAsync();

            return _mapper.Map<IEnumerable<ChiTietKyLuatViewModel>>(list);
        }

        public async Task<ChiTietKyLuatViewModel?> GetByIdAsync(int id)
        {
            var entity = await _context.ChiTietKyLuat
                .Include(x => x.DanhMucKyLuat)
                .Include(x => x.NhanVien)
                .FirstOrDefaultAsync(x => x.MaCTKL == id);

            return _mapper.Map<ChiTietKyLuatViewModel?>(entity);
        }

        public async Task<bool> CreateAsync(ChiTietKyLuatRequest request)
        {
            var entity = _mapper.Map<ChiTietKyLuat>(request);
            _context.Add(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(int id, ChiTietKyLuatViewModel request)
        {
            if (id != request.MaCTKL) return false;
            var entity = _mapper.Map<ChiTietKyLuat>(request);
            _context.Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.ChiTietKyLuat.FindAsync(id);
            if (entity == null) return false;

            _context.ChiTietKyLuat.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
