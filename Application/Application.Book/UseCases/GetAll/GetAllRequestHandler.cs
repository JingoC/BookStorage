using Application.Book.UseCases.GetAll.Dto;
using Database.Interface;
using MediatR;

namespace Application.Book.UseCases.GetAll
{
    public class GetAllRequestHandler : IRequestHandler<GetAllRequest, GetAllResponse>
    {
        private readonly IBookProvider _bookProvider;

        public GetAllRequestHandler(IBookProvider bookProvider)
        {
            _bookProvider = bookProvider;
        }

        public async Task<GetAllResponse> Handle(GetAllRequest request, CancellationToken cancellationToken = default)
        {
            return new GetAllResponse()
            {
                Books = _bookProvider.GetAll()
                    .Select(x => new GetAllBookDto()
                    {
                        Id = x.Id,
                        Title = x.Title,
                        Description = x.Description,
                        AuthorName = x.Author.Name
                    })
                    .ToArray()
            };
        }
    }
}
