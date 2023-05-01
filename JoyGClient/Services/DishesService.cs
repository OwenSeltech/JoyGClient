using AutoMapper;
using JoyGClient.DTOs;
using JoyGClient.Entities;
using JoyGClient.Interfaces;
using JoyGClient.Models;

namespace JoyGClient.Services
{
    public class DishesService : IDishesService
    {
        private readonly IDishesRepository _dishesRepository;
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IDishTypeRepository _dishTypeRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;

        public DishesService(IDishesRepository dishesRepository, IMapper mapper, IUsersRepository usersRepository, IRestaurantRepository restaurantRepository, IDishTypeRepository dishTypeRepository)
        {
            _mapper = mapper;
            _dishesRepository = dishesRepository;
            _usersRepository = usersRepository;
            _restaurantRepository = restaurantRepository;
            _dishTypeRepository = dishTypeRepository;
        }

        public async Task<ResponseDto> AddDish(DishModel dishModel)
        {
            var responseDto = new ResponseDto();
            if (_dishesRepository.DishExists(dishModel.DishName))
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = false;
                responseDto.Message = "Dish Name Exists";
                return responseDto;
            }

            var user = await _usersRepository.GetUserByUsernameAsync(dishModel.CreatedBy);
            if (user == null)
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = false;
                responseDto.Message = "Please Login To add";
                return responseDto;
            }

            var restaurant = await _restaurantRepository.GetRestaurantsByIdAsync(dishModel.RestaurantId);
            if (restaurant == null)
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = false;
                responseDto.Message = "Please Select Restaurant";
                return responseDto;
            }

            var dishType = await _dishTypeRepository.GetDishTypeByIdAsync(dishModel.SelectedDishTypeId.ToString());
            if (dishType == null)
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = false;
                responseDto.Message = "Please Dish Type";
                return responseDto;
            }

            var dish = new Dishes();
            dish.CreatedBy = user;
            dish.UpdatedBy = user;
            dish.DishName = dishModel.DishName;
            dish.DishDescription = dishModel.DishDescription;
            dish.Restaurant = restaurant;
            dish.DishType = dishType;
            if (await _dishesRepository.AddDishAsync(dish))
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = true;
                responseDto.Message = "Dish added successfully";
                return responseDto;
            }

            responseDto = new ResponseDto();
            responseDto.IsSuccess = false;
            responseDto.Message = "Failed to add Dish";
            return responseDto;

        }
        public async Task<ResponseDto> EditDish(DishModel dishModel)
        {
            var responseDto = new ResponseDto();

            var dish = await _dishesRepository.GetDishByIdAsync(dishModel.Id);
            if (dish == null)
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = false;
                responseDto.Message = "Dish Does Not Exist";
                return responseDto;
            }
         

            var user = await _usersRepository.GetUserByUsernameAsync(dishModel.UpdatedBy);
            if (user == null)
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = false;
                responseDto.Message = "Please Login To add";
                return responseDto;
            }

            var restaurant = await _restaurantRepository.GetRestaurantsByIdAsync(dishModel.RestaurantId);
            if (restaurant == null)
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = false;
                responseDto.Message = "Please Select Restaurant";
                return responseDto;
            }

            var dishType = await _dishTypeRepository.GetDishTypeByIdAsync(dishModel.SelectedDishTypeId.ToString());
            if (dishType == null)
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = false;
                responseDto.Message = "Please Dish Type";
                return responseDto;
            }

            dish.UpdatedBy = user;
            dish.DishName = dishModel.DishName;
            dish.DishDescription = dishModel.DishDescription;
            dish.Restaurant = restaurant;
            dish.DishType = dishType;
            dish.DateUpdated = DateTime.Now;
            if (await _dishesRepository.UpdateDishAsync(dish))
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = true;
                responseDto.Message = "Dish edited successfully";
                return responseDto;
            }

            responseDto = new ResponseDto();
            responseDto.IsSuccess = false;
            responseDto.Message = "Dish to edit Classification";
            return responseDto;

        }
        public async Task<IEnumerable<Dishes>> GetDishess()
        {
            var dishes = await _dishesRepository.GetAllDishesAsync();
            return dishes;
        }
        public async Task<Dishes> GetDishById(string id)
        {
            var dishes = await _dishesRepository.GetDishByIdAsync(id);
            return dishes;
        }
        public async Task<IEnumerable<Dishes>> GetDishesByRestaurantAsync(Restaurant restaurant)
        {
            var dishes = await _dishesRepository.GetDishesByRestaurantAsync(restaurant);
            return dishes;
        }

        
    }
}
