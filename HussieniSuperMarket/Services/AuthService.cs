using HussieniSuperMarket.Data;
using HussieniSuperMarket.Models;
using HussieniSuperMarket.Models.AuthDTO;
using HussieniSuperMarket.Services.Interface;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HussieniSuperMarket.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerator _jwtGenerator;

        public AuthService(AppDbContext db,IJwtTokenGenerator jwtGenerator,
            UserManager<ApplicationUser> userManager, RoleManager<IdentityRole>roleManager)
        {
            _db = db;
            _jwtGenerator = jwtGenerator;
            _userManager = userManager;
            _roleManager = roleManager;  
        }

        public async Task<bool> AssignRole(string email, string roleName)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
            if (user != null)
            {
                if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
                }
                await _userManager.AddToRoleAsync(user, roleName);
                return true;
            }
            return false;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user=_db.ApplicationUsers.FirstOrDefault(u=>u.UserName.ToLower()==loginRequestDto.Username.ToLower());

            bool isValid=await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);

            if (user==null || isValid==false)
            {
                return new LoginResponseDto()
                {
                    User = null,
                    Token = ""
                };

            }
            //If User Was Found  Generate JWT Token
          var roles=await _userManager.GetRolesAsync(user);
           var token= _jwtGenerator.GenerateToken(user,roles);

            UserDto userDto = new()
            {
                Email = user.Email,
                ID = user.Id,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber,
                Roles = roles.ToList()
            };
            LoginResponseDto loginResponseDto = new LoginResponseDto() 
            {
                User=userDto,
                Token=token
            
            };

            return loginResponseDto;

        }

        public async Task<string> Register(RegistrationRequestDto registrationRequestDto)
        {
            ApplicationUser user = new()
            {
                UserName = registrationRequestDto.Email,
                Email = registrationRequestDto.Email,
                NormalizedEmail = registrationRequestDto.Email.ToUpper(),
                Name = registrationRequestDto.Name,
                PhoneNumber = registrationRequestDto.PhoneNumber
            };
            try
            {
              var result=await _userManager.CreateAsync(user,registrationRequestDto.password);
              
                if(result.Succeeded)
                {
                    var userToReturn=_db.ApplicationUsers.First(u=>u.UserName == registrationRequestDto.Email);

                    UserDto userDto = new()
                    {
                        Email = userToReturn.Email,
                        ID = userToReturn.Id,
                        Name = userToReturn.Name,
                        PhoneNumber = userToReturn.PhoneNumber
                    };
                    return "" ;
                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }

            }
            catch (Exception ex)
            {

            }
            return "Error Encountered";
        }

        public async Task<int> GetCustomerCount()
        {
            var role = await _roleManager.FindByNameAsync("CUSTOMER");
            if (role == null)
                return 0;

            return await _userManager.Users.CountAsync(u => u.Roles.Any(r => r.RoleId == role.Id));
        }
    }
}
