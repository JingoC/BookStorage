using MediatR;

namespace Application.Book.UseCases.GetAll
{
    public class GetAllRequest : IRequest<GetAllResponse>
    {
        public long Id { get; set; }
    }
}
