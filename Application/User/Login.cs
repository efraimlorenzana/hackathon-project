using System;
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

namespace Application.User
{
    public class Login
    {
        public class Command : IRequest<LoggedInfo>
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Email).NotEmpty().EmailAddress();
                RuleFor(x => x.Password).Password();
            }
        }

        public class Handler : IRequestHandler<Command, LoggedInfo>
        {
            private readonly DataContext _context;
            private readonly SignInManager<AppUser> _signInManager;
            private readonly UserManager<AppUser> _userManager;
            private readonly IJwtGenerator _jwtGenerator;
            public Handler(DataContext context, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IJwtGenerator jwtGenerator)
            {
                _jwtGenerator = jwtGenerator;
                _userManager = userManager;
                _signInManager = signInManager;
                _context = context;
            }

            public async Task<LoggedInfo> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByEmailAsync(request.Email);

                if(user == null) throw new RestException(HttpStatusCode.Unauthorized, new { CredentialError = "Incorrect Email or Password" });

                var signIn = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
                // var setRole = await _userManager.AddToRoleAsync(user, "Customer");

                if(signIn.Succeeded)
                {
                    //var role = await _userManager.GetRolesAsync(user);
                    return new LoggedInfo
                    {
                        DisplayName = user.Fullname,
                        Username = user.UserName,
                        Image = null,
                        Token = _jwtGenerator.CreateToken(user),
                        Role = user.Role.Name
                    };
                }

                throw new RestException(HttpStatusCode.Unauthorized, new { CredentialError = "Incorrect Email or Password" });
            }
        }
    }
}