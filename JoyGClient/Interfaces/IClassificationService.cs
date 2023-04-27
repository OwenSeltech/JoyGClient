using JoyGClient.Data.Repositories;
using JoyGClient.DTOs;
using JoyGClient.Entities;

namespace JoyGClient.Interfaces
{
    public interface IClassificationService
    {
        Task<ResponseDto> AddClassification(RestaurantClassificationDto classificationDto);
        Task<ResponseDto> EditClassification(RestaurantClassificationDto classificationDto);
        Task<IEnumerable<RestaurantClassifications>> GetClassifications();
        Task<RestaurantClassifications> GetClassificationById(string classId);

    }
}
