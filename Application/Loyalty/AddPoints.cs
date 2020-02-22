using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Persistence;

namespace Application.Loyalty
{
    public class AddPoints
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
            public int Value { get; set; }
            public DateTime DateEarned { get; set; }
            public string Source { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Value).NotEmpty();
                RuleFor(x => x.DateEarned).NotEmpty();
                RuleFor(x => x.Source).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            private readonly UserManager<AppUser> _userManager;
            private readonly IUserAccessor _userAccessor;
            public Handler(DataContext context, UserManager<AppUser> userManager, IUserAccessor userAccessor)
            {
                _userAccessor = userAccessor;
                _userManager = userManager;
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByNameAsync(_userAccessor.GetCurrentUsername());

                var points = new Points
                {
                    Id = request.Id,
                    Value = request.Value,
                    Source = request.Source,
                    DateEarned = request.DateEarned
                };

                user.Person.EarnedPoints.Add(points);

                var saved = await _context.SaveChangesAsync() > 0;

                if(saved) return Unit.Value;

                throw new Exception("Error saving changes");
            }
        }
    }
}