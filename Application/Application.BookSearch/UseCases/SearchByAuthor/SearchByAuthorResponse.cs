using Application.BookSearch.UseCases.SearchByAuthor.Dto;

namespace Application.BookSearch.UseCases.SearchByAuthor
{
    public class SearchByAuthorResponse
    {
        public required IReadOnlyCollection<SearchByAuthorBookDto> Books { get; set; }
    }
}
