using RNT_APIREST_LONDON.Entities;

namespace RNT_APIREST_LONDON.Response
{
    public class ProductoResponse
    {
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
        public ICollection<Productos> Listado { get; set; }

        public ProductoResponse()
        {
            Listado = new List<Productos>();
        }
    }
}
