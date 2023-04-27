using JoyGClient.Entities;

namespace JoyGClient.Interfaces
{
    public interface IRestaurantClassificationRepository
    {
       Task<bool> AddClassificationAsync(RestaurantClassifications restaurantClassification);
       Task<bool> UpdateClassificationAsync(RestaurantClassifications restaurantClassification);
       Task<IEnumerable<RestaurantClassifications>> GetAllClassificationsAsync();
       Task<RestaurantClassifications> GetClassificationByIdAsync(string id);
       bool ClassificationExists(string classification);
       Task<RestaurantClassifications> GetClassificationByNameAsync(string name);

    }
}
