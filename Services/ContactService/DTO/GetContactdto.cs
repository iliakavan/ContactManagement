namespace ContactManagementV2.Services.ContactService.DTO;

public class GetContactdto
{
    public int Id { get; set;}
    public required string FirstName { get; set;}
    public required string LastName{ get; set;}
    public string FullName 
    {
        get { return $"{FirstName} {LastName}"; }
    }
    public string Email { get; set;} = string.Empty;
    public required string PhoneNumber { get; set;}
    
    public int? CategoryId { get; set;}
}