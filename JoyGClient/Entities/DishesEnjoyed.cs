namespace JoyGClient.Entities
{
	public class DishesEnjoyed
	{
        public int Id { get; set; }
		public Dishes Dishes { get; set; }
		public string FlavourRating { get; set; }
		public string PresentationRating { get; set; }
		public string WineRating { get; set; }
		public AppUser User { get; set; }
        public Survey Survey { get; set; }
    }
}
