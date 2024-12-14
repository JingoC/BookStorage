using MediatR;

namespace Application.BookSearch.UseCases.SearchByAuthor
{
    public class SearchByAuthorRequest : IRequest<SearchByAuthorResponse>
    {
        public string Name { get; set; } = null!;
    }
}
