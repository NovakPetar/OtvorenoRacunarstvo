using Autoceste.WebAPI2.Models;

namespace Autoceste.WebAPI2
{
    public static class ErrorResponses
    {
        public static ResponseWrapper GeneralException { get; set; } = new ResponseWrapper()
        {
            Status = "Internal Server Error",
            Message = "Unexpected error occurred."
        };
    }
}
