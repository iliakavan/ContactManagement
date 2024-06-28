using AutoMapper;
using ContactManagementV2.Models;
using ContactManagementV2.Services.CategoryService.DTO;

namespace ContactManagementV2.Profiles;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, CategoryDto>().ReverseMap();
        CreateMap<Category, GetCategorydto>().ReverseMap();
        CreateMap<Category, UpdateCategorydto>().ReverseMap();
    }
}
