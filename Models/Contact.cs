using System.ComponentModel.DataAnnotations.Schema;

namespace ContactManagementV2.Models;

public class Contact
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
    
    public Category? Category{ get; set;}
    public int? CategoryId { get; set;}

}