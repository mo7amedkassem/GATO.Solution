using Gato.Core.Dtos;
using Gato.Repository.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GATO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto Model)
        {
            var user = await _userManager.FindByEmailAsync(Model.Email);

            if (user is null)
                return Unauthorized();

            var result = await _signInManager.CheckPasswordSignInAsync(user, Model.Password, false);

            if (result.Succeeded is false) 
                return Unauthorized();

            return Ok(new UserDto
            {
                UserName = user.UserName,
                Email = user.Email,
                Token = "this will be token"
            });
        }
    }
}
