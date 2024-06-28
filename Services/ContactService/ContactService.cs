using AutoMapper;
using ContactManagementV2.Common;
using ContactManagementV2.Models;
using ContactManagementV2.Repository.interfaces;
using ContactManagementV2.Services.ContactService.DTO;

namespace ContactManagementV2.Services.ContactService;

public class ContactService : IContactService
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public ContactService(IUnitOfWork uow,IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<ResultDto> Create(Contactdto contactdto)
    {

        var contact = _mapper.Map<Contact>(contactdto);

        _uow.Contact.Add(contact);
        await _uow.Save();
        return new ResultDto(){IsSuccessfull = true};
    }

    public async Task<ResultDto> Delete(int id)
    {
        var result = await _uow.Contact.GetByIdAsync(id);
        if(result == null) 
        {
            return new ResultDto() { IsSuccessfull = false };
        }
        _uow.Contact.Remove(result);
        await _uow.Save();
        return new ResultDto() { IsSuccessfull = true };
    }

    public async Task<ResultDto<GetContactdto>> Get(int id)
    {
        var contact = await _uow.Contact.GetByIdAsync(id);
        if(contact == null)
        {
            return new ResultDto<GetContactdto>(){IsSuccessfull = false};
        }
        return new ResultDto<GetContactdto>()
        {
            IsSuccessfull = true,Value = _mapper.Map<GetContactdto>(contact)
        };
    }

    public async Task<ResultDto<IEnumerable<GetContactdto>>> GetAll()
    {
        var contact = await _uow.Contact.GetAllAsync();
        if(contact == null)
        {
            return new ResultDto<IEnumerable<GetContactdto>>(){IsSuccessfull = false};
        }
        return new ResultDto<IEnumerable<GetContactdto>>()
        {
            IsSuccessfull = true,
            Value = _mapper.Map<IEnumerable<GetContactdto>>(contact)
        };
    }

    public async Task<ResultDto> Update(int id, UpdateContactdto contactdto)
    {
        var model = await _uow.Contact.GetByIdAsync(id);
        if(model == null) 
        {
            return new ResultDto() { IsSuccessfull=false };
        }

        _mapper.Map(contactdto,model);
        _uow.Contact.Update(model);
        await _uow.Save();
        return new ResultDto() { IsSuccessfull = true };
    }
}