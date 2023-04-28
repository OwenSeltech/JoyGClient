using JoyGClient.Entities;

namespace JoyGClient.Models
{
    public class ClassificationView
    {
        public IEnumerable<RestaurantClassifications> RestaurantClassifications { get; set; }
        public ClassificationModel ClassificationModel { get; set; }

}
}
