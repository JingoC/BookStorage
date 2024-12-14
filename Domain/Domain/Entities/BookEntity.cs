namespace Domain.Entities
{
    public class BookEntity
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required AuthorEntity Author { get; set; }
        public required string Description { get; set; }
    }
}
