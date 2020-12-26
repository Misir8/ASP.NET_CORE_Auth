using AutoMapper;
using WebApplication.DTOs;
using WebApplication.Models;

namespace WebApplication.AutoMapper
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<AppUser, RegisterDto>().ReverseMap();
        }
    }
}