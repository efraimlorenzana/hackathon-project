using System.Collections.Generic;
using Domain;

namespace Application.User
{
    public class ProfileInfo
    {
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public ICollection<Points> PointsHistory { get; set; }
        public int TotalPoints { get; set; }
        public ICollection<PurchasedItem> PurchasedProducts { get; set; }
    }
}