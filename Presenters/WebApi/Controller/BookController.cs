using Application.Book.UseCases.GetAll;
using Application.Book.UseCases.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RestApi.Contracts.Book.GetAll;
using RestApi.Contracts.Book.GetAll.Dto;
using RestApi.Contracts.Book.GetById;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebApi.Controller
{
    [Route("book")]
    public class BookController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<GetByIdHttpResponse> GetById(int id, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetByIdRequest()
            {
                Id = id
            }, cancellationToken);

            return new GetByIdHttpResponse()
            {
                Id = response.Id,
                Title = response.Title,
                AuthorName = response.AuthorName,
                Description = response.Description
            };
        }

        [HttpGet("all")]
        public async Task<GetAllHttpResponse> GetAll(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetAllRequest(), cancellationToken);

            return new GetAllHttpResponse()
            {
                Books = response.Books.Select(x => new GetAllBookHttpDto()
                {
                    Id = x.Id,
                    Title = x.Title,
                    AuthorName = x.AuthorName,
                    Description = x.Description
                }).ToArray()
            };
        }

        [HttpGet("token")]
        public string Token()
        {
            return GenerateJSONWebToken();
        }

        string GenerateJSONWebToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("test_test_test_test_test_test_test_test_test_test"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim("sub", "login"),
                new Claim("email", "email"),
                new Claim("jti", Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken("issuer",
                "issuer",
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
