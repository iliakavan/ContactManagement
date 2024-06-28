namespace ContactManagementV2.Services.ContactService.DTO;

public class UpdateContactdto
{
    public int? Id { get; set;}
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string Email { get; set; } = string.Empty;
    public required string PhoneNumber { get; set; }
    public int? CategoryId { get; set;}
}