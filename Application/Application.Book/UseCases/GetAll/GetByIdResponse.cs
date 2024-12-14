using Application.Book.UseCases.GetAll.Dto;

namespace Application.Book.UseCases.GetAll
{
    public class GetAllResponse
    {
        public required IReadOnlyCollection<GetAllBookDto> Books { get; set; }
    }
}
