using AutoMapper;
using StudentApi.Models;
using StudentApi.Dto;

namespace StudentApi.Mappings
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {


            CreateMap<CreateRequest, Student>();
        }
    }
}
