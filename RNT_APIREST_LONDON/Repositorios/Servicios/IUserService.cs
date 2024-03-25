using RNT_APIREST_LONDON.Request;
using RNT_APIREST_LONDON.Response;

namespace RNT_APIREST_LONDON.Repositorios.Servicios
{
    public interface IUserService
    {

        Task<LoginDtoResponse> LoginAsync(LoginDtoRequest request);
    }
}
