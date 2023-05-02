using JoyGClient.DTOs;
using JoyGClient.Entities;
using JoyGClient.Interfaces;
using JoyGClient.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace JoyGClient.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IPreferenceService _preferenceService;
        private readonly IClassificationService _classificationService;


        public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService, IPreferenceService preferenceService, IClassificationService classificationService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _preferenceService = preferenceService;
            _classificationService = classificationService;
        }
        public async Task<UserDto> Login(LoginModel loginDto)
        {
            var userDto = new UserDto();

            var user = await _userManager.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.Username);

            if (user == null)
            {
                userDto.Message = "Invalid Username";
                return userDto;
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded)
            {
                userDto.Message = "Invalid Password";
                return userDto;
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
            };
            var roles = await _userManager.GetRolesAsync(user);
            userDto.Roles = (IList<string>)roles;
            userDto.Message = "Success";
            userDto.Username = user.UserName;
            userDto.Token = await _tokenService.CreateToken(claims);
            userDto.claims = claims;

            return userDto;
        }

        public async Task<UserDto> Register(RegisterModel registerDto)
        {
            Guid userGuid = Guid.NewGuid();
            var userDto = new UserDto();
            if (await AppUserExists(registerDto.PhoneNumber))
            {
                userDto.Message = "Phone number Exists!";
                return userDto;
            }
            if (await IDExists(registerDto.IDNumber))
            {
                userDto.Message = "ID Number Exists!";
                return userDto;
            }

            var user = new AppUser
            {
                Name = registerDto.FirstName,
                Surname = registerDto.Surname,
                UserName = registerDto.PhoneNumber,
                PhoneNumber = registerDto.PhoneNumber,
                IDNumber = registerDto.IDNumber,
                DateOfBirth = registerDto.DateOfBirth,
                Address = registerDto.Address,
                Province = registerDto.Province.ToString(),
                IsEmployed = registerDto.IsEmployed,
                CreatedBy = registerDto.CreatedBy,
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                userDto.Message = result.Errors.ToString();
                return userDto;
            }

            var roleResult = await _userManager.AddToRoleAsync(user, registerDto.UserRole);
            if (!roleResult.Succeeded)
            {
                userDto.Message = result.Errors.ToString();
                return userDto;
            }

            if (!string.IsNullOrEmpty(registerDto.SelectedOptions))
            {
                var optionsArray = registerDto.SelectedOptions.Split(',');
                for (int i = 0; i < optionsArray.Length; i++)
                {
                    var classification = await _classificationService.GetClassificationByName(optionsArray.ElementAt(i).Trim());
                    var preference = new Preferences();
                    preference.AppUser = user;
                    preference.Classifications = classification;

                    var insertResult = await _preferenceService.AddPreference(preference);
                }
            }

            var claims = new List<Claim>
           {
               new Claim(JwtRegisteredClaimNames.NameId,user.Id.ToString()),
               new Claim(JwtRegisteredClaimNames.UniqueName,user.UserName),
           };
            var roles = await _userManager.GetRolesAsync(user);
            userDto.Roles = (IList<string>)roles;
            userDto.Message = "Success";
            userDto.Username = user.UserName;
            userDto.Token = await _tokenService.CreateToken(claims);
            userDto.claims = claims;
            return userDto;
        }

        private async Task<bool> AppUserExists(string number)
        {
            return _userManager.Users.Any(e => e.PhoneNumber.ToLower() == number.ToLower());
        }

        private async Task<bool> IDExists(string number)
        {
            return _userManager.Users.Any(e => e.PhoneNumber.ToLower() == number.ToLower());
        }
    }
}
