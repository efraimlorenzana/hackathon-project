using System.Text;
using API.Middleware;
using Application.Interfaces;
using Application.User;
using Domain;
using FluentValidation.AspNetCore;
using Infrastructure.Security;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Persistence;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseLazyLoadingProxies();
                opt.UseSqlite(Configuration.GetConnectionString("hackFourConn"));
            });

            services.AddControllers(opt =>
                {
                    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                    opt.Filters.Add(new AuthorizeFilter(policy));
                })
                .AddFluentValidation(cfg =>
                {
                    cfg.RegisterValidatorsFromAssemblyContaining<Login>();
                });

            services.AddMediatR(typeof(Login.Handler).Assembly);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["TokenKey"]));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(opt =>
                    {
                        opt.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = key,
                            ValidateAudience = false,
                            ValidateIssuer = false
                        };
                    });

            var UserBuilder = services.AddIdentityCore<AppUser>();
            var identityBuilder = new IdentityBuilder(UserBuilder.UserType, UserBuilder.Services);
            identityBuilder.AddEntityFrameworkStores<DataContext>().AddDefaultTokenProviders();
            identityBuilder.AddSignInManager<SignInManager<AppUser>>();

            services.AddAuthorization(opt => {
                opt.AddPolicy("Retailer", policy => {
                    policy.Requirements.Add(new IsRetailer());
                });

                opt.AddPolicy("Customer", policy => {
                    policy.Requirements.Add(new IsCustomer());
                });
            });
            services.AddTransient<IAuthorizationHandler, IsRetailerRequirementHandler>();
            services.AddTransient<IAuthorizationHandler, IsCustomerRequirementHandler>();

            services.AddScoped<IJwtGenerator, JwtGenerator>();
            services.AddScoped<IUserAccessor, UserAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ErrorHandlerMiddleware>();
            // if (env.IsDevelopment())
            // {
            //     app.UseDeveloperExceptionPage();
            // }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
