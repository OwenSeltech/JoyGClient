namespace JoyGClient.Entities
{
    public class Preferences
    {
        public int Id { get; set; }
        public AppUser User { get; set; }
        public List<RestaurantClassifications> Classifications { get; set; }
    }
}
