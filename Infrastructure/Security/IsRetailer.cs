using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Persistence;

namespace Infrastructure.Security
{
    public class IsRetailer : IAuthorizationRequirement
    {

    }

    public class IsRequirementHandler : AuthorizationHandler<IsRetailer>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DataContext _context;
        public IsRequirementHandler(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsRetailer requirement)
        {
            var Username = _httpContextAccessor
                .HttpContext.User?.Claims?
                .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            var user = _context.Users.SingleOrDefault(x => x.UserName == Username);

            if(user != null && user.Role.Name == "Retailer")
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}