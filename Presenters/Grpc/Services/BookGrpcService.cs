using Application.Book.UseCases.GetAll;
using Application.Book.UseCases.GetById;
using BookStorage;
using Grpc.Core;
using MediatR;

namespace Grpc.Services
{
    public class BookGrpcService : BookService.BookServiceBase
    {
        private readonly IMediator _mediator;

        public BookGrpcService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override async Task<GetByIdGrpcResponse> GetById(GetByIdGrpcRequest request, ServerCallContext context)
        {
            var book = await _mediator.Send(new GetByIdRequest()
            {
                Id = request.Id
            }, context.CancellationToken);

            return new GetByIdGrpcResponse()
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.AuthorName,
                Description = book.Description,
            };
        }

        public override async Task<GetAllGrpcResponse> GetAll(GetAllGrpcRequest request, ServerCallContext context)
        {
            var response = await _mediator.Send(new GetAllRequest(), context.CancellationToken);

            var result = new GetAllGrpcResponse();
            result.Books.AddRange(response.Books.Select(x => new GetAllBookGrpcDto()
            {
                Id = x.Id,
                Title = x.Title,
                Author = x.AuthorName,
                Description = x.Description,
            }));

            return result;
        }
    }
}
