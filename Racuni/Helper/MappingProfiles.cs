using AutoMapper;
using Racuni.Dto;
using Racuni.Models;

namespace Racuni.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<AccountHeader, AccountHeaderDto>();
        }
    }
}
