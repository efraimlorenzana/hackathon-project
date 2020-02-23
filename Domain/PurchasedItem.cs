using System;

namespace Domain
{
    public class PurchasedItem
    {
        public Guid Id { get; set; }
        public virtual Product Product { get; set; }
        public DateTime DatePurchased { get; set; }
        public string ShippingAddress { get; set; }
        public bool isDelivered { get; set; }
    }
}