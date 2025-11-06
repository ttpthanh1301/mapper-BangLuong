using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BangLuong.Data;
using BangLuong.Data.Entities;
using Microsoft.EntityFrameworkCore;
using static BangLuong.ViewModels.NguoiPhuThuocViewModels;

namespace BangLuong.Services
{
    public class NguoiPhuThuocService : INguoiPhuThuocService
    {
        private readonly BangLuongDbContext _context;
        private readonly IMapper _mapper;

        public NguoiPhuThuocService(BangLuongDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<NguoiPhuThuocViewModel>> GetAllAsync()
        {
            var list = await _context.NguoiPhuThuoc
                .Include(x => x.NhanVien)
                .ToListAsync();
            return _mapper.Map<IEnumerable<NguoiPhuThuocViewModel>>(list);
        }

        public async Task<NguoiPhuThuocViewModel?> GetByIdAsync(int id)
        {
            var entity = await _context.NguoiPhuThuoc
                .Include(x => x.NhanVien)
                .FirstOrDefaultAsync(x => x.MaNPT == id);
            return entity == null ? null : _mapper.Map<NguoiPhuThuocViewModel>(entity);
        }

        public async Task<bool> CreateAsync(NguoiPhuThuocRequest request)
        {
            var entity = _mapper.Map<NguoiPhuThuoc>(request);
            _context.NguoiPhuThuoc.Add(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(int id, NguoiPhuThuocViewModel viewModel)
        {
            if (id != viewModel.MaNPT) return false;
            var entity = await _context.NguoiPhuThuoc.FindAsync(id);
            if (entity == null) return false;

            _mapper.Map(viewModel, entity);
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.NguoiPhuThuoc.FindAsync(id);
            if (entity == null) return false;

            _context.NguoiPhuThuoc.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<string>> GetAllNhanVienIdsAsync()
        {
            return await _context.NhanVien.Select(x => x.MaNV).ToListAsync();
        }
    }
}
