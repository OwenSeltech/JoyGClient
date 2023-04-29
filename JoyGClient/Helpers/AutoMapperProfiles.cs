using AutoMapper;
using JoyGClient.Entities;
using JoyGClient.Models;

namespace JoyGClient.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<ClassificationModel, RestaurantClassifications>();
        }
    }
}
