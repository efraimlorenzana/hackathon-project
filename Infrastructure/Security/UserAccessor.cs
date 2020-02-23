using System.Linq;
using System.Security.Claims;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Persistence;

namespace Infrastructure.Security
{
    public class UserAccessor : IUserAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DataContext _context;
        public UserAccessor(IHttpContextAccessor httpContextAccessor, DataContext context)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetCurrentUsername()
        {
            return _httpContextAccessor
                .HttpContext.User?.Claims?
                .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        }

        public int GetTotalPoints()
        {
            var user = _context.Users.SingleOrDefault(x => x.UserName == GetCurrentUsername());

            return user.EarnedPoints.Select(x => x.Value).Sum();
        }
    }
}