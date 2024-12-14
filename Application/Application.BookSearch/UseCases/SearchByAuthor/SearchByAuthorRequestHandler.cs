using Application.BookSearch.UseCases.SearchByAuthor.Dto;
using Database.Interface;
using MediatR;

namespace Application.BookSearch.UseCases.SearchByAuthor
{
    public class SearchByAuthorRequestHandler : IRequestHandler<SearchByAuthorRequest, SearchByAuthorResponse>
    {
        private readonly IBookProvider _bookProvider;

        public SearchByAuthorRequestHandler(IBookProvider bookProvider)
        {
            _bookProvider = bookProvider;
        }

        public async Task<SearchByAuthorResponse> Handle(SearchByAuthorRequest request, CancellationToken cancellationToken = default)
        {
            return new SearchByAuthorResponse()
            {
                Books = _bookProvider.GetAll()
                    .Where(x => x.Author.Name.Contains(request.Name))
                    .Select(x => new SearchByAuthorBookDto()
                    {
                        Id = x.Id,
                        Title = x.Title,
                        Description = x.Description,
                        AuthorName = x.Author.Name
                    })
                    .ToArray()
            };
        }
    }
}
