using ContactManagementV2.Models;

namespace ContactManagementV2.Services.CategoryService.DTO
{
    public class UpdateCategorydto
    {
        public string? Name { get; set; }
        public ICollection<Contact>? contacts { get; set; }
    }
}
