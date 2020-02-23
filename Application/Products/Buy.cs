using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Products
{
    public class Buy
    {
        public class Command : IRequest
        {
            public string ItemId { get; set; }
            public string ShippingAddress { get; set; }
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

                var Item = await _context.Products.SingleOrDefaultAsync(x => x.Id == Guid.Parse(request.ItemId));

                if(Item == null) throw new RestException(HttpStatusCode.NotFound, new { Product = "Item not found" });

                if(!Item.IsAvailable) throw new RestException(HttpStatusCode.NotFound, new { Product = "Out of stock" });

                if(Item.Points > _userAccessor.GetTotalPoints())
                    throw new RestException(HttpStatusCode.BadRequest, new { Purchase = "Purchase failed, not enough points" });
                
                Item.IsAvailable = false;
                
                var pItem = new PurchasedItem
                {
                    Product = Item,
                    DatePurchased = DateTime.Now,
                    ShippingAddress = string.IsNullOrWhiteSpace(request.ShippingAddress) ? user.Person.Address : request.ShippingAddress,
                    isDelivered = false
                };

                user.PurchasedItems.Add(pItem);

                var saved = await _context.SaveChangesAsync() > 0;

                if(saved) return Unit.Value;

                throw new Exception("Error Saving Purchased");
            }
        }
    }
}