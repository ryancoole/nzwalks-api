using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // Use ForMember for custom values that do not match
            CreateMap<RegionDomain, RegionDto>().ReverseMap();
            CreateMap<RegionDomain, AddRegionRequestDto>().ReverseMap();
            CreateMap<RegionDomain, UpdateRegionRequestDto>().ReverseMap();
        }
    }
}
