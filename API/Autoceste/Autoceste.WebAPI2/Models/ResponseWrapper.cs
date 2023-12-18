namespace Autoceste.WebAPI2.Models
{
    public class ResponseWrapper
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public object Response { get; set; }
    }
}
