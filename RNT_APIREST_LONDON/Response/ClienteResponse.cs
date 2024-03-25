using RNT_APIREST_LONDON.Entities;

namespace RNT_APIREST_LONDON.Response
{
    public class ClienteResponse
    {
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
        public ICollection<Clientes> Listado { get; set; }

        public ClienteResponse()
        {
            Listado = new List<Clientes>();
        }
    }
}
