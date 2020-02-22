using System.Threading.Tasks;
using Application.User;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class UserController : BaseController
    {
        [HttpGet("profile")]
        public async Task<ActionResult<Person>> Details()
        {
            return await Mediator.Send(new Details.Query());
        }

        [HttpGet("{id}/information")]
        [Authorize(Policy = "Retailer")]
        public async Task<ActionResult<Person>> UserInfo(string id)
        {
            return await Mediator.Send(new UserInfo.Query { Id = id });
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<LoggedInfo>> Login(Login.Command command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult<LoggedInfo>> Regiser(Register.Command command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("password")]
        public async Task<ActionResult<Unit>> ChangePasswrod(ChangePassword.Command command)
        {
            return await Mediator.Send(command);
        }
    }
}