using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Domain
{
    public class AppUser : IdentityUser
    {
        public string Fullname { get; set; }
        public virtual Person Person { get; set; }
        public virtual IdentityRole Role { get; set; }
        public virtual ICollection<Points> EarnedPoints { get; set; }
        public virtual ICollection<PurchasedItem> PurchasedItems { get; set; }
    }
}