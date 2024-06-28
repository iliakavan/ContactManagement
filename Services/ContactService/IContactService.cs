using ContactManagementV2.Common;
using ContactManagementV2.Services.ContactService.DTO;

namespace ContactManagementV2.Services.ContactService;

public interface IContactService
{
    Task<ResultDto<GetContactdto>> Get(int id);
    Task<ResultDto<IEnumerable<GetContactdto>>> GetAll();
    Task<ResultDto> Update(int id, UpdateContactdto contactdto);
    Task<ResultDto> Create(Contactdto contactdto);
    Task<ResultDto> Delete(int id);

}