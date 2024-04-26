using Microsoft.AspNetCore.Identity;

namespace Uyg04WorkProject.API.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
        public string PicUrl { get; set; }
    }
}
