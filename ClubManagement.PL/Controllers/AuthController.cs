using ClubManagement.BLL.DTO;
using ClubManagement.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace UnviClubManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginDTO model)
        {
            try
            {
                var token = await _authService.Login(model);
                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegisterDTO model)
        {
            try
            {
                await _authService.Register(model);
                return Ok("User registered successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
