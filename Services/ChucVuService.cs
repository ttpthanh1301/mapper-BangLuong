using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BangLuong.Data;
using BangLuong.Data.Entities;
using Microsoft.EntityFrameworkCore;
using static BangLuong.ViewModels.ChucVuViewModels;

namespace BangLuong.Services
{
    public class ChucVuService : IChucVuService
    {
        private readonly BangLuongDbContext _context;
        private readonly IMapper _mapper;

        public ChucVuService(BangLuongDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ChucVuViewModel>> GetAllAsync()
        {
            var chucVus = await _context.ChucVu.ToListAsync();
            return _mapper.Map<IEnumerable<ChucVuViewModel>>(chucVus);
        }

        public async Task<ChucVuViewModel?> GetByIdAsync(string id)
        {
            var chucVu = await _context.ChucVu.FirstOrDefaultAsync(x => x.MaCV == id);
            return chucVu == null ? null : _mapper.Map<ChucVuViewModel>(chucVu);
        }

        public async Task<bool> CreateAsync(ChucVuRequest request)
        {
            var entity = _mapper.Map<ChucVu>(request);
            _context.ChucVu.Add(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(string id, ChucVuViewModel viewModel)
        {
            if (id != viewModel.MaCV) return false;

            var entity = await _context.ChucVu.FindAsync(id);
            if (entity == null) return false;

            _mapper.Map(viewModel, entity);
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.ChucVu.FindAsync(id);
            if (entity == null) return false;

            _context.ChucVu.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
