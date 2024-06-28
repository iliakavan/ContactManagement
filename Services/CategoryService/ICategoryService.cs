using ContactManagementV2.Common;
using ContactManagementV2.Models;
using ContactManagementV2.Services.CategoryService.DTO;


namespace ContactManagementV2.Services.CategoryService;

public interface ICategoryService
{
    Task<ResultDto> Add(CategoryDto categoryDto);
    Task<ResultDto> Update(int id, UpdateCategorydto categoryDto);
    Task<ResultDto> Delete(int id);
    Task<ResultDto<GetCategorydto>> GetbyId(int id);
    Task<ResultsDto<IEnumerable<GetCategorydto>>> GetAll();

}