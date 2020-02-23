using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.User
{
    public class Details
    {
        public class Query : IRequest<ProfileInfo>
        {

        }

        public class Handler : IRequestHandler<Query, ProfileInfo>
        {
            private readonly UserManager<AppUser> _userManager;
            private readonly IUserAccessor _userAccessor;
            public Handler(UserManager<AppUser> userManager, IUserAccessor userAccessor)
            {
                _userAccessor = userAccessor;
                _userManager = userManager;
            }

            public async Task<ProfileInfo> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByNameAsync(_userAccessor.GetCurrentUsername());

                var Gender = new [] { "Male", "Female", "Complicated" };

                return new ProfileInfo
                {
                    Lastname = user.Person.Lastname,
                    Firstname = user.Person.Firstname,
                    Middlename = user.Person.Middlename,
                    Gender = Gender[(int)user.Person.Gender],
                    Address = user.Person.Address,
                    PointsHistory = user.EarnedPoints,
                    TotalPoints = _userAccessor.GetTotalPoints(),
                    PurchasedProducts = user.PurchasedItems
                };
            }
        }
    }
}