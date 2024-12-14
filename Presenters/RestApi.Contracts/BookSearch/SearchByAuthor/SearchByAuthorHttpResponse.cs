using RestApi.Contracts.BookSearch.SearchByAuthor.Dto;

namespace RestApi.Contracts.BookSearch.SearchByAuthor
{
    public class SearchByAuthorHttpResponse
    {
        public required IReadOnlyCollection<SearchByAuthorBookHttpDto> Books { get; set; }
    }
}
