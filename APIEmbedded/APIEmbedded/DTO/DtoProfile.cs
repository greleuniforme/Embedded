using APIEmbedded.Models.Account.Register;
using AutoMapper;

namespace APIEmbedded.DTO
{
    public class DtoProfile : Profile
    {
        public DtoProfile()
        {
            CreateMap<RegisterUserModel, ApplicationUser>()
                .ForMember(dest => dest.UserName,
                    opts => opts.MapFrom(src => src.Email))
                .ReverseMap();

            //CreateMap<NewAccountNameModel, ApplicationUser>()
            //    .ForMember(dest => dest.UserName,
            //        opts => opts.MapFrom(src => src.NewEmail))
            //    .ForMember(dest => dest.Email,
            //        opts => opts.MapFrom(src => src.NewEmail))
            //    .ReverseMap();
        }
    }
}
