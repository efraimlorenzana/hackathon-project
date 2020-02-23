using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Persistence;

namespace Infrastructure.Security
{
    public class IsCustomer : IAuthorizationRequirement
    {
        
    }

    public class IsCustomerRequirementHandler : AuthorizationHandler<IsCustomer>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DataContext _context;
        public IsCustomerRequirementHandler(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsCustomer requirement)
        {
            var Username = _httpContextAccessor
                .HttpContext.User?.Claims?
                .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            var user = _context.Users.SingleOrDefault(x => x.UserName == Username);

            if(user != null && user.Role.Name == "Customer")
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}