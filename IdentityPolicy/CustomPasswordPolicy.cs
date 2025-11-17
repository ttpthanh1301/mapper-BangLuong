// IdentityPolicy/CustomPasswordPolicy.cs
using Microsoft.AspNetCore.Identity;
using BangLuong.Data.Entities;

namespace BangLuong.IdentityPolicy
{
    public class CustomPasswordPolicy : PasswordValidator<NguoiDung>
    {
        public override async Task<IdentityResult> ValidateAsync(
            UserManager<NguoiDung> manager, 
            NguoiDung user, 
            string? password)
        {
            IdentityResult result = await base.ValidateAsync(manager, user, password);
            List<IdentityError> errors = result.Succeeded 
                ? new List<IdentityError>() 
                : result.Errors.ToList();

            // Không cho phép password chứa MaNV
            if (password!.ToLower().Contains(user.UserName!.ToLower()))
            {
                errors.Add(new IdentityError
                {
                    Description = "Mật khẩu không được chứa mã nhân viên"
                });
            }

            // Không cho phép dãy số 123
            if (password.Contains("123"))
            {
                errors.Add(new IdentityError
                {
                    Description = "Mật khẩu không được chứa dãy số 123"
                });
            }

            return errors.Count == 0 
                ? IdentityResult.Success 
                : IdentityResult.Failed(errors.ToArray());
        }
    }
}