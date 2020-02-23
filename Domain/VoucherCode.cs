using System;

namespace Domain
{
    public class VoucherCode
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public int PointsValue { get; set; }
        public DateTime? DateRedeem { get; set; }
        public bool IsValid { get; set; }
    }
}