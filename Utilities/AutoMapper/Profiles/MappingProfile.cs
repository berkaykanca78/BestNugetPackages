using AutoMapper;
using BaseWebApp.Models;
using BaseWebApp.ViewModels;

namespace BaseWebApp.Utilities.AutoMapper.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Student, StudentDto>()
                .ForMember(destination => destination.FullName, operation => operation.MapFrom(source => source.Name + " " + source.Surname))
                .ReverseMap();
        }
    }
}
