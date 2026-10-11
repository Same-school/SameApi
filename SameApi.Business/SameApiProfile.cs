using AutoMapper;
using SameApi.Dto;
using SameApi.Model;

namespace SameApi.Business
{
    public class SameApiProfile : Profile
    {
        public SameApiProfile()
        {
            //User
            CreateMap<UserInput, UserDao>();
            CreateMap<UserDao, UserResponse>()
                .ForMember(
                    dest => dest.CreatedAt,
                    opt => opt.MapFrom(src => src.CreatedAt.ToDateTime())
                );
        }
    }
}