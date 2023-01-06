using AutoMapper;
using SecretsSharing.DTO;
using SecretsSharing.Model;

namespace SecretsSharing.Profiles
{
    public class FileProfile: Profile
    {
        /// <summary>
        /// converts an object of one type of object containing information about a file
        /// into an object of another type containing information about a file
        /// </summary>
        public FileProfile()
        {
            CreateMap<UploadFileModel, File>()
                .ForMember(dst => dst.UserId,
                    opt => opt.MapFrom(src => src.UserId))
                .ForMember(dst => dst.IsDelete,
                    opt => opt.MapFrom(src => src.IsDelete))
                .ForMember(dst => dst.Id,
                    opt => opt.Ignore());

        }
    }
}