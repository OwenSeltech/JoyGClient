using AutoMapper;
using JoyGClient.DTOs;
using JoyGClient.Entities;
using JoyGClient.Interfaces;
using JoyGClient.Models;

namespace JoyGClient.Services
{
    public class DishTypeService : IDishTypeService
    {
        private readonly IDishTypeRepository _dishTypeRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;

        public DishTypeService(IDishTypeRepository dishTypeRepository, IMapper mapper, IUsersRepository usersRepository)
        {
            _mapper = mapper;
            _dishTypeRepository = dishTypeRepository;
            _usersRepository = usersRepository;
        }

        public async Task<ResponseDto> AddDishType(DishTypeModel dishTypeModel)
        {
            var responseDto = new ResponseDto();
            if (_dishTypeRepository.DishTypeExists(dishTypeModel.DishTypeName))
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = false;
                responseDto.Message = "Dish Type Exists";
                return responseDto;
            }

            var user = await _usersRepository.GetUserByUsernameAsync(dishTypeModel.CreatedBy);
            if (user == null)
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = false;
                responseDto.Message = "Please Login To add";
                return responseDto;
            }

            var dishType = new DishTypes();
            dishType.CreatedBy = user;
            dishType.UpdatedBy = user;
            dishType.DishTypeName = dishTypeModel.DishTypeName;
            if (await _dishTypeRepository.AddDishTypeAsync(dishType))
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = true;
                responseDto.Message = "Dish Type added successfully";
                return responseDto;
            }

            responseDto = new ResponseDto();
            responseDto.IsSuccess = false;
            responseDto.Message = "Failed to add Dish Type";
            return responseDto;

        }
        public async Task<ResponseDto> EditClassification(DishTypeModel dishTypeModel)
        {
            var responseDto = new ResponseDto();

            var dishType = await _dishTypeRepository.GetDishTypeByIdAsync(dishTypeModel.Id);
            if (dishType == null)
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = false;
                responseDto.Message = "Dish Type Does Not Exist";
                return responseDto;
            }

            if (_dishTypeRepository.DishTypeExists(dishTypeModel.DishTypeName))
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = false;
                responseDto.Message = "Dish Type Name Exist";
                return responseDto;
            }

            var user = await _usersRepository.GetUserByUsernameAsync(dishTypeModel.CreatedBy);
            if (user == null)
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = false;
                responseDto.Message = "Please Login To add";
                return responseDto;
            }

            dishType.CreatedBy = user;
            dishType.UpdatedBy = user;
            dishType.DishTypeName = dishTypeModel.DishTypeName;
            dishType.DateUpdated = DateTime.Now;
            if (await _dishTypeRepository.UpdateDishTypeAsync(dishType))
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = true;
                responseDto.Message = "Dish Type edited successfully";
                return responseDto;
            }

            responseDto = new ResponseDto();
            responseDto.IsSuccess = false;
            responseDto.Message = "Failed to edit Dish Type";
            return responseDto;

        }
        public async Task<IEnumerable<DishTypes>> GetDishTypes()
        {
            var dishTypes = await _dishTypeRepository.GetAllDishTypesAsync();
            return dishTypes;
        }
        public async Task<DishTypes> GetDishTypeById(string typeId)
        {
            var dishType = await _dishTypeRepository.GetDishTypeByIdAsync(typeId);
            return dishType;
        }
    }
}
