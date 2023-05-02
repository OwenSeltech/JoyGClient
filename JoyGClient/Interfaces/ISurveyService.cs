using JoyGClient.Data.Repositories;
using JoyGClient.DTOs;
using JoyGClient.Entities;
using JoyGClient.Models;

namespace JoyGClient.Interfaces
{
	public interface ISurveyService
	{
		Task<ResponseDto> AddSurvey(SurveyModel surveyModel);
		Task<Survey> GetClassificationById(string id);
		
	}
}
