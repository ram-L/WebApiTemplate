using AutoMapper;
using WebApiTemplate.Application.Models.Client;
using WebApiTemplate.Application.Models.Common;
using WebApiTemplate.Application.Models.User;
using WebApiTemplate.Domain.Entities;

namespace NetCoreApiTemplate.Application.Mappings
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<Account, BaseUserDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.AccountType, opt => opt.MapFrom(src => src.AccountType))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.UserProfile != null ? src.UserProfile.Username : null))
                .ForMember(dest => dest.Firstname, opt => opt.MapFrom(src => src.UserProfile != null ? src.UserProfile.Firstname : null))
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.UserProfile != null ? src.UserProfile.Surname : null))
                .ForMember(dest => dest.Fullname, opt => opt.MapFrom(src => src.UserProfile != null ? src.UserProfile.Firstname + " " + src.UserProfile.Surname : null))
                .ForMember(dest => dest.ContactNo, opt => opt.MapFrom(src => src.UserProfile != null ? src.UserProfile.ContactNo : null))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.UserProfile != null ? src.UserProfile.Email : null))
                .ForMember(dest => dest.Website, opt => opt.MapFrom(src => src.UserProfile != null ? src.UserProfile.Website : null));

            CreateMap<Account, UserSummaryDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.AccountType, opt => opt.MapFrom(src => src.AccountType))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.UserProfile != null ? src.UserProfile.Username : null))
                .ForMember(dest => dest.Firstname, opt => opt.MapFrom(src => src.UserProfile != null ? src.UserProfile.Firstname : null))
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.UserProfile != null ? src.UserProfile.Surname : null))
                .ForMember(dest => dest.Fullname, opt => opt.MapFrom(src => src.UserProfile != null ? src.UserProfile.Firstname + " " + src.UserProfile.Surname : null))
                .ForMember(dest => dest.ContactNo, opt => opt.MapFrom(src => src.UserProfile != null ? src.UserProfile.ContactNo : null))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.UserProfile != null ? src.UserProfile.Email : null))
                .ForMember(dest => dest.Website, opt => opt.MapFrom(src => src.UserProfile != null ? src.UserProfile.Website : null))
                // Mapping for audit information
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
                .ForMember(dest => dest.CreatedById, opt => opt.MapFrom(src => src.CreatedById))
                .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => src.ModifiedDate))
                .ForMember(dest => dest.ModifiedById, opt => opt.MapFrom(src => src.ModifiedById))
                .ForMember(dest => dest.OwnerId, opt => opt.MapFrom(src => src.OwnerId));

            // Mapping from User entity to UserDetailDto
            CreateMap<Account, UserDetailDto>()
                .IncludeBase<Account, UserSummaryDto>();

            // Mapping from AddUserDto to User
            CreateMap<UserAddDto, Account>()
                .ForMember(dest => dest.OwnerId, opt => opt.MapFrom(src => src.OwnerId))
                .ForPath(dest => dest.UserProfile.Username, opt => opt.MapFrom(src => src.Username))
                .ForPath(dest => dest.UserProfile.Firstname, opt => opt.MapFrom(src => src.Firstname))
                .ForPath(dest => dest.UserProfile.Surname, opt => opt.MapFrom(src => src.Surname))
                .ForPath(dest => dest.UserProfile.Email, opt => opt.MapFrom(src => src.Email))
                .ForPath(dest => dest.UserProfile.ContactNo, opt => opt.MapFrom(src => src.ContactNo))
                .ForPath(dest => dest.UserProfile.Website, opt => opt.MapFrom(src => src.Website));

            // Mapping from UserUpdateDto to User
            CreateMap<UserUpdateDto, Account>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForPath(dest => dest.UserProfile.Firstname, opt => opt.MapFrom(src => src.Firstname))
                .ForPath(dest => dest.UserProfile.Surname, opt => opt.MapFrom(src => src.Surname))
                .ForPath(dest => dest.UserProfile.Email, opt => opt.MapFrom(src => src.Email))
                .ForPath(dest => dest.UserProfile.ContactNo, opt => opt.MapFrom(src => src.ContactNo))
                .ForPath(dest => dest.UserProfile.Website, opt => opt.MapFrom(src => src.Website));
        }
    }
}
