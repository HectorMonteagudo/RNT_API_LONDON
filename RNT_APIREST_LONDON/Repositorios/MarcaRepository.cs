using Dapper;
using RNT_APIREST_LONDON.Entities;
using RNT_APIREST_LONDON.Response;
using Sap.Data.Hana;

namespace RNT_APIREST_LONDON.Repositorios
{
    public interface IMarcaRepository
    {
        Task<GenericoDtoResponse> ObtenerAsync();
    }

    public class MarcaRepository : IMarcaRepository
    {
        private readonly string? connectionString;
        private readonly IConfiguration _configuration;

        public MarcaRepository( IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<GenericoDtoResponse> ObtenerAsync()
        {
            var response = new GenericoDtoResponse();
            IEnumerable<Generico>? listMarcas;
            string query = string.Empty;

            try
            {
                query = "SELECT \"PrcCode\" as \"Codigo\",\"PrcName\" as \"Descripcion\" FROM OPRC where \"DimCode\"='2' AND \"U_SYP_RICO_DIM2MARCA\"='Y' ORDER BY 1"; 
                using var connection = new HanaConnection(connectionString);

                listMarcas = await connection.QueryAsync<Generico>(query);
                
                response.Success = true;
               response.Listado = listMarcas.ToList();
            }
            catch (Exception ex)
            {
                response.Success=false;
                response.ErrorMessage = $"Error al obtener las Marcas: {ex.Message}.";
                throw;
            }

            return response;
          
        }
    }
}
