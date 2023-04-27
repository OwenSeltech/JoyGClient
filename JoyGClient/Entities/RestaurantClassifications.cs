namespace JoyGClient.Entities
{
    public class RestaurantClassifications
    {
        public int Id { get; set; }
        public string ClassificationName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public string UpdatedBy { get; set; }
        public DateTime DateUpdated { get; set; } = DateTime.Now;
    }
}
