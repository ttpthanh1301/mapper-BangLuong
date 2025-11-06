using AutoMapper;
using BangLuong.Data;
using BangLuong.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BangLuong.ViewModels.HopDongViewModels;

namespace BangLuong.Services
{
    public class HopDongService : IHopDongService
    {
        private readonly BangLuongDbContext _context;
        private readonly IMapper _mapper;

        public HopDongService(BangLuongDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<HopDongViewModel>> GetAll()
        {
            var list = await _context.HopDong.Include(h => h.NhanVien).ToListAsync();
            return _mapper.Map<IEnumerable<HopDongViewModel>>(list);
        }

        public async Task<HopDongViewModel> GetById(int id)
        {
            var hopDong = await _context.HopDong
                .Include(h => h.NhanVien)
                .FirstOrDefaultAsync(h => h.MaHD == id);
            return _mapper.Map<HopDongViewModel>(hopDong);
        }

        public async Task<int> Create(HopDongRequest request)
        {
            var hopDong = _mapper.Map<HopDong>(request);
            _context.HopDong.Add(hopDong);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Update(HopDongViewModel request)
        {
            if (!HopDongExists(request.MaHD))
            {
                throw new KeyNotFoundException("Hợp đồng không tồn tại");
            }

            _context.HopDong.Update(_mapper.Map<HopDong>(request));
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(int id)
        {
            var hopDong = await _context.HopDong.FindAsync(id);
            if (hopDong != null)
            {
                _context.HopDong.Remove(hopDong);
            }
            return await _context.SaveChangesAsync();
        }

        private bool HopDongExists(int id)
        {
            return _context.HopDong.Any(e => e.MaHD == id);
        }
    }
}
