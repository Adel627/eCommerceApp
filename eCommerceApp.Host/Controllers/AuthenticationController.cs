using eCommerceApp.Application.DTOs.Authentication;
using eCommerceApp.Application.Services.Interfaces.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceApp.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController(IAuthenticationService authenticationService) : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService = authenticationService;

        

        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromForm] CreateUser user)
        {
            
            var result = await _authenticationService.CreateUser(user);
            return result.Success ? Ok(result) : BadRequest(result);
        }


        [HttpPost("login")]
        public async Task<IActionResult> LoginUser(LoginUser user)
        {
            var result = await _authenticationService.LoginUser(user);
            return result.Success ? Ok(result) : BadRequest(result);
        }



        [HttpPost("refresh")]
        public async Task<IActionResult> ReviveToken([FromBody] string refreshToken)
        {
            var result = await _authenticationService.ReviveToken(refreshToken);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("revoke-refresh-token")]
        public async Task<IActionResult> RevokeRefreshToken([FromBody] string refreshToken)
        {
            var result = await _authenticationService.RevokeRefreshToken(refreshToken);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
