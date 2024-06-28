using AutoMapper;
using ContactManagementV2.Common;
using ContactManagementV2.Models;
using ContactManagementV2.Repository.interfaces;
using ContactManagementV2.Services.Cache;
using ContactManagementV2.Services.CategoryService.DTO;

namespace ContactManagementV2.Services.CategoryService;


public class CategoryService : ICategoryService
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public CategoryService(IUnitOfWork uow,IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }
    public async Task<ResultDto> Add(CategoryDto categoryDto)
    {
        if(categoryDto == null) 
        {
            return new ResultDto() { IsSuccessfull = false };
        }
        var category = _mapper.Map<Category>(categoryDto);
        _uow.Category.Add(category);
        await _uow.Save();
        return new ResultDto() { IsSuccessfull = true };

    }

    public async Task<ResultDto> Delete(int id)
    {
        var result = await _uow.Category.GetByIdAsync(id);
        if(result == null) 
        {
            return new ResultDto() { IsSuccessfull = false };
        }
        _uow.Category.Remove(result);
        await _uow.Save();
        return new ResultDto() { IsSuccessfull = true };
    }

    public async Task<ResultsDto<IEnumerable<GetCategorydto>>> GetAll()
    {
        var category = await _uow.Category.GetAllAsync();
        if (category == null)
        {
            return new ResultsDto<IEnumerable<GetCategorydto>> { IsSuccessfull = false };
        }
        return new ResultsDto<IEnumerable<GetCategorydto>>() 
        { 
            IsSuccessfull = true,
            Values = _mapper.Map<IEnumerable<GetCategorydto>>(category)
        };
    }

    public async Task<ResultDto<GetCategorydto>> GetbyId(int id)
    {
        var category = await _uow.Category.GetByIdAsync(id);
        if (category == null) 
        {
            return new ResultDto<GetCategorydto>() { IsSuccessfull=false };
        }
        return new ResultDto<GetCategorydto>() 
        {
            IsSuccessfull =true,
            Value = _mapper.Map<GetCategorydto>(category)
        };
    }

    public async Task<ResultDto> Update(int id, UpdateCategorydto categoryDto)
    {
        var model = await _uow.Category.GetByIdAsync(id);
        if(model == null) 
        {
            return new ResultDto() { IsSuccessfull=false };
        }

        _mapper.Map(categoryDto,model);
        _uow.Category.Update(model);
        await _uow.Save();
        return new ResultDto() { IsSuccessfull = true };
    }
}