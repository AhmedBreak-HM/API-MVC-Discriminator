using API_MVC_Discriminator.API.Contracts;
using Asp.netCore_Identity.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Asp.netCore_Identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthenticationController(IAuthService authService)
        {
            _authService = authService;
        }




        [HttpPost("signUp")]
        public async Task<ActionResult<UserForReturnDto>> SignUp(UserForCreateDto userForCreateDto)
        {
            var result = await _authService.SignUp(userForCreateDto);
            if (!result.Succeeded) return BadRequest(result.Errors);
            var userToReturn = _authService.GetUserByName(userForCreateDto.UserName);
            //CreatedAtAction("GetUserById", new { controller = "User", id = userToReturn.Id }, userToReturn);
            return Ok(userToReturn);
        }


        [HttpPost("logIn")]
        public async Task<ActionResult<UserForReturnDto>> LogIn(UserForLogInDto userForLogInDto)
        {
            var result = await _authService.LogIn(userForLogInDto);

            if (result != null) return Ok(result);

            return Unauthorized();

        }

    }
}
