using ContactManagementV2.Models;

namespace ContactManagementV2.Services.CategoryService.DTO
{
    public class GetCategorydto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public ICollection<Contact>? contacts { get; }

    }
}
