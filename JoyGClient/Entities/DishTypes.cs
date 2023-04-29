namespace JoyGClient.Entities
{
    public class DishTypes
    {
        public int Id { get; set; }
        public string DishTypeName { get; set; }
        public AppUser CreatedBy { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public AppUser UpdatedBy { get; set; }
        public DateTime DateUpdated { get; set; } = DateTime.Now;
    }
}
