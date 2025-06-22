using Microsoft.AspNetCore.Identity;

namespace SmartBiz.Domain.Models
{
    public class AppRole : IdentityRole<int>
    {
        public AppRole(string name) : base(name) { }
    }
}
