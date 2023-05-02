using AutoMapper;
using JoyGClient.DTOs;
using JoyGClient.Entities;
using JoyGClient.Helpers;
using JoyGClient.Interfaces;
using JoyGClient.Models;

namespace JoyGClient.Services
{
	public class SurveyService : ISurveyService
	{
		private readonly IRestaurantRepository _restaurantRepository;
		private readonly ISurveyRepository _surveyRepository;
		private readonly IUsersRepository _usersRepository;
		private readonly IMapper _mapper;

		public SurveyService(IRestaurantRepository restaurantRepository, IMapper mapper, IUsersRepository usersRepository, ISurveyRepository surveyRepository)
		{
			_mapper = mapper;
			_restaurantRepository = restaurantRepository;
			_usersRepository = usersRepository;
			_surveyRepository = surveyRepository;
		}

		public async Task<ResponseDto> AddSurvey(SurveyModel surveyModel)
		{
			var responseDto = new ResponseDto();
			
			var user = await _usersRepository.GetUserByUsernameAsync(surveyModel.User);
			if (user == null)
			{
				responseDto = new ResponseDto();
				responseDto.IsSuccess = false;
				responseDto.Message = "Please Login To add";
				return responseDto;
			}

			var restaurant = await _restaurantRepository.GetRestaurantsByIdAsync(surveyModel.SelectedRestaurantId.ToString());
			if (restaurant == null)
			{
				responseDto = new ResponseDto();
				responseDto.IsSuccess = false;
				responseDto.Message = "Restaurant Not Found";
				return responseDto;
			}

			var survey = new Survey();
			survey.Restaurant = restaurant;
			survey.AmbienceRating = RatingHelper.GetRating(surveyModel.AmbienceRating);
			survey.Comments = surveyModel.Comments;
			survey.DateVisited = surveyModel.DateVisited;
			survey.OverallRating = RatingHelper.GetRating(surveyModel.OverallRating);
			survey.ServiceRating = RatingHelper.GetRating(surveyModel.ServiceRating);
			survey.User = user;
		
			if (await _surveyRepository.AddSurveyAsync(survey))
			{
				responseDto = new ResponseDto();
				responseDto.IsSuccess = true;
				responseDto.Message = survey.Id.ToString();
				return responseDto;
			}

			responseDto = new ResponseDto();
			responseDto.IsSuccess = false;
			responseDto.Message = "Failed to add Survey";
			return responseDto;

		}

		public async Task<Survey> GetClassificationById(string id)
		{
			var survey = await _surveyRepository.GetSurveyByIdAsync(id);
			return survey;
		}

    }
}
