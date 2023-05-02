using JoyGClient.Entities;

namespace JoyGClient.Interfaces
{
	public interface ISurveyRepository
	{
		Task<bool> AddSurveyAsync(Survey survey);
		Task<bool> UpdateSurveyAsync(Survey survey);
		Task<IEnumerable<Survey>> GetAllSurveysAsync();
		Task<Survey> GetSurveyByIdAsync(string id);
		Task<IEnumerable<Survey>> GetAllSurveysByUserAsync(AppUser appUser);
		
	}
}
