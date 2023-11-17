using ProductPro.Models;
using ProductPro.Models.Dto;

namespace ProductPro.Repository
{
    public interface IUserRepository
    {
        bool IsUniqueUserName(string userName); 
       Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
       Task<LocalUser> Register (RegistrationRequestDto registrationRequestDto);
    }
}
