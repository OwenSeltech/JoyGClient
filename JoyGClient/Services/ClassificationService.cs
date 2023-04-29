using AutoMapper;
using JoyGClient.DTOs;
using JoyGClient.Entities;
using JoyGClient.Interfaces;
using JoyGClient.Models;

namespace JoyGClient.Services
{
    public class ClassificationService : IClassificationService
    {
        private readonly IRestaurantClassificationRepository _restaurantClassificationRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;

        public ClassificationService(IRestaurantClassificationRepository restaurantClassificationRepository, IMapper mapper,IUsersRepository usersRepository)
        {
            _mapper = mapper;
            _restaurantClassificationRepository = restaurantClassificationRepository;
            _usersRepository = usersRepository;
        }

        public async Task<ResponseDto> AddClassification(ClassificationModel classificationDto)
        {
            var responseDto = new ResponseDto();
            if (_restaurantClassificationRepository.ClassificationExists(classificationDto.ClassificationName))
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = false;
                responseDto.Message = "Classification Name Exists";
                return responseDto;
            }

            var user = await _usersRepository.GetUserByUsernameAsync(classificationDto.CreatedBy);
            if (user == null) 
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = false;
                responseDto.Message = "Please Login To add";
                return responseDto;
            }

            var restaurantClassification = new RestaurantClassifications();
            restaurantClassification.CreatedBy = user;
            restaurantClassification.UpdatedBy = user;
            restaurantClassification.ClassificationName = classificationDto.ClassificationName;
            if (await _restaurantClassificationRepository.AddClassificationAsync(restaurantClassification))
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = true;
                responseDto.Message = "Classification added successfully";
                return responseDto;
            }

            responseDto = new ResponseDto();
            responseDto.IsSuccess = false;
            responseDto.Message = "Failed to add Classification";
            return responseDto;

        }

        public async Task<ResponseDto> EditClassification(ClassificationModel classificationDto)
        {
            var responseDto = new ResponseDto();

            var classification = await _restaurantClassificationRepository.GetClassificationByIdAsync(classificationDto.Id);
            if (classification == null)
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = false;
                responseDto.Message = "Classification Does Not Exist";
                return responseDto;
            }

            if (_restaurantClassificationRepository.ClassificationExists(classificationDto.ClassificationName))
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = false;
                responseDto.Message = "Classification Name Exist";
                return responseDto;
            }

            var user = await _usersRepository.GetUserByUsernameAsync(classificationDto.CreatedBy);
            if (user == null)
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = false;
                responseDto.Message = "Please Login To add";
                return responseDto;
            }

            classification.CreatedBy = user;
            classification.UpdatedBy = user;
            classification.ClassificationName = classificationDto.ClassificationName;
            classification.DateUpdated = DateTime.Now;
            if (await _restaurantClassificationRepository.UpdateClassificationAsync(classification))
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = true;
                responseDto.Message = "Classification edited successfully";
                return responseDto;
            }

            responseDto = new ResponseDto();
            responseDto.IsSuccess = false;
            responseDto.Message = "Failed to edit Classification";
            return responseDto;

        }

        public async Task<IEnumerable<RestaurantClassifications>> GetClassifications()
        {
            var classifications = await _restaurantClassificationRepository.GetAllClassificationsAsync();
            return classifications;
        }
        public async Task<RestaurantClassifications> GetClassificationById(string classId)
        {
            var classifications = await _restaurantClassificationRepository.GetClassificationByIdAsync(classId);
            return classifications;
        }

    }
}
