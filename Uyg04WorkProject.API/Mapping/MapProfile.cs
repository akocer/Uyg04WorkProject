using AutoMapper;
using Uyg04WorkProject.API.DTOs;
using Uyg04WorkProject.API.Models;

namespace Uyg04WorkProject.API.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Work, WorkDto>().ReverseMap();
            CreateMap<WorkStep, WorkStepDto>().ReverseMap();
            CreateMap<AppUser, UserDto>().ReverseMap();
        }
    }
}
