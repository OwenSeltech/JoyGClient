namespace JoyGClient.Entities
{
    public class Preferences
    {
        public int Id { get; set; }
        public RestaurantClassifications Classifications { get; set; }
        public AppUser AppUser { get; set; }
    }
}
