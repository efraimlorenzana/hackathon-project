using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Application.Interfaces;
using Application.Validators;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Persistence;

namespace Application.Products
{
    public class Review
    {
        public class Command : IRequest
        {
            public string ProductId { get; set; }
            public string Comments { get; set; }
            public int Rating { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.ProductId).NotEmpty();
                RuleFor(x => x.Comments).NotEmpty();
                RuleFor(x => x.Rating).Rating();
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _userAccessor;
            private readonly UserManager<AppUser> _userManager;
            public Handler(DataContext context, IUserAccessor userAccessor, UserManager<AppUser> userManager)
            {
                _userManager = userManager;
                _userAccessor = userAccessor;
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByNameAsync(_userAccessor.GetCurrentUsername());

                var product = user.PurchasedItems.Where(x => x.Product.Id == Guid.Parse(request.ProductId)).Select(p => p.Product).SingleOrDefault();

                if(product == null) throw new RestException(HttpStatusCode.NotFound, new { Product = "Product not found" });

                var review = new ProductReview
                {
                    Comments = request.Comments,
                    Rating = request.Rating,
                    Customer = user.Fullname,
                    Date = DateTime.Now
                };

                product.Reviews.Add(review);

                var saved = await _context.SaveChangesAsync() > 0;

                if(saved) return Unit.Value;

                throw new Exception("Error saving review");
            }
        }
    }
}