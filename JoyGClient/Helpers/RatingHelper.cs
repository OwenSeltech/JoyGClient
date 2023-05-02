namespace JoyGClient.Helpers
{
    public class RatingHelper
    {
        public static string GetRating(string rating)
        {
            if (rating == "5") return "Strongly Agree";
            if (rating == "4") return "Agree";
            if (rating == "3") return "Neutral";
            if (rating == "2") return "Disagree";
            if (rating == "1") return "Strongly Disagree";
            return "";
        }
    }
}
