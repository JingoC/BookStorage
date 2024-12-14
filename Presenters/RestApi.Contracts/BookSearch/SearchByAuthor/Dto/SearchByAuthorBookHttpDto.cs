namespace RestApi.Contracts.BookSearch.SearchByAuthor.Dto
{
    public class SearchByAuthorBookHttpDto
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string AuthorName { get; set; }
        public required string Description { get; set; }
    }
}
