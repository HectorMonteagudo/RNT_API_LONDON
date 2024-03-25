using Dapper;
using RNT_APIREST_LONDON.Entities;
using RNT_APIREST_LONDON.Response;
using Sap.Data.Hana;

namespace RNT_APIREST_LONDON.Repositorios
{
    public interface IClientesRepository
    {
        Task<ClienteResponse> ObtenerAsync();
    }

    public class ClientesRepository : IClientesRepository
    {
        private readonly string? connectionString;
        private readonly IConfiguration _configuration;

        public ClientesRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<ClienteResponse> ObtenerAsync()
        {
            var response = new ClienteResponse();
            IEnumerable<Clientes>? listaLineas;
            string query = string.Empty;

            try
            {
                query = "SELECT  \"CodigoCliente\" ,\"Cliente\",TO_NVARCHAR(\"CodCanal\") AS \"CodCanal\",\"Canal\",TO_NVARCHAR(\"CodSubCanal\") AS \"CodSubCanal\",\"SubCanal\",\"SellOut\",\"FillRate\",TO_NVARCHAR(\"CodGrupoCanal\") AS \"CodGrupoCanal\",\"GrupoCanal\",TO_NVARCHAR(\"CodGrupoCliente\") AS \"CodGrupoCliente\",\"GrupoCliente\",TO_NVARCHAR(\"CodRegion\") AS \"CodRegion\",\"Region\",TO_NVARCHAR(\"CodLista\") AS \"CodLista\",\"NombreLista\",\"Activo\",TO_NVARCHAR(\"CodDescuento\") AS \"CodDescuento\",\"Descuento\",\"CodTipificacion\",\"Tipificacion\",TO_NVARCHAR(\"Anio\") AS \"Anio\",TO_NVARCHAR(\"Mes\") AS \"Mes\" FROM _SYS_BIC.\"RNT_DM/RNT_CLIENTE\"";
                using var connection = new HanaConnection(connectionString);
                listaLineas = await connection.QueryAsync<Clientes>(query);
                response.Success = true;
                response.Listado = listaLineas.ToList();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = $"Error al obtener los Empaques: {ex.Message}.";
                throw;
            }

            return response;
        }


    }
}
