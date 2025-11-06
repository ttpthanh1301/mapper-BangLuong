using AutoMapper;
using BangLuong.Data;
using BangLuong.Data.Entities;
using Microsoft.EntityFrameworkCore;
using static BangLuong.ViewModels.TongHopCongViewModels;

namespace BangLuong.Services
{
    public class TongHopCongService : ITongHopCongService
    {
        private readonly BangLuongDbContext _context;
        private readonly IMapper _mapper;

        public TongHopCongService(BangLuongDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TongHopCongViewModel>> GetAllAsync()
        {
            var list = await _context.TongHopCong.Include(x => x.NhanVien).ToListAsync();
            return _mapper.Map<IEnumerable<TongHopCongViewModel>>(list);
        }

        public async Task<TongHopCongViewModel?> GetByIdAsync(int id)
        {
            var entity = await _context.TongHopCong
                .Include(x => x.NhanVien)
                .FirstOrDefaultAsync(x => x.MaTHC == id);

            return _mapper.Map<TongHopCongViewModel>(entity);
        }

        public async Task<bool> CreateAsync(TongHopCongRequest request)
        {
            var entity = _mapper.Map<TongHopCong>(request);
            _context.TongHopCong.Add(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(int id, TongHopCongViewModel request)
        {
            var existing = await _context.TongHopCong.FirstOrDefaultAsync(x => x.MaTHC == id);
            if (existing == null)
                return false;

            _mapper.Map(request, existing);
            _context.TongHopCong.Update(existing);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.TongHopCong.FindAsync(id);
            if (entity == null)
                return false;

            _context.TongHopCong.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
