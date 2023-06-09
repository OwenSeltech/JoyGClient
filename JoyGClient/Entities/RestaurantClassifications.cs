﻿namespace JoyGClient.Entities
{
    public class RestaurantClassifications
    {
        public int Id { get; set; }
        public string ClassificationName { get; set; }
        public AppUser CreatedBy { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public AppUser UpdatedBy { get; set; }
        public DateTime DateUpdated { get; set; } = DateTime.Now;
    }
}
