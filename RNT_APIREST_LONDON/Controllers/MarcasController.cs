using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RNT_APIREST_LONDON.Repositorios;

namespace RNT_APIREST_LONDON.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MarcasController : ControllerBase
    {
        private readonly IMarcaRepository _repository;
        private readonly ILogger<MarcasController> logger;

        public MarcasController(IMarcaRepository repository , ILogger<MarcasController> logger)
        {
            _repository = repository;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _repository.ObtenerAsync();
            return response.Success ? Ok(response) : BadRequest(response) ;
        }
    }
}
