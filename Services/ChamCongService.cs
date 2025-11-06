using AutoMapper;
using BangLuong.Data;
using BangLuong.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BangLuong.ViewModels.ChamCongViewModels;

namespace BangLuong.Services
{
    public class ChamCongService : IChamCongService
    {
        private readonly BangLuongDbContext _context;
        private readonly IMapper _mapper;

        public ChamCongService(BangLuongDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ChamCongViewModel>> GetAll()
        {
            var list = await _context.ChamCong.Include(c => c.NhanVien).ToListAsync();
            return _mapper.Map<IEnumerable<ChamCongViewModel>>(list);
        }

        public async Task<ChamCongViewModel> GetById(int id)
        {
            var chamCong = await _context.ChamCong
                .Include(c => c.NhanVien)
                .FirstOrDefaultAsync(c => c.MaCC == id);
            return _mapper.Map<ChamCongViewModel>(chamCong);
        }

        public async Task<int> Create(ChamCongRequest request)
        {
            var chamCong = _mapper.Map<ChamCong>(request);
            _context.ChamCong.Add(chamCong);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Update(ChamCongViewModel request)
        {
            if (!ChamCongExists(request.MaCC))
            {
                throw new KeyNotFoundException("Bản ghi chấm công không tồn tại");
            }

            _context.ChamCong.Update(_mapper.Map<ChamCong>(request));
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(int id)
        {
            var chamCong = await _context.ChamCong.FindAsync(id);
            if (chamCong != null)
            {
                _context.ChamCong.Remove(chamCong);
            }
            return await _context.SaveChangesAsync();
        }

        private bool ChamCongExists(int id)
        {
            return _context.ChamCong.Any(e => e.MaCC == id);
        }
    }
}
