using AutoMapper;
using Receive_API.Dto;
using Receive_API.Models;

namespace Receive_API.Helpers.AutoMapper
{
    public class EfToDtoMappingProfile : Profile
    {
        public EfToDtoMappingProfile()
        {
            CreateMap<User, User_Dto>();
        }
    }
}