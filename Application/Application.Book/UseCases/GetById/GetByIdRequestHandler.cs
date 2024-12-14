using Application.Common.Exceptions;
using Database.Interface;
using MediatR;

namespace Application.Book.UseCases.GetById
{
    public class GetByIdRequestHandler : IRequestHandler<GetByIdRequest, GetByIdResponse>
    {
        private readonly IBookProvider _bookProvider;

        public GetByIdRequestHandler(IBookProvider bookProvider)
        {
            _bookProvider = bookProvider;
        }

        public async Task<GetByIdResponse> Handle(GetByIdRequest request, CancellationToken cancellationToken = default)
        {
            return _bookProvider.GetAll()
                .Where(x => x.Id == request.Id)
                .Select(x => new GetByIdResponse()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    AuthorName = x.Author.Name
                })
                .FirstOrDefault() ?? throw new NotFoundException($"Book '{request.Id}' is not found");
        }
    }
}
