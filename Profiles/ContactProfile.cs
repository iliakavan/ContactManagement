using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ContactManagementV2.Models;
using ContactManagementV2.Services.ContactService.DTO;

namespace ContactManagementV2.Profiles;

public class ContactProfile : Profile
{
    public ContactProfile()
    {
        CreateMap<GetContactdto , Contact>().ReverseMap();
        CreateMap<UpdateContactdto , Contact>().ReverseMap();
        CreateMap<Contactdto , Contact>().ReverseMap();
    }
}
