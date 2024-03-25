using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RNT_APIREST_LONDON.Repositorios;

namespace RNT_APIREST_LONDON.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoRepository _repository;

        public ProductosController( IProductoRepository repository)
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
