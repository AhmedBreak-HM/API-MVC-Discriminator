using Asp.netCore_Identity.Dtos;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace API_MVC_Discriminator.API.Contracts
{
    public interface IAuthService
    {
        Task<IdentityResult> SignUp(UserForCreateDto userForCreateDto);
        Task<UserForReturnDto> LogIn(UserForLogInDto userForLogInDto);
        Task<UserForReturnDto> GetUserByName(string name);
    }
}
