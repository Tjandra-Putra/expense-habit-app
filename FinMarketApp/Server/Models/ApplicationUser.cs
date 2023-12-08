using Microsoft.AspNetCore.Identity;

namespace FinMarketApp.Server.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set;}
        public string Password { get; set;}

    }
}
