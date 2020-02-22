using Microsoft.AspNetCore.Identity;

namespace Domain
{
    public class AppUser : IdentityUser
    {
        public string Fullname { get; set; }
        public virtual Person Person { get; set; }
        public virtual IdentityRole Role { get; set; }
    }
}