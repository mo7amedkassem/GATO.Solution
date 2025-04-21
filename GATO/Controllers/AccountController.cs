using Gato.Core.Dtos;
using Gato.Core.Service_Contract;
using Gato.Repository.Data;
using Gato.Repository.Entities;
using Gato.Service;
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
        private readonly IAuthServices _authServices;
        private readonly StoreDBContext _dBContext;
        private readonly IEmailService _emailService;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager,IAuthServices authServices ,StoreDBContext dBContext , IEmailService emailService)
        {
            _userManager   = userManager;
            _signInManager = signInManager;
            _authServices  = authServices;
            _dBContext     = dBContext  ;
            _emailService  = emailService;
        }


        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login([FromBody]LoginDto Model)
        {
            var user = await _userManager.FindByEmailAsync(Model.Email);

            if (user is null)
                return Unauthorized();

            var result = await _signInManager.CheckPasswordSignInAsync(user, Model.Password, false);

            if (result.Succeeded is false) 
                return Unauthorized();

            return Ok(new UserDto
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _authServices.CreateTokenAsync(user, _userManager)
            });
        }



        [HttpPost("register")] // POST: /api/account/register 

        public async Task<ActionResult<UserDto>> Register(RegisterDto model)
        {


            var baseUsername = model.Email.Split("@")[0];
            var username = baseUsername;
            int counter = 1;

            while (await _userManager.FindByNameAsync(username) != null)
            {
                username = $"{baseUsername}{counter}";
                counter++;
            }

            var user = new User()
            {
                DisplayName = model.DisplayName,
                Email = model.Email, // mohamedahmedkassem@gmail.com 
                UserName = username, // mohamedahmedkassem 
                PhoneNumber = model.PhoneNumber 
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new { errors });
            }
            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _authServices.CreateTokenAsync(user, _userManager)
            });
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDTo model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return BadRequest(new { message = "User not found" });
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            

            await _userManager.SetAuthenticationTokenAsync(user, "PasswordReset", "Code", token);


            await _emailService.SendEmailAsync(user.Email, "Password Reset Code", $"Your password reset code: {token}");

            return Ok(new { message = "Password reset link has been sent to your email." });
        }


        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword( ResetPasswordDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return BadRequest("No user associated with this email");
            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new { errors });
            }
            return Ok("Password has been reset successfully.");
        }

    }
}
