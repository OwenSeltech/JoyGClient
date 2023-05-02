namespace JoyGClient.Entities
{
	public class Survey
	{
        public int Id { get; set; }
		public AppUser User { get; set; }
		public Restaurant Restaurant { get; set; }
		public DateTime DateVisited { get; set; }
		public string AmbienceRating { get; set; }
		public string ServiceRating { get; set; }
		public string OverallRating { get; set; }
		public string Comments { get; set; }
		public List<DishesEnjoyed>? DishesEnjoyed { get; set; }

	}
}
