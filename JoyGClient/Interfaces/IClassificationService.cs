using JoyGClient.DTOs;
using JoyGClient.Entities;
using JoyGClient.Models;

namespace JoyGClient.Interfaces
{
    public interface IClassificationService
    {
        Task<ResponseDto> AddClassification(ClassificationModel classificationDto);
        Task<ResponseDto> EditClassification(ClassificationModel classificationDto);
        Task<IEnumerable<RestaurantClassifications>> GetClassifications();
        Task<RestaurantClassifications> GetClassificationById(string classId);

    }
}
