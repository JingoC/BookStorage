using Database.Interface;
using Domain.Entities;

namespace Database.Services
{
    public class BookProvider : IBookProvider
    {
        private readonly List<BookEntity> Books = new();

        public BookProvider()
        {
            var authors = new List<AuthorEntity>();

            for (int i = 0; i < 10; i++)
            {
                authors.Add(new AuthorEntity()
                {
                    Id = i + 1,
                    Name = $"Name{i}"
                });
            }

            for (int i = 0; i < 10000; i++)
            {
                Books.Add(new BookEntity()
                {
                    Id = i + 1,
                    Title = $"Title{i}",
                    Author = authors[Random.Shared.Next(0, 10)],
                    Description = $"Description{i}",
                });
            }
        }

        public IReadOnlyCollection<BookEntity> GetAll()
            => Books;
    }
}
