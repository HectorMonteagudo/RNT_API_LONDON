using Microsoft.IdentityModel.Tokens;
using RNT_APIREST_LONDON.Request;
using RNT_APIREST_LONDON.Response;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static Dapper.SqlMapper;

namespace RNT_APIREST_LONDON.Repositorios.Servicios
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;

        public UserService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<LoginDtoResponse> LoginAsync(LoginDtoRequest request)
        {
            var fechaExpiracion = DateTime.Now.AddHours(1);

            //Validamos el USUARIO Y CLAVE
            if (request.Usuario == _configuration["LoginJwt:Usuario"] && request.Password== _configuration["LoginJwt:Password"])
            {
                //Vamos a devolver Claims
               
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name ,"Administrador"),
                    new Claim(ClaimTypes.Expiration, fechaExpiracion.ToString("yyyy-MM-dd HH:mm:ss"))
                };



                //Crear el JWT
                var llaveSimetrica = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!));
                var credenciales = new SigningCredentials(llaveSimetrica, SecurityAlgorithms.HmacSha256);

                var header = new JwtHeader(credenciales);

                var payload = new JwtPayload(
                      _configuration["Jwt:Issuer"],
                       _configuration["Jwt:Audience"],
                       claims,
                       DateTime.Now,
                       fechaExpiracion

                    );

                var jwtToken = new JwtSecurityToken(header, payload); //Devuelve un dato en Binarios

                var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

                return new LoginDtoResponse(token);
            }
            else
            {
                return new LoginDtoResponse( string.Empty);
            }
        }
    }
}
