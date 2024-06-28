using ContactManagementV2.Common;
using ContactManagementV2.Repository.interfaces;
using ContactManagementV2.Services.AuthorizeService.DTO;
using ContactManagementV2.Services.AuthorizeService.interfaces;
using Microsoft.AspNetCore.Identity;

namespace ContactManagementV2.Services.AuthorizeService
{
    public class AuthorizeService(
        UserManager<IdentityUser> userManager,
        ITokenRepository tokenRepository) : IAuthorize
    {
        private readonly UserManager<IdentityUser> _userManager = userManager;
        private readonly ITokenRepository _tr = tokenRepository;

        public async Task<ResultDto> Register(RegisterUserDto user)
        {
            var register_user = new IdentityUser
            {
                UserName = user.UserName,
                Email = user.EmailAddress
            };
            var result = await _userManager.CreateAsync(register_user,user.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(register_user, user.Role);
                return new ResultDto() { IsSuccessfull = true };
            }
            else
            {
                return new ResultDto() { IsSuccessfull = false };
            }
        }

        public async Task<ResultDto<string>> Login(LoginUserDto user)
        {
            var username = await _userManager.FindByNameAsync(user.UserName);
            if (username != null)
            {
                var password = await _userManager.CheckPasswordAsync(username, user.Password);
                if (password) 
                {
                    var role = await _userManager.GetRolesAsync(username);
                    
                    if(role != null) 
                    {
                        var token = _tr.CreateTokens(username,role.ToList());
                        return new ResultDto<string> { IsSuccessfull = true, Value = token };
                    }
                }
            }
            
            return new ResultDto<string>() { IsSuccessfull = false, Message = "user name or password is inncorrect" };
            
        }
    }
}
