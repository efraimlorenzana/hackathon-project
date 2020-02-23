using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Application.Interfaces;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Loyalty
{
    public class AddPoints
    {
        public class Command : IRequest
        {
            public string Source { get; set; }
            public string VoucherCode { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Source).NotEmpty();
                RuleFor(x => x.VoucherCode).NotEmpty();
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

                var voucherCode = await _context.VoucherCodes.SingleOrDefaultAsync(x => x.Code == request.VoucherCode);

                if(voucherCode == null) throw new RestException(HttpStatusCode.NotFound, new { VoucherCode = "Voucher code not found" });

                if(!voucherCode.IsValid) throw new RestException(HttpStatusCode.BadRequest, new { VoucherCode = "Voucher code already redeem or invalid" });

                var points = new Points
                {
                    Value = voucherCode.PointsValue,
                    Source = request.Source,
                    DateEarned = DateTime.Now
                };

                voucherCode.IsValid = false;
                voucherCode.DateRedeem = DateTime.Now;

                _context.Entry(voucherCode).State = EntityState.Modified;
                user.EarnedPoints.Add(points);

                var saved = await _context.SaveChangesAsync() > 0;

                if(saved) return Unit.Value;

                throw new Exception("Error saving changes");
            }
        }
    }
}