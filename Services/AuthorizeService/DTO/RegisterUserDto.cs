using System.ComponentModel.DataAnnotations;

namespace ContactManagementV2.Services.AuthorizeService.DTO
{
    public class RegisterUserDto
    {
        [DataType(DataType.EmailAddress)]
        public required string EmailAddress { get; set; }

        public required string UserName {  get; set; }

        [DataType(DataType.Password)]
        public required string Password { get; set; }

        public string Role { get; set; }

    }
}
