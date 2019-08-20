using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RoutineCatalogue.Models.Entities;
using RoutineCatalogue.Models.Settings;
using RoutineCatalogue.Models.Types;
using RoutineCatalogue.Models.ViewModels;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
namespace RoutineCatalogue.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : ControllerBase
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly ApplicationSettings _appSettings;

        public UserController(RoleManager<Role> roleManager, UserManager<User> userManager, IOptions<ApplicationSettings> appSettings)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("Signin")]
        public async Task<IActionResult> Signin([FromBody]ApiSigninModel signInModel)
        {
            var user = await _userManager.FindByNameAsync(signInModel.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, signInModel.Password))
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Role, _userManager.GetRolesAsync(user).Result.FirstOrDefault())
                    }),
                    Expires = DateTime.UtcNow.AddHours(6),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.ApplicationSecret)), SecurityAlgorithms.HmacSha256)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);

                return Ok(new { BearerToken = token });
            }
            return Unauthorized();
        }

        [AllowAnonymous]
        [HttpPost("Signup")]
        public async Task<IActionResult> Signup([FromBody]ApiSigninModel signupModel)
        {
            var user = new User
            {
                UserName = signupModel.Email,
                Email = signupModel.Email,
                Role = await _roleManager.FindByNameAsync(RoleType.User.ToString())
            };
            var result = await _userManager.CreateAsync(user, signupModel.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, RoleType.User.ToString());
            }

            if (!result.Errors.Any())
                return await Signin(signupModel);
            return Unauthorized(new { message = result.Errors.ToList() });
        }
    }
}
