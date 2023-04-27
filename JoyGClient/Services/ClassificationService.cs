using AutoMapper;
using JoyGClient.Data.Repositories;
using JoyGClient.DTOs;
using JoyGClient.Entities;
using JoyGClient.Interfaces;

namespace JoyGClient.Services
{
    public class ClassificationService : IClassificationService
    {
        private readonly IRestaurantClassificationRepository _restaurantClassificationRepository;
        private readonly IMapper _mapper;

        public ClassificationService(IRestaurantClassificationRepository restaurantClassificationRepository, IMapper mapper)
        {
            _mapper = mapper;
            _restaurantClassificationRepository = restaurantClassificationRepository;
        }

        public async Task<ResponseDto> AddClassification(RestaurantClassificationDto classificationDto)
        {
            var responseDto = new ResponseDto();
            if (_restaurantClassificationRepository.ClassificationExists(classificationDto.ClassificationName))
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = false;
                responseDto.Message = "Classification Name Exists";
                return responseDto;
            }
               
            var restaurantClassification = new RestaurantClassifications();
            _mapper.Map(classificationDto, restaurantClassification);
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

        public async Task<ResponseDto> EditClassification(RestaurantClassificationDto classificationDto)
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
         
            _mapper.Map(classificationDto, classification);
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
