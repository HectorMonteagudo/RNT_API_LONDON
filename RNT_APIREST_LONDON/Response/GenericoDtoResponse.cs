using RNT_APIREST_LONDON.Entities;

namespace RNT_APIREST_LONDON.Response
{
    public class GenericoDtoResponse //: BaseResponse
    {
       
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
        public ICollection<Generico> Listado { get; set; }

        public GenericoDtoResponse()
        {
            Listado = new List<Generico>();
        }
    }
}
