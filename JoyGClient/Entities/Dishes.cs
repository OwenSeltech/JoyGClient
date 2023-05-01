namespace JoyGClient.Entities
{
    public class Dishes
    {
        public int Id { get; set; }
        public string DishName { get; set; }
        public string DishDescription { get; set; }
        public Restaurant Restaurant { get; set; }

        public DishTypes DishType { get; set; }
        public AppUser CreatedBy { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public AppUser UpdatedBy { get; set; }
        public DateTime DateUpdated { get; set; } = DateTime.Now;
    }
}
