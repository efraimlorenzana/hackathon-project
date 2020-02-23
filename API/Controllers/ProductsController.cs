using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Application.Products;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using MediatR;

namespace API.Controllers
{
    public class ProductsController : BaseController
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<Product>>> Products()
        {
            return await Mediator.Send(new List.Query());
        }

        [HttpPost("buy")]
        [Authorize(Policy = "Customer")]
        public async Task<ActionResult<Unit>> Buy(Buy.Command command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("add")]
        [Authorize(Policy = "Retailer")]
        public async Task<ActionResult<Unit>> Add(Add.Command command)
        {
            return await Mediator.Send(command);
        }
    }
}