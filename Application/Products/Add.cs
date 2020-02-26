using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Persistence;

namespace Application.Products
{
    public class Add
    {
        public class Command : IRequest
        {
            public string Name { get; set; }
            public string Category { get; set; }
            public double Price { get; set; }
            public int Points { get; set; }
            public IFormFile File { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Name).NotEmpty();
                RuleFor(x => x.Category).NotEmpty();
                RuleFor(x => x.Price).NotEmpty();
                RuleFor(x => x.Points).NotEmpty();
                RuleFor(x => x.File).NotNull();
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            private readonly IPhotoAccessor _photoAccessor;
            public Handler(DataContext context, IPhotoAccessor photoAccessor)
            {
                _photoAccessor = photoAccessor;
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var uploadResult = _photoAccessor.AddPhoto(request.File);

                var image = new Photo
                {
                    Id = uploadResult.PublicId,
                    Url = uploadResult.Url
                };

                var item = new Product
                {
                    Name = request.Name,
                    Category = request.Category,
                    Price = request.Price,
                    Points = request.Points,
                    IsAvailable = true,
                    Thumbnail = image
                };

                _context.Products.Add(item);

                var saved = await _context.SaveChangesAsync() > 0;

                if (saved) return Unit.Value;

                throw new Exception("Error Saving product");
            }
        }
    }
}