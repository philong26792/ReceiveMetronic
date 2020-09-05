using AutoMapper;
using Receive_API.Dto;
using Receive_API.Models;

namespace Receive_API.Helpers.AutoMapper
{
    public class DtoToEfMappingProfile : Profile
    {
        public DtoToEfMappingProfile()
        {
            CreateMap<User_Dto, User>();
            CreateMap<Receive_Dto, Receive>();
        }
    }
}