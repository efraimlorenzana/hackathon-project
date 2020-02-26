using System;
using System.Collections.Generic;

namespace Domain
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public double Price { get; set; }
        public int Points { get; set; }
        public bool IsAvailable { get; set; }
        public virtual ICollection<ProductReview> Reviews { get; set; }
        public virtual Photo Thumbnail { get; set; }
    }
}