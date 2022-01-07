using API_MVC_Discriminator.Persistence.Model;
using Asp.netCore_Identity.Dtos;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.netCore_Identity.Helper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<UserForCreateDto, UserApplication>();
            CreateMap<UserApplication, UserForReturnDto>();
        }
    }
}
