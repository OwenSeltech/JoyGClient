namespace JoyGClient.Entities
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string RestaurantName { get; set; }
        public RestaurantClassifications RestaurantClassification { get; set; }
        public List<Dishes> Dishes  { get; set; }
        public AppUser CreatedBy { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public AppUser UpdatedBy { get; set; }
        public DateTime DateUpdated { get; set; } = DateTime.Now;
    }
}
