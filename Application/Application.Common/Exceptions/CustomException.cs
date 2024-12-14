using Application.Common.Exceptions.Details;

namespace Application.Common.Exceptions
{
    public class CustomException : BaseCustomException<ErrorDetails>
    {
        public CustomException(string errorCode, string? message = null, Exception? innerException = null) : base(errorCode, message, innerException)
        {
        }
    }
}
