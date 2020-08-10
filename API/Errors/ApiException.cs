namespace API.Errors
{
      public class ApiException : ApiErrorResponse
      {
            public string Details { get; }
            public ApiException(int statusCode, string message = null, string details = null) : base(statusCode, message)
            {
                  this.Details = details;
            }
      }
}