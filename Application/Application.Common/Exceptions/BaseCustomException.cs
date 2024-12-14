using System.Text.Json;
using Application.Common.Exceptions.Details;

namespace Application.Common.Exceptions
{
    public class BaseCustomException : Exception
    {
        public BaseCustomException(string? message = null, Exception? innerException = null) : base(message,
            innerException)
        {

        }
    }

    public class BaseCustomException<T> : BaseCustomException
        where T : ErrorDetails, new()
    {
        private readonly JsonSerializerOptions _options = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        protected T Details { get; }

        public BaseCustomException(string errorCode, string? message = null, Exception? innerException = null) 
            : base(message, innerException)
        {
            Details = new T()
            {
                ErrorCode = errorCode,
                ErrorMessage = message
            };
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(Details, _options);
        }
    }
}
