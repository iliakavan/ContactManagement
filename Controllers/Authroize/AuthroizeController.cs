using ContactManagementV2.Services.AuthorizeService.DTO;
using ContactManagementV2.Services.AuthorizeService.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContactManagementV2.Controllers.Authroize
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class AuthroizeController(IAuthorize Service) : ControllerBase
    {
        private readonly IAuthorize _Service = Service;

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto user) 
        {
            var result = await _Service.Register(user);
            if(!result.IsSuccessfull) 
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto user) 
        {
            var result = await _Service.Login(user);
            if (!result.IsSuccessfull)
            {
                return NotFound(result);
            }
            return Ok(result);

        }
    }
}
