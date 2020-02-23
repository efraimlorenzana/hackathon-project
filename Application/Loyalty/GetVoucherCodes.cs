using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Loyalty
{
    public class GetVoucherCodes
    {
        public class Query : IRequest<List<VoucherCode>>
        {

        }

        public class Handler : IRequestHandler<Query, List<VoucherCode>>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<List<VoucherCode>> Handle(Query request, CancellationToken cancellationToken)
            {
                var list = await _context.VoucherCodes.ToListAsync();

                return list;
            }
        }
    }
}