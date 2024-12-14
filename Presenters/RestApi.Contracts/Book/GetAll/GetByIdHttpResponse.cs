using RestApi.Contracts.Book.GetAll.Dto;

namespace RestApi.Contracts.Book.GetAll
{
    public class GetAllHttpResponse
    {
        public required IReadOnlyCollection<GetAllBookHttpDto> Books { get; set; }
    }
}
