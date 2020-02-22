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
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.User
{
    public class Register
    {
        public class Command : IRequest<LoggedInfo>
        {
            public Guid Id { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string Username { get; set; }
            public string Lastname { get; set; }
            public string Firstname { get; set; }
            public string Middlename { get; set; }
            public Gender Gender { get; set; }
            public string Address { get; set; }
            public bool isRetailer { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Email).NotEmpty().EmailAddress();
                RuleFor(x => x.Password).Password();
                RuleFor(x => x.Username).NotEmpty();
                RuleFor(x => x.Lastname).NotEmpty();
                RuleFor(x => x.Firstname).NotEmpty();
                RuleFor(x => x.Gender).IsInEnum();
                RuleFor(x => x.Address).NotEmpty();
                RuleFor(x => x.Address).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command, LoggedInfo>
        {
            private readonly DataContext _context;
            private readonly IJwtGenerator _jwtGenerator;
            private readonly UserManager<AppUser> _userManager;
            public Handler(DataContext context, IJwtGenerator jwtGenerator, UserManager<AppUser> userManager)
            {
                _userManager = userManager;
                _jwtGenerator = jwtGenerator;
                _context = context;

            }

            public async Task<LoggedInfo> Handle(Command request, CancellationToken cancellationToken)
            {
                if(await _context.Users.Where(x => x.Email == request.Email).AnyAsync())
                    throw new RestException(HttpStatusCode.BadRequest, new { Email = "Email address already in use" });

                if(await _context.Users.Where(x => x.UserName == request.Username).AnyAsync())
                    throw new RestException(HttpStatusCode.BadRequest, new { Email = "Username already in use" });
                
                var person = new Person
                {
                    Id = request.Id,
                    Lastname = request.Lastname,
                    Firstname = request.Firstname,
                    Middlename = request.Middlename,
                    Gender = request.Gender,
                    Address = request.Address
                };

                var role = _context.Roles.Where(x => x.Name == Constant.Customer);

                var user = new AppUser
                {
                    Email = request.Email,
                    Fullname = Utility.FormatName(person),
                    UserName = request.Username,
                    Person = person,
                    Role = _context.Roles.Where(x => x.Name == (request.isRetailer ? "Retailer" : "Customer")).FirstOrDefault()
                };

                var register = await _userManager.CreateAsync(user, request.Password);

                if(register.Succeeded) 
                {
                    return new LoggedInfo
                    {
                        DisplayName = user.Fullname,
                        Username = user.UserName,
                        Image = null,
                        Token = _jwtGenerator.CreateToken(user),
                        Role = user.Role.Name
                    };
                }

                throw new Exception("Registration Failed");
            }
        }
    }
}