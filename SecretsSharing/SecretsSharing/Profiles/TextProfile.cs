using AutoMapper;
using SecretsSharing.DTO;
using SecretsSharing.Model;

namespace SecretsSharing.Profiles
{
    public class TextProfile : Profile
    {
        public TextProfile()
        {
            CreateMap<TextModel, UserText>()
                .ForMember(dst => dst.UserId,
                    opt => opt.MapFrom(src => src.UserId))
                .ForMember(dst => dst.Id, opt => opt.Ignore())
                .ForMember(dst => dst.Text,
                    opt => opt.MapFrom(src => src.Text));
        }
    }
}