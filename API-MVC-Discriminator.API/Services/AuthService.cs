using API_MVC_Discriminator.API.Contracts;
using API_MVC_Discriminator.Persistence.Model;
using Asp.netCore_Identity.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API_MVC_Discriminator.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<UserApplication> _signInManager;
        private readonly UserManager<UserApplication> _userManager;

        public AuthService(UserManager<UserApplication> userManager,
            IConfiguration configuration, IMapper mapper, SignInManager<UserApplication> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _configuration = configuration;
            _mapper = mapper;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }



        public async Task<IdentityResult> SignUp(UserForCreateDto userForCreateDto)
        {
            var userToCreate = _mapper.Map<UserApplication>(userForCreateDto);

            var result = await _userManager.CreateAsync(userToCreate, userForCreateDto.Password);


            if (!result.Succeeded) return null;

            // Create Role Member For User
            var user = await _userManager.FindByNameAsync(userForCreateDto.UserName);
            var resultRoleCreate = await _userManager.AddToRolesAsync(user, new[] { "Member" });
            if (!resultRoleCreate.Succeeded) return null;
            return result;
        }
        public async Task<UserForReturnDto> LogIn(UserForLogInDto userForLogInDto)
        {
            var userFromRepo = await _userManager.FindByNameAsync(userForLogInDto.UserName);

            var result = await _signInManager.CheckPasswordSignInAsync(userFromRepo, userForLogInDto.Password, false);

            if (result.Succeeded)
            {
                var userApp = await _userManager.Users.FirstOrDefaultAsync(u => u.NormalizedUserName == userForLogInDto.UserName.ToUpper());

                var userToReturn = _mapper.Map<UserForReturnDto>(userApp);
                userToReturn.Token = GenerateJwtToken(userApp);

                return userToReturn;
            }
            return null;
        }
        public async Task<UserForReturnDto> GetUserByName(string name)
        {
            var user = await _userManager.FindByNameAsync(name);

            return _mapper.Map<UserForReturnDto>(user);
        }
        private string GenerateJwtToken(UserApplication user)
        {
            var claims = GetClaims(user);

            // genreated key and convert to bytes
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JWT:TokenKey").Value));

            // genreated Credentials by hashing key
            var Creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            // genreate Token Descriptor
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = Creds
            };

            // genreate token
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        private List<Claim> GetClaims(UserApplication user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.UserName)
            };

            var roles = GetUserRolesAsync(user);
            foreach (var role in roles.Result)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }

        private async Task<IList<string>> GetUserRolesAsync(UserApplication user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            return roles;
        }

    }
}
