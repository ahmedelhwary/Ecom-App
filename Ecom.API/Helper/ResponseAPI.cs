namespace Ecom.API.Helper
{
    public class ResponseAPI
    {
        public ResponseAPI(int statusCode, string? message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetMessageFromStatusCode(statusCode);
        }
        private string GetMessageFromStatusCode(int statusCode)
        {
            return statusCode switch
            {
                200 => "Done",
                400 => "Bad Request",
                401 => "Unauthorized",
                404 => "Not Found Resources",
                500 => "Internal Server Error",
                _ => "Unknown Status"
            };
        }
        public int StatusCode { get; set; }
        public string? Message { get; set; }
    }
}
