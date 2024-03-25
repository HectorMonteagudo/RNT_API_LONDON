using RNT_APIREST_LONDON.Entities;
using RNT_APIREST_LONDON.Response;
using Sap.Data.Hana;
using Dapper;
namespace RNT_APIREST_LONDON.Repositorios
{
    public interface IProductoRepository
    {
        Task<ProductoResponse> ObtenerAsync();
    }
    public class ProductoRepository : IProductoRepository
    {
        private readonly string? connectionString;
        private readonly IConfiguration _configuration;

        public ProductoRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<ProductoResponse> ObtenerAsync()
        {
            var response = new ProductoResponse();
            IEnumerable<Productos>? listaLineas;
            string query = string.Empty;

            try
            {
                query = "SELECT \"CodProducto\", \"Producto\", \"CodCondicion\",\"CodEmpaque\",\"CodEspecie\",\"CodEtapa\",\"CodExistencia\",\"CodFamilia\",\"CodGrupoArticulo\",\"CodGrupoMarca\",\"CodLinea\",\"CodMarca\",\"CodPresentacion\",\"CodRaza\",\"CodSegmento\",\"CodSubFamilia\",\"CodSubLinea\",\"CodSubSegmento\",\"CodVariedad\",\"Condicion\",\"Empaque\",\"Equivalencia1\",\"Equivalencia2\",\"Equivalencia3\",\"Especie\",\"Etapa\",\"Existencia\",\"Familia\",\"GrupoArticulo\",\"GrupoMarca\",\"Linea\",TO_NVARCHAR(\"PesoNetoUnitario\") AS \"PesoNetoUnitario\",\"Presentacion\",\"Raza\",\"Segmento\", TO_NVARCHAR(\"Stock\")AS \"Stock\",\"SubFamilia\",\"SubLinea\",\"SubSegmento\",\"Variedad\" FROM _SYS_BIC.\"RNT_DM/RNT_PRODUCTO\"";
                using var connection = new HanaConnection(connectionString);

                listaLineas = await connection.QueryAsync<Productos>(query);
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
