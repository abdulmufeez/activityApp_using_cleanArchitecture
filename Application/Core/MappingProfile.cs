using AutoMapper;
using Domain;

namespace Application.Core
{
    public class MappingProfile : Profile
    {
        protected MappingProfile()
        {
            CreateMap<Activity,Activity>();
        }
    }
}