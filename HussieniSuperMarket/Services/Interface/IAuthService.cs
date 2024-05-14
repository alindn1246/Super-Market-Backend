

using HussieniSuperMarket.Models.AuthDTO;

namespace HussieniSuperMarket.Services.Interface
{
    public interface IAuthService
    {
        Task<string> Register(RegistrationRequestDto registrationRequestDto);

        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);

        Task<int> GetCustomerCount();

        Task<bool> AssignRole(string email, string roleName);
    }
}
