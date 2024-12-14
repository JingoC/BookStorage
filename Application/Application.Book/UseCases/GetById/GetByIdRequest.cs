using MediatR;

namespace Application.Book.UseCases.GetById
{
    public class GetByIdRequest : IRequest<GetByIdResponse>
    {
        public long Id { get; set; }
    }
}
