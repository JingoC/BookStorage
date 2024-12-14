namespace RestApi.Contracts.Book.GetAll.Dto
{
    public class GetAllBookHttpDto
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string AuthorName { get; set; }
        public required string Description { get; set; }
    }
}
