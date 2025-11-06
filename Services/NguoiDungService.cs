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
        public async Task<PaginatedList<NguoiDungViewModel>> GetAllFilter(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? pageNumber,
            int pageSize)
        {
            if (searchString != null)
                pageNumber = 1;
            else
                searchString = currentFilter;

            var query = from nd in _context.NguoiDung
                        select nd;

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(nd =>
                    nd.MaNV.Contains(searchString) ||
                    nd.PhanQuyen.Contains(searchString) ||
                    nd.TrangThai.Contains(searchString)
                );
            }

            query = sortOrder switch
            {
                "ma_desc" => query.OrderByDescending(nd => nd.MaNV),
                "role" => query.OrderBy(nd => nd.PhanQuyen),
                "role_desc" => query.OrderByDescending(nd => nd.PhanQuyen),
                _ => query.OrderBy(nd => nd.MaNV)
            };

            var list = await query.ToListAsync();
            var viewModels = _mapper.Map<IEnumerable<NguoiDungViewModel>>(list);

            return PaginatedList<NguoiDungViewModel>.Create(viewModels, pageNumber ?? 1, pageSize);
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
