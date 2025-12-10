using AutoMapper;
using BangLuong.Data;
using BangLuong.Data.Entities;
using BangLuong.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BangLuong.Services
{
    public class NguoiDungService : INguoiDungService
    {
        private readonly UserManager<NguoiDung> _userManager;
        private readonly SignInManager<NguoiDung> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly BangLuongDbContext _context;

        public NguoiDungService(
            UserManager<NguoiDung> userManager,
            SignInManager<NguoiDung> signInManager,
            RoleManager<IdentityRole> roleManager,
            IMapper mapper,
            BangLuongDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _context = context;
        }

        // ======================= AUTHENTICATE (Không còn dùng JWT) =======================
        // Phương thức này có thể xóa hoặc giữ cho API
        public async Task<string> Authenticate(NguoiDungViewModels.LoginRequest request)
        {
            // Tìm user bằng UserName (MaNV)
            var user = await _userManager.FindByNameAsync(request.MaNV);
            if (user == null)
                throw new Exception($"Không tìm thấy người dùng với mã nhân viên {request.MaNV}");

            // Kiểm tra trạng thái
            if (user.TrangThai != "Active")
                throw new Exception("Tài khoản đã bị vô hiệu hóa");

            // Kiểm tra mật khẩu
            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!result.Succeeded)
                throw new Exception("Đăng nhập không thành công. Vui lòng kiểm tra lại mật khẩu");

            // Return thông báo thành công (không trả JWT nữa)
            return "OK";
        }

        // ======================= REGISTER =======================
        public async Task<bool> Register(NguoiDungViewModels.NguoiDungRequest request)
        {
            // Kiểm tra nhân viên tồn tại
            var nhanVien = await _context.NhanVien.FirstOrDefaultAsync(x => x.MaNV == request.MaNV);
            if (nhanVien == null)
                throw new Exception("Nhân viên không tồn tại, không thể đăng ký user.");

            // Kiểm tra user đã tồn tại chưa
            var existingUser = await _userManager.FindByNameAsync(request.MaNV);
            if (existingUser != null)
                throw new Exception("Mã nhân viên này đã được đăng ký tài khoản.");

            // Kiểm tra password không null
            if (string.IsNullOrEmpty(request.Password))
                throw new Exception("Mật khẩu không được để trống");

            // ✅ FIX: Trim email để loại bỏ khoảng trắng
            var email = request.Email?.Trim() ?? string.Empty;

            // Tạo user mới với MaNV là Id
            var user = new NguoiDung
            {
                Id = request.MaNV.Trim(),
                UserName = request.MaNV.Trim(),
                Email = email,  // ✅ Dùng email đã trim
                PhanQuyen = request.PhanQuyen ?? "Employee",
                TrangThai = request.TrangThai ?? "Active"
            };

            // Debug log
            Console.WriteLine($"[DEBUG] Creating user: MaNV={user.Id}, UserName={user.UserName}, Email={user.Email}");

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                Console.WriteLine($"[ERROR] User creation failed: {errors}");
                throw new Exception($"Đăng ký thất bại: {errors}");
            }

            Console.WriteLine($"[DEBUG] User created successfully");

            // Gán role
            if (!string.IsNullOrEmpty(request.PhanQuyen))
            {
                if (!await _roleManager.RoleExistsAsync(request.PhanQuyen))
                    await _roleManager.CreateAsync(new IdentityRole(request.PhanQuyen));

                await _userManager.AddToRoleAsync(user, request.PhanQuyen);
            }

            return true;
        }

        // ======================= GET ALL =======================
        public async Task<IEnumerable<NguoiDungViewModels.NguoiDungViewModel>> GetAll()
        {
            var users = await _userManager.Users.ToListAsync();
            return users.Select(u => new NguoiDungViewModels.NguoiDungViewModel
            {
                MaNV = u.Id,
                Email = u.Email,
                UserName = u.UserName,
                PhanQuyen = u.PhanQuyen,
                TrangThai = u.TrangThai
            });
        }

        // ======================= GET BY ID =======================
        public async Task<NguoiDungViewModels.NguoiDungViewModel?> GetById(string maNV)
        {
            // Tìm bằng Id hoặc UserName
            var user = await _userManager.FindByIdAsync(maNV);
            if (user == null)
                user = await _userManager.FindByNameAsync(maNV);

            if (user == null)
                return null;

            return new NguoiDungViewModels.NguoiDungViewModel
            {
                MaNV = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                PhanQuyen = user.PhanQuyen,
                TrangThai = user.TrangThai
            };
        }

        // ======================= UPDATE =======================
        public async Task<bool> Update(NguoiDungViewModels.NguoiDungRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.MaNV);
            if (user == null)
                throw new Exception("Không tìm thấy người dùng");

            // Cập nhật thông tin
            user.PhanQuyen = request.PhanQuyen ?? user.PhanQuyen;
            user.TrangThai = request.TrangThai ?? user.TrangThai;

            // ✅ FIX: Trim email
            if (!string.IsNullOrEmpty(request.Email))
                user.Email = request.Email.Trim();

            // Cập nhật mật khẩu nếu có
            if (!string.IsNullOrEmpty(request.Password))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var resultPass = await _userManager.ResetPasswordAsync(user, token, request.Password);
                if (!resultPass.Succeeded)
                {
                    var errors = string.Join(", ", resultPass.Errors.Select(e => e.Description));
                    throw new Exception($"Cập nhật mật khẩu thất bại: {errors}");
                }
            }

            // Cập nhật role
            if (!string.IsNullOrEmpty(request.PhanQuyen))
            {
                var currentRoles = await _userManager.GetRolesAsync(user);
                if (currentRoles.Any())
                    await _userManager.RemoveFromRolesAsync(user, currentRoles);

                if (!await _roleManager.RoleExistsAsync(request.PhanQuyen))
                    await _roleManager.CreateAsync(new IdentityRole(request.PhanQuyen));

                await _userManager.AddToRoleAsync(user, request.PhanQuyen);
            }

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"Cập nhật thất bại: {errors}");
            }

            return true;
        }

        // ======================= DELETE =======================
        public async Task<bool> Delete(string maNV)
        {
            var user = await _userManager.FindByIdAsync(maNV);
            if (user == null)
                throw new Exception("Không tìm thấy người dùng");

            // Xóa roles
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.Any())
                await _userManager.RemoveFromRolesAsync(user, roles);

            // Xóa user
            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"Xóa thất bại: {errors}");
            }

            return true;
        }
    }
}