using Microsoft.AspNetCore.Identity;

namespace ITI_MVC.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string Address {  get; set; }

    }
}
