using AutoMapper;
using OCAS.Domain.Common;
using OCASAPI.Application.Requests;

namespace OCASAPI.Application.Mappings
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Activity, ActivityDto>().ReverseMap();
            CreateMap<ActivityPersonResponse, ActivitySignUp>().ReverseMap();
            CreateMap<ActivityPersonRequest, ActivitySignUp>().ReverseMap();
            CreateMap<Activity, ActivityPersonResponse>().ReverseMap();
            CreateMap<Activity, ActivityPersonRequest>().ReverseMap();
            CreateMap<ActivitySignUp, ActivityPersonResponse>().ReverseMap();
            CreateMap<ActivitySignUp, ActivityPersonRequest>().ReverseMap();
            CreateMap<IReadOnlyList<Activity>, IReadOnlyList<ActivityPersonResponse>>().ReverseMap();

        }

    }
}