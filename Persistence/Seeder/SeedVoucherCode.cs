using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;

namespace Persistence.Seeder
{
    public static class SeedVoucherCode
    {
        public static async Task Initialize(DataContext context)
        {
            if(!context.VoucherCodes.Any())
            {
                var voucherCodes = new List<VoucherCode>
                {
                    new VoucherCode { Code = "guthhBcf", PointsValue = 100, DateRedeem = null, IsValid = true },
                    new VoucherCode { Code = "BePTZprm", PointsValue = 85, DateRedeem = null, IsValid = true },
                    new VoucherCode { Code = "CGWgajLr", PointsValue = 150, DateRedeem = null, IsValid = true },
                    new VoucherCode { Code = "ybHBuzTf", PointsValue = 300, DateRedeem = null, IsValid = true },
                    new VoucherCode { Code = "YAxHhIPn", PointsValue = 50, DateRedeem = null, IsValid = true },
                    new VoucherCode { Code = "CuPkfqMt", PointsValue = 50, DateRedeem = null, IsValid = true },
                    new VoucherCode { Code = "pegbuKwZ", PointsValue = 80, DateRedeem = null, IsValid = true },
                    new VoucherCode { Code = "iArYabzy", PointsValue = 150, DateRedeem = null, IsValid = true },
                    new VoucherCode { Code = "GCPjxdlG", PointsValue = 90, DateRedeem = null, IsValid = true },
                    new VoucherCode { Code = "uWZiHuUo", PointsValue = 120, DateRedeem = null, IsValid = true },
                    new VoucherCode { Code = "OiqAJkxp", PointsValue = 100, DateRedeem = null, IsValid = true }
                };

                context.VoucherCodes.AddRange(voucherCodes);
                await context.SaveChangesAsync();
            }
        }
    }
}