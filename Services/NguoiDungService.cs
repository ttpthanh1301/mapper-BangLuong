using AutoMapper;
using BangLuong.Data.Entities;
using BangLuong.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace BangLuong.Services
{
    public class NguoiDungService : INguoiDungService
    {
        private readonly UserManager<NguoiDung> _userManager;
        private readonly SignInManager<NguoiDung> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly JwtOptions _jwtOptions;

        public NguoiDungService(UserManager<NguoiDung> userManager,
                                 SignInManager<NguoiDung> signInManager,
                                 RoleManager<IdentityRole> roleManager,
                                 IConfiguration config,
                                 IMapper mapper,
                                 IOptions<JwtOptions> jwtOptions)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _config = config;
            _mapper = mapper;
            _jwtOptions = jwtOptions.Value;
        }

        public async Task<string> Authenticate(NguoiDungViewModels.LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email!);
            if (user == null)
                throw new Exception($"Không tìm thấy người dùng với email {request.Email}");

            var result = await _signInManager.PasswordSignInAsync(user, request.Password!, request.RememberMe, true);
            if (!result.Succeeded)
                throw new Exception("Đăng nhập không thành công");

            var roles = await _userManager.GetRolesAsync(user);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Role, string.Join(";", roles))
            };
            var keyBytes = _jwtOptions.SigningKey != null ? Encoding.UTF8.GetBytes(_jwtOptions.SigningKey) : throw new ArgumentNullException(nameof(_jwtOptions.SigningKey));
            var key = new SymmetricSecurityKey(keyBytes);
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,        // "https://api.vnLab.com"
                audience: _jwtOptions.Audience,    // "https://api.vnLab.com"
                claims: claims,
                expires: DateTime.Now.AddHours(3), // token hết hạn sau 3 giờ
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<bool> Register(NguoiDungViewModels.NguoiDungRequest request)
        {
            var user = _mapper.Map<NguoiDung>(request);
            user.UserName = request.MaNV;

            var result = await _userManager.CreateAsync(user, request.Password!);
            if (!result.Succeeded) return false;

            // Thêm role nếu có
            if (!string.IsNullOrEmpty(request.PhanQuyen))
            {
                if (!await _roleManager.RoleExistsAsync(request.PhanQuyen))
                    await _roleManager.CreateAsync(new IdentityRole(request.PhanQuyen));

                await _userManager.AddToRoleAsync(user, request.PhanQuyen);
            }

            return true;
        }

        public async Task<IEnumerable<NguoiDungViewModels.NguoiDungViewModel>> GetAll()
        {
            var users = await _userManager.Users.ToListAsync();
            return _mapper.Map<IEnumerable<NguoiDungViewModels.NguoiDungViewModel>>(users);
        }

        public async Task<NguoiDungViewModels.NguoiDungViewModel?> GetById(string maNV)
        {
            var user = await _userManager.FindByIdAsync(maNV);
            return user == null ? null : _mapper.Map<NguoiDungViewModels.NguoiDungViewModel>(user);
        }

        public async Task<bool> Update(NguoiDungViewModels.NguoiDungRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.MaNV);
            if (user == null) return false;

            user.PhanQuyen = request.PhanQuyen;
            user.TrangThai = request.TrangThai;

            if (!string.IsNullOrEmpty(request.Password))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var resultPass = await _userManager.ResetPasswordAsync(user, token, request.Password);
                if (!resultPass.Succeeded) return false;
            }

            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }

        public async Task<bool> Delete(string maNV)
        {
            var user = await _userManager.FindByIdAsync(maNV);
            if (user == null) return false;

            // Xóa user khỏi tất cả role trước khi xóa user
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.Any())
                await _userManager.RemoveFromRolesAsync(user, roles);

            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }
    }
}
