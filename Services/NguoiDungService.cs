using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BangLuong.Data;
using BangLuong.Data.Entities;
using Microsoft.EntityFrameworkCore;
using static BangLuong.ViewModels.NguoiDungViewModels;

namespace BangLuong.Services
{
    public class NguoiDungService : INguoiDungService
    {
        private readonly BangLuongDbContext _context;
        private readonly IMapper _mapper;

        public NguoiDungService(BangLuongDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<NguoiDungViewModel>> GetAllAsync()
        {
            var list = await _context.NguoiDung
                .Include(x => x.NhanVien)
                .ToListAsync();
            return _mapper.Map<IEnumerable<NguoiDungViewModel>>(list);
        }

        public async Task<NguoiDungViewModel?> GetByIdAsync(string id)
        {
            var entity = await _context.NguoiDung
                .Include(x => x.NhanVien)
                .FirstOrDefaultAsync(x => x.MaNV == id);
            return entity == null ? null : _mapper.Map<NguoiDungViewModel>(entity);
        }

        public async Task<bool> CreateAsync(NguoiDungRequest request)
        {
            var entity = _mapper.Map<NguoiDung>(request);
            _context.NguoiDung.Add(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(string id, NguoiDungViewModel viewModel)
        {
            if (id != viewModel.MaNV) return false;

            var entity = await _context.NguoiDung.FindAsync(id);
            if (entity == null) return false;

            _mapper.Map(viewModel, entity);
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.NguoiDung.FindAsync(id);
            if (entity == null) return false;

            _context.NguoiDung.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<string>> GetAllNhanVienIdsAsync()
        {
            return await _context.NhanVien.Select(x => x.MaNV).ToListAsync();
        }
    }
}
