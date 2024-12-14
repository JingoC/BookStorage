namespace Application.Book.UseCases.GetById
{
    public class GetByIdResponse
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string AuthorName { get; set; }
        public required string Description { get; set; }
    }
}
