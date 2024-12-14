namespace Application.Book.UseCases.GetAll.Dto
{
    public class GetAllBookDto
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string AuthorName { get; set; }
        public required string Description { get; set; }
    }
}
