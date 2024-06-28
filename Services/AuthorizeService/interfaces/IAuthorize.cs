using ContactManagementV2.Common;
using ContactManagementV2.Services.AuthorizeService.DTO;

namespace ContactManagementV2.Services.AuthorizeService.interfaces;

public interface IAuthorize
{
    Task<ResultDto> Register(RegisterUserDto user);
    Task<ResultDto<string>> Login(LoginUserDto user);

}
