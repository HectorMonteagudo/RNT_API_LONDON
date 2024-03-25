using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RNT_APIREST_LONDON.Repositorios;

namespace RNT_APIREST_LONDON.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmpaquesController : ControllerBase
    {
        private readonly IEmpaqueRepository _repository;

        public EmpaquesController(IEmpaqueRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]

        public async Task<IActionResult> Get()
        {
            var reponse = await _repository.ObtenerAsync();
            return reponse.Success ? Ok(reponse) : BadRequest(reponse);
        }

    }
}
