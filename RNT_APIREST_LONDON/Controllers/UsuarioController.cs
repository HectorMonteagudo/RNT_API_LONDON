using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RNT_APIREST_LONDON.Repositorios.Servicios;
using RNT_APIREST_LONDON.Request;

namespace RNT_APIREST_LONDON.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUserService _service;

        public UsuarioController(IUserService service)
        {
            _service = service;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDtoRequest request)
        {
            var response = await _service.LoginAsync(request);
            if (string.IsNullOrEmpty(response.token))
            {
                return Unauthorized();
            }
            else
            {
                return Ok(response);    
            }
        }
    }
}
