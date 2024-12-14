namespace Application.Common.Exceptions
{
    public class NotFoundException : CustomException
    {
        public NotFoundException(string? message = null, Exception? innerException = null)
            : base("NOT_FOUND", message, innerException)
        {

        }
    }
}
