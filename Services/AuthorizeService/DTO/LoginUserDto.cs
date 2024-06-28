using System.ComponentModel.DataAnnotations;

namespace ContactManagementV2.Services.AuthorizeService.DTO;

public class LoginUserDto
{
    public required string UserName { get; set; }

    [DataType(DataType.Password)]
    public required string Password { get; set; }
}
