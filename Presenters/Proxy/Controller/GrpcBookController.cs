using BookStorage;
using Grpc.Net.ClientFactory;
using Microsoft.AspNetCore.Mvc;
using RestApi.Contracts.Book.GetAll;
using RestApi.Contracts.Book.GetAll.Dto;
using RestApi.Contracts.Book.GetById;

namespace Proxy.Controller
{
    [Route("grpc/book")]
    public class GrpcBookController : ControllerBase
    {
        private readonly BookService.BookServiceClient _bookServiceClient;

        public GrpcBookController( BookService.BookServiceClient bookServiceClient, GrpcClientFactory grpcClientFactory)
        {
            _bookServiceClient = grpcClientFactory.CreateClient<BookService.BookServiceClient>("book");
        }

        [HttpGet("{id}")]
        public async Task<GetByIdHttpResponse> GetById(int id, CancellationToken cancellationToken)
        {
            var book = await _bookServiceClient.GetByIdAsync(new GetByIdGrpcRequest()
            {
                Id = id
            }, cancellationToken: cancellationToken);

            return new GetByIdHttpResponse()
            {
                Id = book.Id,
                AuthorName = book.Author,
                Description = book.Description,
                Title = book.Title
            };
        }

        [HttpGet("all")]
        public async Task<GetAllHttpResponse> GetAll(CancellationToken cancellationToken)
        {
            var response = await _bookServiceClient.GetAllAsync(new GetAllGrpcRequest(), cancellationToken: cancellationToken);

            return new GetAllHttpResponse()
            {
                Books = response.Books.Select(x => new GetAllBookHttpDto()
                {
                    Id = x.Id,
                    AuthorName = x.Author,
                    Description = x.Description,
                    Title = x.Title
                }).ToArray()
            };
        }
    }
}
