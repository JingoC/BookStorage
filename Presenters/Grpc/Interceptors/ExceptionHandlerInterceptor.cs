using Application.Common.Exceptions;
using Grpc.Core;
using Grpc.Core.Interceptors;

namespace Grpc.Interceptors
{
    public class ExceptionHandlerInterceptor : Interceptor
    {
        public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request, ServerCallContext context,
            UnaryServerMethod<TRequest, TResponse> continuation)
        {
            try
            {
                return await continuation(request, context);
            }
            catch (Exception ex) when (ex is BaseCustomException)
            {
                throw new RpcException(new Status(StatusCode.Internal, ex.ToString()));
            }
            catch (Exception ex)
            {
                var unknownException = new CustomException("UNKNOWN_ERROR", ex.Message);
                throw new RpcException(new Status(StatusCode.Unknown, unknownException.ToString()));
            }
        }
    }
}
