using System;

namespace Domain
{
    public class ProductReview
    {
        public Guid Id { get; set; }
        public string Comments { get; set; }
        public int Rating { get; set; }
        public string Customer { get; set; }
        public DateTime Date { get; set; }
    }
}