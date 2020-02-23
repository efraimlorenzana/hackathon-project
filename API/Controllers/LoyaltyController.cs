using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Loyalty;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class LoyaltyController : BaseController
    {
        [HttpGet("vouchercodes")]
        [Authorize(Policy = "Retailer")]
        public async Task<ActionResult<List<VoucherCode>>> GetVoucherCodesList()
        {
            return await Mediator.Send(new GetVoucherCodes.Query());
        }

        [HttpPost("points/redeem")]
        public async Task<ActionResult<Unit>> Redeem(AddPoints.Command command)
        {
            return await Mediator.Send(command);
        }
    }
}