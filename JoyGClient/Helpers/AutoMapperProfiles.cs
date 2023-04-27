using AutoMapper;
using JoyGClient.DTOs;
using JoyGClient.Entities;

namespace JoyGClient.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<RestaurantClassificationDto, RestaurantClassifications>();
        }
    }
}
