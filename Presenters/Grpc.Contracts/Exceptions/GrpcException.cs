namespace Grpc.Contracts.Exceptions
{
    public class GrpcException : Exception
    {
        public string ErrorCode { get; }

        public GrpcException(string errorCode, string? message = null, Exception? innerException = null) : base(message,
            innerException)
        {
            ErrorCode = errorCode;
        }
    }
}
