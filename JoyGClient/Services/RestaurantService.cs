using AutoMapper;
using JoyGClient.DTOs;
using JoyGClient.Entities;
using JoyGClient.Interfaces;
using JoyGClient.Models;

namespace JoyGClient.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly IRestaurantClassificationRepository _restaurantClassificationRepository;

        private readonly IMapper _mapper;

        public RestaurantService(IRestaurantRepository restaurantRepository, IMapper mapper, IUsersRepository usersRepository, IRestaurantClassificationRepository restaurantClassificationRepository)
        {
            _mapper = mapper;
            _restaurantRepository = restaurantRepository;
            _usersRepository = usersRepository;
            _restaurantClassificationRepository = restaurantClassificationRepository;
        }

        public async Task<ResponseDto> AddRestaurant(RestaurantModel restaurantModel)
        {
            var responseDto = new ResponseDto();
            if (_restaurantRepository.RestaurantExists(restaurantModel.RestaurantName))
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = false;
                responseDto.Message = "Restaurant Name Exists";
                return responseDto;
            }

            var user = await _usersRepository.GetUserByUsernameAsync(restaurantModel.CreatedBy);
            if (user == null)
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = false;
                responseDto.Message = "Please Login To add";
                return responseDto;
            }

            var restarantClass = await _restaurantClassificationRepository.GetClassificationByIdAsync(restaurantModel.SelectedClassificationsId.ToString());
            var restaurant = new Restaurant();
            restaurant.CreatedBy = user;
            restaurant.UpdatedBy = user;
            restaurant.RestaurantName = restaurantModel.RestaurantName;
            restaurant.RestaurantClassification = restarantClass;

            if (await _restaurantRepository.AddRestaurantAsync(restaurant))
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = true;
                responseDto.Message = "Restaurant added successfully";
                return responseDto;
            }

            responseDto = new ResponseDto();
            responseDto.IsSuccess = false;
            responseDto.Message = "Failed to add Restaurant";
            return responseDto;

        }

        public async Task<ResponseDto> EditRestaurant(RestaurantModel restaurantModel)
        {
            var responseDto = new ResponseDto();

            var restaurant = await _restaurantRepository.GetRestaurantsByIdAsync(restaurantModel.Id);
            if (restaurant == null)
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = false;
                responseDto.Message = "Restaurant Does Not Exist";
                return responseDto;
            }

            var user = await _usersRepository.GetUserByUsernameAsync(restaurantModel.CreatedBy);
            if (user == null)
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = false;
                responseDto.Message = "Please Login To add";
                return responseDto;
            }

            var restarantClass = await _restaurantClassificationRepository.GetClassificationByIdAsync(restaurantModel.SelectedClassificationsId.ToString());

            restaurant.CreatedBy = user;
            restaurant.UpdatedBy = user;
            restaurant.RestaurantName = restaurantModel.RestaurantName;
            restaurant.RestaurantClassification = restarantClass;
            restaurant.DateUpdated = DateTime.Now;
            if (await _restaurantRepository.UpdateRestaurantAsync(restaurant))
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = true;
                responseDto.Message = "Restaurant edited successfully";
                return responseDto;
            }

            responseDto = new ResponseDto();
            responseDto.IsSuccess = false;
            responseDto.Message = "Restaurant to edit Classification";
            return responseDto;

        }

        public async Task<IEnumerable<Restaurant>> GetRestaurants()
        {
            var restaurants = await _restaurantRepository.GetAllRestaurantsAsync();
            return restaurants;
        }
        public async Task<Restaurant> GetRestaurantById(string restId)
        {
            var restaurants = await _restaurantRepository.GetRestaurantsByIdAsync(restId);
            return restaurants;
        }
    }
}
