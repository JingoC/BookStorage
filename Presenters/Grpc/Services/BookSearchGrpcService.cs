using Application.BookSearch.UseCases.SearchByAuthor;
using BookStorage;
using Grpc.Core;
using MediatR;

namespace Grpc.Services
{
    public class BookSearchGrpcService : BookSearchService.BookSearchServiceBase
    {
        private readonly IMediator _mediator;

        public BookSearchGrpcService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override async Task<SearchByAuthorGrpcResponse> SearchByAuthor(SearchByAuthorGrpcRequest request, ServerCallContext context)
        {
            var response = await _mediator.Send(new SearchByAuthorRequest()
            {
                Name = request.Name
            }, context.CancellationToken);

            return new SearchByAuthorGrpcResponse()
            {
                Books =
                {
                    response.Books.Select(x => new SearchByAuthorBookGrpcDto()
                    {
                        Id = x.Id,
                        Title = x.Title,
                        Description = x.Description,
                        Author = x.AuthorName
                    })
                },
            };
        }
    }
}
