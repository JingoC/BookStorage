using BookStorage;
using Microsoft.AspNetCore.Mvc;
using RestApi.Contracts.BookSearch.SearchByAuthor;
using RestApi.Contracts.BookSearch.SearchByAuthor.Dto;

namespace Proxy.Controller
{
    [Route("grpc/booksearch")]
    public class GrpcBookSearchController : ControllerBase
    {
        private readonly BookSearchService.BookSearchServiceClient _bookSearchServiceClient;

        public GrpcBookSearchController(BookSearchService.BookSearchServiceClient bookSearchServiceClient)
        {
            _bookSearchServiceClient = bookSearchServiceClient;
        }

        [HttpGet]
        public async Task<SearchByAuthorHttpResponse> Get(string name, CancellationToken cancellationToken)
        {
            var response = await _bookSearchServiceClient.SearchByAuthorAsync(new SearchByAuthorGrpcRequest()
            {
                Name = name
            }, cancellationToken: cancellationToken);

            return new SearchByAuthorHttpResponse()
            {
                Books = response.Books.Select(x => new SearchByAuthorBookHttpDto()
                {
                    Id = x.Id,
                    Title = x.Title,
                    AuthorName = x.Author,
                    Description = x.Description,
                }).ToArray()
            };
        }
    }
}
