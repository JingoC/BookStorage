using Application.BookSearch.UseCases.SearchByAuthor;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RestApi.Contracts.BookSearch.SearchByAuthor;
using RestApi.Contracts.BookSearch.SearchByAuthor.Dto;

namespace WebApi.Controller
{
    [Route("booksearch")]
    public class BookSearchController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookSearchController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<SearchByAuthorHttpResponse> SearchByAuthor(string name, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new SearchByAuthorRequest()
            {
                Name = name
            }, cancellationToken);

            return new SearchByAuthorHttpResponse()
            {
                Books = response.Books.Select(x => new SearchByAuthorBookHttpDto()
                {
                    Id = x.Id,
                    Title = x.Title,
                    AuthorName = x.AuthorName,
                    Description = x.Description
                }).ToArray()
            };
        }
    }
}
