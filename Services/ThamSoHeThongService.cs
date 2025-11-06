using AutoMapper;
using BangLuong.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BangLuong.ViewModels.ThamSoHeThongViewModels;

namespace BangLuong.Services
{
    public class ThamSoHeThongService : IThamSoHeThongService
    {
        private readonly BangLuongDbContext _context;
        private readonly IMapper _mapper;

        public ThamSoHeThongService(BangLuongDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ThamSoHeThongViewModel>> GetAllAsync()
        {
            var list = await _context.ThamSoHeThong.ToListAsync();
            return _mapper.Map<IEnumerable<ThamSoHeThongViewModel>>(list);
        }

        public async Task<ThamSoHeThongViewModel?> GetByIdAsync(string id)
        {
            var entity = await _context.ThamSoHeThong.FindAsync(id);
            return entity == null ? null : _mapper.Map<ThamSoHeThongViewModel>(entity);
        }

        public async Task<bool> CreateAsync(ThamSoHeThongRequest request)
        {
            var entity = _mapper.Map<ThamSoHeThong>(request);
            _context.ThamSoHeThong.Add(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(string id, ThamSoHeThongViewModel request)
        {
            if (id != request.MaTS) return false;

            var entity = await _context.ThamSoHeThong.FindAsync(id);
            if (entity == null) return false;

            _mapper.Map(request, entity);
            _context.ThamSoHeThong.Update(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.ThamSoHeThong.FindAsync(id);
            if (entity == null) return false;

            _context.ThamSoHeThong.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
