using System.Net;

namespace ProductPro.Models
{
    public class APIRespons
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } = true;
        public List<string> ErrorMessages { get; set; } = new List<string>();
        public object Resutl { get; set; }
    }
}
