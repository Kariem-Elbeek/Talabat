using System;

namespace Talabat.APIs.Errors
{
    public class ApiResponses
    {
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }
        public ApiResponses(int statusCode, string errorMessage = null)
        {
            StatusCode = statusCode;
            ErrorMessage = errorMessage?? GetDefaultMessageForErrorCode(statusCode);
        }

        private string GetDefaultMessageForErrorCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "Bad Request",
                401 => "You are not Authorized",
                404 => "Resource Not Found",
                500 => "Server Error",
                _ => null,
            };
        }
    }
}
