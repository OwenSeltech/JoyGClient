using AutoMapper;
using JoyGClient.DTOs;
using JoyGClient.Entities;
using JoyGClient.Interfaces;
using JoyGClient.Models;

namespace JoyGClient.Services
{
    public class PreferenceService : IPreferenceService
    {
        private readonly IRestaurantClassificationRepository _restaurantClassificationRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly IPreferenceRepository _preferenceRepository;
        private readonly IMapper _mapper;

        public PreferenceService(IRestaurantClassificationRepository restaurantClassificationRepository, IMapper mapper, IUsersRepository usersRepository, IPreferenceRepository preferenceRepository)
        {
            _mapper = mapper;
            _restaurantClassificationRepository = restaurantClassificationRepository;
            _usersRepository = usersRepository;
            _preferenceRepository  = preferenceRepository;
        }

        public async Task<ResponseDto> AddPreference(Preferences preference)
        {
            var responseDto = new ResponseDto();
           
            if (await _preferenceRepository.AddPreferenceAsync(preference))
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = true;
                responseDto.Message = "Preference added successfully";
                return responseDto;
            }

            responseDto = new ResponseDto();
            responseDto.IsSuccess = false;
            responseDto.Message = "Preference to add Classification";
            return responseDto;

        }
    }
}
