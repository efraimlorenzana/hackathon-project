using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Domain;
using MediatR;
using Persistence;

namespace Application.User
{
    public class UserInfo
    {
        public class Query : IRequest<Person>
        {
            public string Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Person>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Person> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await _context.Users.FindAsync(request.Id);

                if(user == null)  throw new RestException(HttpStatusCode.NotFound, new { UserInfo = "Requested User not found" });

                return user.Person;
            }
        }
    }
}