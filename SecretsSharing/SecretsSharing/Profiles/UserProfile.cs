using AutoMapper;
using SecretsSharing.DTO;
using SecretsSharing.Model;

namespace SecretsSharing.Profiles
{
    /// <summary>
    /// Map email in password from AuthModel in User 
    /// </summary>
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<AuthModel, User>()
                .ForMember(dst => dst.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dst => dst.Password, opt => opt.MapFrom(src => src.Password));
        }
    }
}