using Domain.Entities;

namespace Database.Interface
{
    public interface IBookProvider
    {
        public IReadOnlyCollection<BookEntity> GetAll();
    }
}
