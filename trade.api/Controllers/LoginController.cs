using Microsoft.AspNetCore.Mvc;
using trade.api.Models.DTOs.LoginDTOs;
using trade.api.Services;

namespace trade.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly LoginService _loginService;

        public LoginController(LoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public IActionResult Login(LoginDto dto)
        {
            return Ok(_loginService.SignIn(dto));
        }
    }
}
