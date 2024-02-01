using AutoMapper;
using NZWalks.Api.Models;
using NZWalks.Api.Models.DTO;

namespace NZWalks.Api.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<UpdateRegionRequestDto, Region>().ReverseMap();
        }  
    }
}
